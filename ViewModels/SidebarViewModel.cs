using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Copier.Interfaces;
using Copier.Models;
using System.Diagnostics;

namespace Copier.ViewModels
{
    public partial class SidebarViewModel : ObservableObject
    {
        private readonly IMessenger Messenger;

        [ObservableProperty]
        public List<IJob<CopyJobConfig>> jobs = [];

        public SidebarViewModel(IMessenger messenger)
        {
            Messenger = messenger;
            InitMessenger();
            Debug.WriteLine(Jobs.Count);
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
    }
}
