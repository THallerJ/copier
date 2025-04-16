using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Copier.Interfaces;
using Copier.Messages;

namespace Copier.ViewModels
{
    public partial class ActionPanelViewModel : ObservableObject
    {
        private readonly IFileCopyManager FileCopyManager;
        private readonly IMessenger Messenger;
        private readonly IDialogFactory DialogFactory;
        private readonly CopyJobDialogViewModel CopyJobDialogViewModel;

        [ObservableProperty]
        public float progressValue = 0;

        public ActionPanelViewModel(IFileCopyManager fileCopyManager, IMessenger messenger, IDialogFactory dialogFactory, CopyJobDialogViewModel copyJobDialogViewModel)
        {
            FileCopyManager = fileCopyManager;
            Messenger = messenger;
            DialogFactory = dialogFactory;
            CopyJobDialogViewModel = copyJobDialogViewModel;
        }

        [RelayCommand]
        public async Task CopyFiles()
        {
            var progress = new Progress<float>(value => ProgressValue = value);
            await Task.Run(() =>FileCopyManager.RunCopyJob(progress));
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
