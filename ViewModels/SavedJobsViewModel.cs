using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Copier.Interfaces;
using Copier.Models;
using System.Diagnostics;

namespace Copier.ViewModels
{
    public partial class SavedJobsViewModel : ObservableObject
    {
        private readonly IMessenger Messenger;
        private readonly IFileCopyManager FileCopyManager;

        [ObservableProperty]
        public List<IJob<CopyJobConfig>> jobs = [];


        [ObservableProperty]
        private CopyJob? selectedJob;

        public SavedJobsViewModel(IFileCopyManager fileCopyManager, IMessenger messenger)
        {
            Messenger = messenger;
            FileCopyManager = fileCopyManager;
            Jobs = FileCopyManager.CopyJobs;
            InitMessenger();
        }

        private void InitMessenger()
        {
            Messenger.Register<SavedJobsViewModel, CopyJobSavedMessage>(this, (recipient, message) =>
            {
                recipient.MessageReceived(message);
            });
        }

        private void MessageReceived(CopyJobSavedMessage message)
        {
            Jobs = message.Value;
        }

        [RelayCommand]
        public void SetCurrentJob(CopyJob job)
        {
            Debug.WriteLine("set " + job.Id);
        }

        [RelayCommand]
        public void DeleteJob(CopyJob job)
        {
            if (job != null)
            {
                Debug.WriteLine("Delete " + job.Id);
            }
        }
    }
}
