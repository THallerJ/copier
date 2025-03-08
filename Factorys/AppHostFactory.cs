using CommunityToolkit.Mvvm.Messaging;
using Copier.Factories;
using Copier.Interfaces;
using Copier.Services;
using Copier.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Copier.Factorys
{
    class AppHostFactory
    {
        public static IHost CreateHost()
        {
            return Host.CreateDefaultBuilder().ConfigureServices((hostContext, services) =>
            {
                services.AddSingleton<MainWindow>();
                services.AddSingleton<IFolderDialog, FolderDialog>();
                services.AddSingleton<IJsonJobFileHandler, JsonJobFileHandler>();
                services.AddSingleton<IMessenger, WeakReferenceMessenger>();
                services.AddSingleton<IFileExplorer, FileExplorer>();
                services.AddSingleton(s => CreateDialogFactory(s));
                services.AddSingleton(s => CreateFileCopyManager(s));
                services.AddSingleton(s => CreateMainWindowViewModel(s));
                services.AddTransient(s => CreateSelectFromFolderViewModel(s));
                services.AddTransient(s => CreateSelectToFolderViewModel(s));
                services.AddTransient(s => CreateActionPanelViewModel(s));
                services.AddTransient(s => CreateSidebarViewModel(s));
                services.AddTransient(s => CreateCopyJobDialogViewModel(s));
                services.AddTransient(s => CreateTopMenuViewModel(s));
            }).Build();
        }

        private static IFileCopyManager CreateFileCopyManager(IServiceProvider services)
        {
            var jsonWriter = services.GetService<IJsonJobFileHandler>();

            if (jsonWriter == null)
            {
                throw new InvalidOperationException("Required services are not registered.");
            }

            return new FileCopyManager(jsonWriter);
        }

        private static MainWindowViewModel CreateMainWindowViewModel(IServiceProvider services)
        {
            var selectFromViewModel = services.GetService<SelectFromFolderViewModel>();
            var selectToFolderViewModel = services.GetService<SelectToFolderViewModel>();
            var actionPanelViewModel = services.GetService<ActionPanelViewModel>();
            var sidebarViewModel = services.GetService<SidebarViewModel>();
            var topMenuViewModel = services.GetService<TopMenuViewModel>();

            if (selectFromViewModel == null || selectToFolderViewModel == null || actionPanelViewModel == null || sidebarViewModel == null || topMenuViewModel == null)
            {
                throw new InvalidOperationException("Required services are not registered.");
            }

            return new MainWindowViewModel(selectFromViewModel, selectToFolderViewModel, actionPanelViewModel, sidebarViewModel, topMenuViewModel);
        }

        private static SelectFromFolderViewModel CreateSelectFromFolderViewModel(IServiceProvider services)
        {
            var fileExplorer = services.GetService<IFileExplorer>();
            var fileCopyManager = services.GetService<IFileCopyManager>();
            var messenger = services.GetService<IMessenger>();
            var folderDialog = services.GetService<IFolderDialog>();

            if (fileExplorer == null || fileCopyManager == null || messenger == null || folderDialog == null)
            {
                throw new InvalidOperationException("Required services are not registered.");
            }

            return new SelectFromFolderViewModel(fileExplorer, fileCopyManager, messenger, folderDialog);
        }

        private static SelectToFolderViewModel CreateSelectToFolderViewModel(IServiceProvider services)
        {
            var fileExplorer = services.GetService<IFileExplorer>();
            var fileCopyManager = services.GetService<IFileCopyManager>();
            var messenger = services.GetService<IMessenger>();
            var folderDialog = services.GetService<IFolderDialog>();

            if (fileExplorer == null || fileCopyManager == null || messenger == null || folderDialog == null)
            {
                throw new InvalidOperationException("Required services are not registered.");
            }

            return new SelectToFolderViewModel(fileExplorer, fileCopyManager, messenger, folderDialog);
        }

        private static ActionPanelViewModel CreateActionPanelViewModel(IServiceProvider services)
        {
            var fileExplorer = services.GetService<IFileCopyManager>();
            var messenger = services.GetService<IMessenger>();
            var dialogFactory = services.GetService<IDialogFactory>();
            var copyJobDialogViewModel = services.GetService<CopyJobDialogViewModel>();


            if (fileExplorer == null || messenger == null || copyJobDialogViewModel == null || dialogFactory == null)
            {
                throw new InvalidOperationException("Required services are not registered.");
            }

            return new ActionPanelViewModel(fileExplorer, messenger, dialogFactory, copyJobDialogViewModel);
        }

        private static SidebarViewModel CreateSidebarViewModel(IServiceProvider services)
        {
            return new SidebarViewModel();
        }

        private static CopyJobDialogViewModel CreateCopyJobDialogViewModel(IServiceProvider services)
        {
            var fileCopyManager = services.GetService<IFileCopyManager>();

            if (fileCopyManager == null)
            {
                throw new InvalidOperationException("Required services are not registered.");
            }

            return new CopyJobDialogViewModel(fileCopyManager);
        }

        private static IDialogFactory CreateDialogFactory(IServiceProvider services)
        {
            var fileCopyManager = services.GetService<IFileCopyManager>();

            if (fileCopyManager == null)
            {
                throw new InvalidOperationException("Required services are not registered.");
            }

            return new DialogFactory(fileCopyManager);
        }

        private static TopMenuViewModel CreateTopMenuViewModel(IServiceProvider services)
        {
            var jsonJobFileHandler = services.GetService<IJsonJobFileHandler>();

            if (jsonJobFileHandler == null)
            {
                throw new InvalidOperationException("Required services are not registered.");
            }

            return new TopMenuViewModel(jsonJobFileHandler);
        }
    }
}
