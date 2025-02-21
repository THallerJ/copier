using Copier.Interfaces;

namespace Copier.ViewModels
{
    public class SelectToFolderViewModel : SelectFolderViewModel
    {
        private string title = "Copy to...";

        public SelectToFolderViewModel(IFileManager fileManager) : base(fileManager) { }

        public override string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        protected override void SaveCopyInfo()
        {
        }
    }
}
