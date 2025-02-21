using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Copier.Interfaces;
using Microsoft.Win32;
using System.Collections.ObjectModel;


namespace Copier.ViewModels
{
    public abstract class SelectFolderViewModel : ObservableObject
    {
        public string buttonText = "*** SELECT FOLDER LOCATION ***";
        private ObservableCollection<string> files = [];
        protected readonly IFileManager FileManager;

        public SelectFolderViewModel(IFileManager fileManager)
        {
            FileManager = fileManager;
        }

        public void OpenDialog()
        {
            var folderDialog = new OpenFolderDialog();
            folderDialog.Multiselect = false;
            var result = folderDialog.ShowDialog();

            if (result == true)
            {
                buttonText = folderDialog.FolderName;
                SaveCopyInfo();
                Files.Clear();
                FileManager.FileMap(buttonText, (string file) => Files.Add(file));
            }
        }

        private RelayCommand? openDialogCommand;

        public IRelayCommand OpenDialogCommand => openDialogCommand ??= new RelayCommand(OpenDialog);

        public ObservableCollection<string> Files
        {
            get => files;
            private set => SetProperty(ref files, value);
        }

        public string? ButtonText
        {
            get => buttonText;
            private set => SetProperty(ref buttonText, value ?? string.Empty);
        }

        protected abstract void SaveCopyInfo();

        public abstract string Title { get; set; }
    }
}
