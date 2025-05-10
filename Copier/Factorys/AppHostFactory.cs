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
                services.AddSingleton<IMessenger, WeakReferenceMessenger>();
                services.AddSingleton(s => CreateFile(s));
                services.AddSingleton(s => CreateDirectory(s));
                services.AddSingleton(s => CreateJsonJobFileHandler(s));
                services.AddSingleton(s => CreateFileExplorer(s));
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
                services.AddSingleton(s => CreateShortcutManager(s));
            }).Build();
        }

        private static IFileExplorer CreateFileExplorer(IServiceProvider services)
        {
            var fileService = GetService<IFileService>(services);
            var directoryService = GetService<IDirectoryService>(services);

            return new FileExplorer(fileService, directoryService);
        }

        private static IFileCopyManager CreateFileCopyManager(IServiceProvider services)
        {
            var jsonJobFileHandler = GetService<IJsonJobFileHandler>(services);
            var fileService = GetService<IFileService>(services);
            var directoryService = GetService<IDirectoryService>(services);

            return FileCopyManager.Create(jsonJobFileHandler, fileService, directoryService);
        }

        private static MainWindowViewModel CreateMainWindowViewModel(IServiceProvider services)
        {
            var selectFromViewModel = GetService<SelectSrcFolderViewModel>(services);
            var selectToFolderViewModel = GetService<SelectDestFolderViewModel>(services);
            var actionPanelViewModel = GetService<ActionPanelViewModel>(services);
            var sidebarViewModel = GetService<SavedJobsViewModel>(services);
            var topMenuViewModel = GetService<TopMenuViewModel>(services);

            return new MainWindowViewModel(selectFromViewModel, selectToFolderViewModel, actionPanelViewModel, sidebarViewModel, topMenuViewModel);
        }

        private static SelectSrcFolderViewModel CreateSelectSrcFolderViewModel(IServiceProvider services)
        {
            var fileExplorer = GetService<IFileExplorer>(services);
            var fileCopyManager = GetService<IFileCopyManager>(services);
            var messenger = GetService<IMessenger>(services);
            var folderDialog = GetService<IFolderDialog>(services);
          
            return new SelectSrcFolderViewModel(fileExplorer, fileCopyManager, messenger, folderDialog);
        }

        private static SelectDestFolderViewModel CreateSelectToFolderViewModel(IServiceProvider services)
        {
            var fileExplorer = GetService<IFileExplorer>(services);
            var fileCopyManager = GetService<IFileCopyManager>(services);
            var messenger = GetService<IMessenger>(services);
            var folderDialog = GetService<IFolderDialog>(services);

            return new SelectDestFolderViewModel(fileExplorer, fileCopyManager, messenger, folderDialog);
        }

        private static ActionPanelViewModel CreateActionPanelViewModel(IServiceProvider services)
        {
            var dialogFactory = GetService<IDialogFactory>(services);
            return new ActionPanelViewModel(dialogFactory);
        }

        private static SavedJobsViewModel CreateSavedJobsViewModel(IServiceProvider services)
        {
            var messenger = GetService<IMessenger>(services);
            var fileCopyManager = GetService<IFileCopyManager>(services);
            var shortcutManager = GetService<IShortcutManager>(services);

            return new SavedJobsViewModel(fileCopyManager, messenger, shortcutManager);
        }

        private static CopyJobDialogViewModel CreateCopyJobDialogViewModel(IServiceProvider services)
        {
            var fileCopyManager = GetService<IFileCopyManager>(services);
            var messenger = GetService<IMessenger>(services);

            return new CopyJobDialogViewModel(fileCopyManager, messenger);
        }

        private static CopyProgressDialogViewModel CreateProgressViewModel(IServiceProvider services)
        {
            var fileCopyManager = GetService<IFileCopyManager>(services);
            var messenger = GetService<IMessenger>(services);

            return new CopyProgressDialogViewModel(fileCopyManager, messenger);
        }

        private static IDialogFactory CreateDialogFactory(IServiceProvider services)
        {
            return new DialogFactory(services);
        }

        private static TopMenuViewModel CreateTopMenuViewModel(IServiceProvider services)
        {
            var fileCopyManager = GetService<IFileCopyManager>(services);
            var messenger = GetService<IMessenger>(services);

            return new TopMenuViewModel(fileCopyManager, messenger);
        }

        private static IShortcutManager CreateShortcutManager(IServiceProvider services)
        {
            var fileService = GetService<IFileService>(services);
            return new ShortcutManager(fileService);
        }

        private static IFileService CreateFile(IServiceProvider services)
        {
            return new FileService();
        }

        private static IDirectoryService CreateDirectory(IServiceProvider services)
        {
            return new DirectoryService();
        }

        private static IJsonJobFileHandler CreateJsonJobFileHandler(IServiceProvider services)
        {
           var fileService = GetService<IFileService>(services);
           var directoryService = GetService<IDirectoryService>(services);
           
           return new JsonJobFileHandler(fileService, directoryService);
        }

        private static T GetService<T>(IServiceProvider services)
        {
            var s = services.GetService<T>();

            if (s == null)
            {
                throw new ArgumentNullException(nameof(services), "Required services are not registered.");
            }

            return s;
        }
    }
}
