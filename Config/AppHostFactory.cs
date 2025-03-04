using CommunityToolkit.Mvvm.Messaging;
using Copier.Interfaces;
using Copier.Services;
using Copier.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Copier.Config
{
    class AppHostFactory
    {
        public static IHost CreateHost()
        {
            return Host.CreateDefaultBuilder().ConfigureServices((hostContext, services) =>
            {
                services.AddSingleton<MainWindow>();
                services.AddSingleton<IMessenger, WeakReferenceMessenger>();
                services.AddSingleton<IFileManager, FileManager>();
                services.AddSingleton<ICopyManager, CopyManager>();
                services.AddSingleton(s => CreateMainWindowViewModel(s));
                services.AddTransient(s => CreateSelectFromFolderViewModel(s));
                services.AddTransient(s => CreateSelectToFolderViewModel(s));
                services.AddTransient(s => CreateActionPanelViewModel(s));
            }).Build();
        }

        private static MainWindowViewModel CreateMainWindowViewModel(IServiceProvider services)
        {
            var selectFromViewModel = services.GetService<SelectFromFolderViewModel>();
            var selectToFolderViewModel = services.GetService<SelectToFolderViewModel>();
            var actionPanelViewModel = services.GetService<ActionPanelViewModel>();

            if (selectFromViewModel == null || selectToFolderViewModel == null || actionPanelViewModel == null)
            {
                throw new InvalidOperationException("Required services are not registered.");
            }

            return new MainWindowViewModel(selectFromViewModel, selectToFolderViewModel, actionPanelViewModel);
        }

        private static SelectFromFolderViewModel CreateSelectFromFolderViewModel(IServiceProvider services)
        {
            var fileManager = services.GetService<IFileManager>();
            var copyManager = services.GetService<ICopyManager>();
            var messenger = services.GetService<IMessenger>();

            if (fileManager == null || copyManager == null || messenger == null)
            {
                throw new InvalidOperationException("Required services are not registered.");
            }

            return new SelectFromFolderViewModel(fileManager, copyManager, messenger);
        }

        private static SelectToFolderViewModel CreateSelectToFolderViewModel(IServiceProvider services)
        {
            var fileManager = services.GetService<IFileManager>();
            var copyManager = services.GetService<ICopyManager>();
            var messenger = services.GetService<IMessenger>();

            if (fileManager == null || copyManager == null || messenger == null)
            {
                throw new InvalidOperationException("Required services are not registered.");
            }

            return new SelectToFolderViewModel(fileManager, copyManager, messenger);
        }

        private static ActionPanelViewModel CreateActionPanelViewModel(IServiceProvider services)
        {
            var copyManager = services.GetService<ICopyManager>();
            var messenger = services.GetService<IMessenger>();

            if (copyManager == null || messenger == null)
            {
                throw new InvalidOperationException("Required services are not registered.");
            }

            return new ActionPanelViewModel(copyManager, messenger);
        }
    }
}
