using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Copier.Interfaces;
using Copier.Messages;
using Copier.Models;
using Copier.Services;
using System.Diagnostics;

namespace Copier.ViewModels
{
    public partial class SavedJobsViewModel : ObservableObject
    {
        private readonly IMessenger Messenger;
        private readonly IFileCopyManager FileCopyManager;
        private readonly IShortcutManager ShortcutManager;

        [ObservableProperty]
        public List<IJob<CopyJobConfig>> jobs = [];

        public SavedJobsViewModel(IFileCopyManager fileCopyManager, IMessenger messenger, IShortcutManager shortcutManager)
        {
            Messenger = messenger;
            FileCopyManager = fileCopyManager;
            ShortcutManager = shortcutManager;
            Jobs = FileCopyManager.CopyJobs;
            InitMessenger();
        }

        private void InitMessenger()
        {
            Messenger.Register<SavedJobsViewModel, CopyJobSavedMessage>(this, (recipient, message) =>
            {
                recipient.CopyJobSavedMessageReceived(message);
            });

            Messenger.Register<SavedJobsViewModel, ClearDataMessage>(this, (recipient, message) =>
            {
                recipient.ClearDataMessageReceived();
            });
        }

        private void CopyJobSavedMessageReceived(CopyJobSavedMessage message)
        {
            Jobs = message.Value;
        }

        private void ClearDataMessageReceived()
        {
            Jobs = [];
        }

        private void SendCopyJobChangedMessage(CopyJob job)
        {
            Messenger.Send(new CopyJobChangedMessage(job));
        }

        [RelayCommand]
        public void SetCurrentJob(CopyJob job)
        {
            FileCopyManager.Job = job;
            SendCopyJobChangedMessage(job);
        }

        [RelayCommand]
        public async Task DeleteJob(CopyJob job)
        {
            if (job.Id == null) return;
            Jobs = await FileCopyManager.DeleteJobAsync(job.Id);
        }

        [RelayCommand]
        public async Task CreateCopyShortcut(CopyJob job)
        {
            if (job == null || job.Id == null || job.Config.Src == null || job.Config.Dest == null) 
                return;

            await ShortcutManager.CreateCopyShortcut(job.Id, job.Config.Src, job.Config.Dest);            
        }
    }
}
