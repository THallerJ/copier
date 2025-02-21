using Copier.Interfaces;
using Copier.Services;
using Copier.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Windows;

namespace Copier;

public partial class App : Application
{  
    public static IHost? AppHost { get; private set; }
    public App()
    {
        AppHost = Host.CreateDefaultBuilder().ConfigureServices((hostContext, services) =>
        {
            services.AddSingleton<MainWindow>();
            services.AddSingleton<IFileManager, FileManager>();
            services.AddSingleton(s => CreateMainWindowViewModel(s));
            services.AddTransient(s => CreateSelectFromFolderViewModel(s));
            services.AddTransient(s => CreateSelectToFolderViewModel(s));
        }).Build();
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

    private MainWindowViewModel CreateMainWindowViewModel(IServiceProvider services)
    {
        var selectFromViewModel = services.GetService<SelectFromFolderViewModel>();
        var selectToFolderView = services.GetService<SelectToFolderViewModel>();

        if (selectFromViewModel == null || selectToFolderView == null)
        {
            throw new InvalidOperationException("Required services are not registered.");
        }

        return new MainWindowViewModel(selectFromViewModel, selectToFolderView);
    }
    

    private SelectFromFolderViewModel CreateSelectFromFolderViewModel(IServiceProvider services)
    {
        var fileManager = services.GetService<IFileManager>();

        if (fileManager == null)
        {
            throw new InvalidOperationException("Required services are not registered.");
        }

        return new SelectFromFolderViewModel(fileManager);
    }

    private SelectToFolderViewModel CreateSelectToFolderViewModel(IServiceProvider services)
    {
        var fileManager = services.GetService<IFileManager>();

        if (fileManager == null)
        {
            throw new InvalidOperationException("Required services are not registered.");
        }

        return new SelectToFolderViewModel(fileManager);
    }
}

