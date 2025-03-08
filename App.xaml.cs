using Copier.Factorys;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;

namespace Copier;

public partial class App : Application
{  
    public static IHost? AppHost { get; private set; }

    public App()
    {
        AppHost = AppHostFactory.CreateHost();   
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        await AppHost!.StartAsync();

        var startUpForm = AppHost.Services.GetRequiredService<MainWindow>();
        startUpForm.Show();

        base.OnStartup(e);
    }

    protected override async void OnExit(ExitEventArgs e)
    {
        await AppHost!.StopAsync();
        base.OnExit(e);
    }
}

