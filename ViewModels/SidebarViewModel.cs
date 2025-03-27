using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Copier.Interfaces;
using Copier.Models;
using System.Diagnostics;

namespace Copier.ViewModels
{
    public partial class SidebarViewModel : ObservableObject
    {
        private readonly IMessenger Messenger;
        private readonly IFileCopyManager FileCopyManager;

        [ObservableProperty]
        public List<IJob<CopyJobConfig>> jobs = [];

        public SidebarViewModel(IFileCopyManager fileCopyManager, IMessenger messenger)
        {
            Messenger = messenger;
            FileCopyManager = fileCopyManager;
            Jobs = FileCopyManager.CopyJobs;
            InitMessenger();
        }

        private void InitMessenger()
        {
            Messenger.Register<SidebarViewModel, CopyJobSavedMessage>(this, (recipient, message) =>
            {
                recipient.MessageReceived(message);
            });
        }

        private void MessageReceived(CopyJobSavedMessage message)
        {
            Jobs = message.Value;
        }

        [RelayCommand]
        public void SetCurrentJob(CopyJob thing)
        {
            Debug.WriteLine(thing.Id);
        }
    }
}
