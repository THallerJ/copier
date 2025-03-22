using CommunityToolkit.Mvvm.Messaging;
using Copier.Interfaces;

namespace Copier.ViewModels
{
    public class SelectSrcFolderViewModel : SelectFolderViewModel
    {
        private string title = "Copy from...";

        public SelectSrcFolderViewModel(IFileExplorer fileManager, IFileCopyManager fileCopyManager, IMessenger messenger, IFolderDialog folderDialog): base(fileManager, fileCopyManager, messenger,folderDialog) { }

        public override string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        protected override void PathSelected(string path)
        {
            FileCopyManager.SetSrcPath(path);
        }
    }
}
