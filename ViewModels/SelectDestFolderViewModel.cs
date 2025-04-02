using CommunityToolkit.Mvvm.Messaging;
using Copier.Interfaces;
using Copier.Messages;
using Copier.Models;

namespace Copier.ViewModels
{
    public class SelectDestFolderViewModel : SelectFolderViewModel
    {
        private string title = "COPY TO...";

        public SelectDestFolderViewModel(IFileExplorer fileManager, IFileCopyManager copyManager, IMessenger messenger, IFolderDialog folderDialog): base(fileManager, copyManager, messenger, folderDialog)
        {
            InitMessenger();
        }

        public override string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        protected override void PathSelected(string path)
        {
            FileCopyManager.SetDestination(path);
        }

        private void InitMessenger()
        {
            Messenger.Register<SelectDestFolderViewModel, FilesCopiedMessage>(this, (recipient, val) =>
            {
                recipient.FilesCopiedMessageReceived();
            });

            Messenger.Register<SelectDestFolderViewModel, CopyJobChangedMessage>(this, (recipient, val) =>
            {
                recipient.CopyJobChangedMessageReceived(val.Value);
            });
        }

        private void FilesCopiedMessageReceived()
        {
            UpdateFiles();
        }

        private void CopyJobChangedMessageReceived(CopyJob job)
        {
            if (job.Config.Dest != null)
            {
                PathSelected(job.Config.Dest);
                UpdateFiles(job.Config.Dest);
            }
        }
    }
}
