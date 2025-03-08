using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Copier.Interfaces;
using System.Collections.ObjectModel;

namespace Copier.ViewModels
{
    public abstract class SelectFolderViewModel : ObservableObject
    {
        protected string? currentPath;
        private ObservableCollection<string> files = [];
        protected readonly IFileExplorer FileExplorer;
        protected readonly IFileCopyManager FileCopyManager;
        protected readonly IMessenger Messenger;
        private readonly IFolderDialog FolderDialog;
        private bool Result = false;

        public SelectFolderViewModel(IFileExplorer fileExplorer, IFileCopyManager fileCopyManager, IMessenger messenger, IFolderDialog folderDialog)
        {
            FileExplorer = fileExplorer;
            FileCopyManager = fileCopyManager;
            Messenger = messenger;
            FolderDialog = folderDialog;
        }

        public void SelectFolder()
        {
            Result = FolderDialog.SelectFolder();

            if (Result)
            {
                CurrentPath = FolderDialog.FolderName;
                if (CurrentPath != null) PathSelected(CurrentPath);
                UpdateFiles();
            }
        }

        public void UpdateFiles()
        {
            if (Result && CurrentPath != null)
            {
                Files.Clear();
                FileExplorer.FileMap(CurrentPath, (string file) => Files.Add(file));
            }
        }

        private RelayCommand? selectFolderCommand;

        public IRelayCommand SelectFolderCommand => selectFolderCommand ??= new RelayCommand(SelectFolder);

        public ObservableCollection<string> Files
        {
            get => files;
            private set => SetProperty(ref files, value);
        }

        public string? CurrentPath
        {
            get => currentPath;
            private set => SetProperty(ref currentPath, value);
        }

        protected abstract void PathSelected(string path);

        public abstract string Title { get; set; }
    }
}
