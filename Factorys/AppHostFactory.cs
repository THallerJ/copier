using CommunityToolkit.Mvvm.Messaging;
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
                services.AddTransient(s => CreateSelectSrcFolderViewModel(s));
                services.AddTransient(s => CreateSelectToFolderViewModel(s));
                services.AddTransient(s => CreateActionPanelViewModel(s));
                services.AddTransient(s => CreateSavedJobsViewModel(s));
                services.AddTransient(s => CreateCopyJobDialogViewModel(s));
                services.AddTransient(s => CreateTopMenuViewModel(s));
                services.AddTransient(s => CreateProgressViewModel(s));
            }).Build();
        }

        private static IFileCopyManager CreateFileCopyManager(IServiceProvider services)
        {
            var jsonJobFileHandler = services.GetService<IJsonJobFileHandler>();

            if (jsonJobFileHandler == null)
            {
                throw new InvalidOperationException("Required services are not registered.");
            }

            return FileCopyManager.Create(jsonJobFileHandler);
        }

        private static MainWindowViewModel CreateMainWindowViewModel(IServiceProvider services)
        {
            var selectFromViewModel = services.GetService<SelectSrcFolderViewModel>();
            var selectToFolderViewModel = services.GetService<SelectDestFolderViewModel>();
            var actionPanelViewModel = services.GetService<ActionPanelViewModel>();
            var sidebarViewModel = services.GetService<SavedJobsViewModel>();
            var topMenuViewModel = services.GetService<TopMenuViewModel>();

            if (selectFromViewModel == null || selectToFolderViewModel == null || actionPanelViewModel == null || sidebarViewModel == null || topMenuViewModel == null)
            {
                throw new InvalidOperationException("Required services are not registered.");
            }

            return new MainWindowViewModel(selectFromViewModel, selectToFolderViewModel, actionPanelViewModel, sidebarViewModel, topMenuViewModel);
        }

        private static SelectSrcFolderViewModel CreateSelectSrcFolderViewModel(IServiceProvider services)
        {
            var fileExplorer = services.GetService<IFileExplorer>();
            var fileCopyManager = services.GetService<IFileCopyManager>();
            var messenger = services.GetService<IMessenger>();
            var folderDialog = services.GetService<IFolderDialog>();

            if (fileExplorer == null || fileCopyManager == null || messenger == null || folderDialog == null)
            {
                throw new InvalidOperationException("Required services are not registered.");
            }

            return new SelectSrcFolderViewModel(fileExplorer, fileCopyManager, messenger, folderDialog);
        }

        private static SelectDestFolderViewModel CreateSelectToFolderViewModel(IServiceProvider services)
        {
            var fileExplorer = services.GetService<IFileExplorer>();
            var fileCopyManager = services.GetService<IFileCopyManager>();
            var messenger = services.GetService<IMessenger>();
            var folderDialog = services.GetService<IFolderDialog>();

            if (fileExplorer == null || fileCopyManager == null || messenger == null || folderDialog == null)
            {
                throw new InvalidOperationException("Required services are not registered.");
            }

            return new SelectDestFolderViewModel(fileExplorer, fileCopyManager, messenger, folderDialog);
        }

        private static ActionPanelViewModel CreateActionPanelViewModel(IServiceProvider services)
        {
            var dialogFactory = services.GetService<IDialogFactory>();
 
            if (dialogFactory == null)
            {
                throw new InvalidOperationException("Required services are not registered.");
            }

            return new ActionPanelViewModel(dialogFactory);
        }

        private static SavedJobsViewModel CreateSavedJobsViewModel(IServiceProvider services)
        {
            var messenger = services.GetService<IMessenger>();
            var fileCopyManager = services.GetService<IFileCopyManager>();

            if (messenger == null || fileCopyManager == null)
            {
                throw new InvalidOperationException("Required services are not registered.");
            }

            return new SavedJobsViewModel(fileCopyManager, messenger);
        }

        private static CopyJobDialogViewModel CreateCopyJobDialogViewModel(IServiceProvider services)
        {
            var fileCopyManager = services.GetService<IFileCopyManager>();
            var messenger = services.GetService<IMessenger>();


            if (fileCopyManager == null || messenger == null)
            {
                throw new InvalidOperationException("Required services are not registered.");
            }

            return new CopyJobDialogViewModel(fileCopyManager, messenger);
        }

        private static CopyProgressDialogViewModel CreateProgressViewModel(IServiceProvider services)
        {
            var fileCopyManager = services.GetService<IFileCopyManager>();
            var messenger = services.GetService<IMessenger>();

            if (fileCopyManager == null || messenger == null)
            {
                throw new InvalidOperationException("Required services are not registered.");
            }

            return new CopyProgressDialogViewModel(fileCopyManager, messenger);
        }

        private static IDialogFactory CreateDialogFactory(IServiceProvider services)
        {
            return new DialogFactory(services);
        }

        private static TopMenuViewModel CreateTopMenuViewModel(IServiceProvider services)
        {
            var fileCopyManager = services.GetService<IFileCopyManager>();
            var messenger = services.GetService<IMessenger>();

            if (fileCopyManager == null || messenger == null)
            {
                throw new InvalidOperationException("Required services are not registered.");
            }

            return new TopMenuViewModel(fileCopyManager, messenger);
        }
    }
}
