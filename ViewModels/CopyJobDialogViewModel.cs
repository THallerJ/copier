using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Copier.Interfaces;
using Copier.Models;

namespace Copier.ViewModels
{
    public partial class CopyJobDialogViewModel : ISubmittableDialog
    {
        public event EventHandler? OnCancel;
        public event EventHandler? OnOk;
        private readonly IFileCopyManager FileCopyManager;
        private readonly IMessenger Messenger;
        public CopyJobDialogViewModel(IFileCopyManager fileCopyManager, IMessenger messenger)
        {
            FileCopyManager = fileCopyManager;
            Messenger = messenger;
        }

        [RelayCommand]
        public async Task Save(string name)
        {
            var jobs = await FileCopyManager.SaveCopyJobAsync(name);
            SendMessage(jobs);
            OnOk?.Invoke(this, EventArgs.Empty);
        }

        [RelayCommand]
        public void Cancel()
        {
            OnCancel?.Invoke(this, EventArgs.Empty);
        }

        private void SendMessage(List<IJob<CopyJobConfig>> jobs)
        {
            Messenger.Send(new CopyJobSavedMessage(jobs));
        }
    }
}
