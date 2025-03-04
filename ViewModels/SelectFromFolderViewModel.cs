using CommunityToolkit.Mvvm.Messaging;
using Copier.Interfaces;

namespace Copier.ViewModels
{
    public class SelectFromFolderViewModel : SelectFolderViewModel
    {
        private string title = "Copy from...";

        public SelectFromFolderViewModel(IFileManager fileManager, ICopyManager copyManager, IMessenger messenger)
            : base(fileManager, copyManager, messenger) { }

        public override string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        protected override void SetPath(string path)
        {
            CopyManager.FromPath = path;
        }
    }
}
