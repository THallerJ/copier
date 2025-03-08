using CommunityToolkit.Mvvm.Messaging;
using Copier.Interfaces;

namespace Copier.ViewModels
{
    public class SelectFromFolderViewModel : SelectFolderViewModel
    {
        private string title = "Copy from...";

        public SelectFromFolderViewModel(IFileExplorer fileManager, IFileCopyManager fileCopyManager, IMessenger messenger, IFolderDialog folderDialog): base(fileManager, fileCopyManager, messenger,folderDialog) { }

        public override string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        protected override void PathSelected(string path)
        {
            FileCopyManager.SetFromPath(path);
        }
    }
}
