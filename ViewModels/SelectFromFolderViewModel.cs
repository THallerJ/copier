using Copier.Interfaces;

namespace Copier.ViewModels
{
    public class SelectFromFolderViewModel : SelectFolderViewModel
    {
        private string title = "Copy from...";

        public SelectFromFolderViewModel(IFileManager fileManager) : base(fileManager) { }

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
