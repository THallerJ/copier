using CommunityToolkit.Mvvm.Messaging;
using Copier.Interfaces;
using Copier.Models;

namespace Copier.ViewModels
{
    public class SelectToFolderViewModel : SelectFolderViewModel
    {
        private string title = "Copy to...";

        public SelectToFolderViewModel(IFileManager fileManager, ICopyManager copyManager, IMessenger messenger)
            : base(fileManager, copyManager, messenger)
        {
            initMessenger();
        }

        public override string Title
        {
            get => title;
            set => SetProperty(ref title, value);
        }

        protected override void SetPath(string path)
        {
            CopyManager.ToPath = path;
        }

        private void initMessenger()
        {
            Messenger.Register<SelectToFolderViewModel, FilesCopiedMessage>(this, (recipient, val) =>
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
