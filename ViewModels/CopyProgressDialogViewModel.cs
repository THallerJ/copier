using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Copier.Interfaces;
using Copier.Messages;

namespace Copier.ViewModels
{
    public partial class CopyProgressDialogViewModel : ObservableObject, IDialog
    {
        public event EventHandler? OnCancel;
        public event EventHandler? OnOk;

        private readonly IMessenger Messenger;
        private readonly IFileCopyManager FileCopyManager;

        [ObservableProperty]
        public float progressValue = 0;

        public CopyProgressDialogViewModel(IFileCopyManager fileCopyManager, IMessenger messenger)
        {
            FileCopyManager = fileCopyManager;
            Messenger = messenger;
            // TODO: use delegate to call runcopyjob
        }

        public async Task RunCopyJob()
        {
            var progress = new Progress<float>(value => ProgressValue = value);
            await Task.Run(() => FileCopyManager.RunCopyJob(progress));
            Finished();
        }

        public void Finished() 
        { 
            OnOk?.Invoke(this, EventArgs.Empty);
            SendFilesCopiedMessage();
        }

        [RelayCommand]
        public void Cancel() 
        { 
            OnCancel?.Invoke(this, EventArgs.Empty); 
        }

        private void SendFilesCopiedMessage()
        {
            Messenger.Send(new FilesCopiedMessage());
        }
    }
}
