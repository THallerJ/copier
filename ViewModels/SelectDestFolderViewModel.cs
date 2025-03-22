using CommunityToolkit.Mvvm.Messaging;
using Copier.Interfaces;
using Copier.Models;

namespace Copier.ViewModels
{
    public class SelectDestFolderViewModel : SelectFolderViewModel
    {
        private string title = "Copy to...";

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
            FileCopyManager.SetDestPath(path);
        }

        private void InitMessenger()
        {
            Messenger.Register<SelectDestFolderViewModel, FilesCopiedMessage>(this, (recipient, val) =>
            {
                recipient.MessageReceived();
            });
        }

        private void MessageReceived()
        {
            UpdateFiles();
        }
    }
}
