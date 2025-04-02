using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Copier.Interfaces;
using Copier.Messages;

namespace Copier.ViewModels
{
    public partial class ActionPanelViewModel
    {
        private readonly IFileCopyManager FileCopyManager;
        private readonly IMessenger Messenger;
        private readonly IDialogFactory DialogFactory;
        private readonly CopyJobDialogViewModel CopyJobDialogViewModel;

        public ActionPanelViewModel(IFileCopyManager fileCopyManager, IMessenger messenger, IDialogFactory dialogFactory, CopyJobDialogViewModel copyJobDialogViewModel)
        {
            FileCopyManager = fileCopyManager;
            Messenger = messenger;
            DialogFactory = dialogFactory;
            CopyJobDialogViewModel = copyJobDialogViewModel;
        }

        [RelayCommand]
        public void CopyFiles()
        {
            FileCopyManager.RunCopyJob();
            SendFilesCopiedMessage();
        }

        [RelayCommand]
        public void SaveCopyJob()
        {
            DialogFactory.ShowDialog(CopyJobDialogViewModel);
        }

        private void SendFilesCopiedMessage()
        {
            Messenger.Send(new FilesCopiedMessage());
        }
    }
}
