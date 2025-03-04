using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Copier.Interfaces;
using Microsoft.Win32;
using System.Collections.ObjectModel;

namespace Copier.ViewModels
{
    public abstract class SelectFolderViewModel : ObservableObject
    {
        protected string? currentPath;
        private ObservableCollection<string> files = [];
        protected readonly IFileManager FileManager;
        protected readonly ICopyManager CopyManager;
        protected readonly IMessenger Messenger;
        private bool Result = false;

        public SelectFolderViewModel(IFileManager fileManager, ICopyManager copyManager, IMessenger messenger)
        {
            FileManager = fileManager;
            CopyManager = copyManager;
            Messenger = messenger; 
        }

        public void OpenDialog()
        {
            var folderDialog = new OpenFolderDialog();
            folderDialog.Multiselect = false;
            Result = folderDialog.ShowDialog() ?? false;

            if (Result)
            {
                CurrentPath = folderDialog.FolderName;
                if (CurrentPath != null) SetPath(CurrentPath);
                UpdateFiles();
            }
        }

        public void UpdateFiles()
        {
            if (Result && currentPath != null)
            {
                Files.Clear();
                FileManager.FileMap(currentPath, (string file) => Files.Add(file));
            }
        }

        private RelayCommand? openDialogCommand;

        public IRelayCommand OpenDialogCommand => openDialogCommand ??= new RelayCommand(OpenDialog);

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

        protected abstract void SetPath(string path);

        public abstract string Title { get; set; }
    }
}
