using CommunityToolkit.Mvvm.Messaging;
using Copier.Interfaces;
using Copier.Models;

namespace Copier.ViewModels
{
    public class SelectSrcFolderViewModel : SelectFolderViewModel
    {
        private string title = "Copy from...";

        public SelectSrcFolderViewModel(IFileExplorer fileManager, IFileCopyManager fileCopyManager, IMessenger messenger, IFolderDialog folderDialog): base(fileManager, fileCopyManager, messenger,folderDialog) {
            InitMessenger();
        }
        
        public override string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }
        private void InitMessenger()
        {
            Messenger.Register<SelectSrcFolderViewModel, CopyJobChangedMessage>(this, (recipient, val) =>
            {
                recipient.CopyJobChangedMessageReceived(val.Value);
            });
        }

        protected override void PathSelected(string path)
        {
            FileCopyManager.SetSource(path);
        }

        private void CopyJobChangedMessageReceived(CopyJob job)
        {
            if (job.Config.Src != null)
            {
                PathSelected(job.Config.Src);
                UpdateFiles(job.Config.Src);
            }
        }
    }
}
