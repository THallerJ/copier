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

        private readonly CancellationTokenSource CancellationTokenSource = new();

        [ObservableProperty]
        public float progressValue = 0;

        public CopyProgressDialogViewModel(IFileCopyManager fileCopyManager, IMessenger messenger)
        {
            FileCopyManager = fileCopyManager;
            Messenger = messenger;
        }

        [RelayCommand]
        public async Task RunCopyJob()
        {
            var progress = new Progress<float>(value => ProgressValue = value);
            var token = CancellationTokenSource.Token;
            await Task.Run(() => FileCopyManager.RunCopyJob(progress, token), token);
            if (token.IsCancellationRequested == false) Finished();
        }

        public void Finished() 
        {
            SendFilesCopiedMessage();
            OnOk?.Invoke(this, EventArgs.Empty);
        }

        [RelayCommand]
        public void Cancel() 
        {
            CancellationTokenSource.Cancel();
            OnCancel?.Invoke(this, EventArgs.Empty);
        }

        private void SendFilesCopiedMessage()
        {
            Messenger.Send(new FilesCopiedMessage());
        }
    }
}
