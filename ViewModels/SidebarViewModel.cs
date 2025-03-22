using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Copier.Models;
using System.Diagnostics;

namespace Copier.ViewModels
{
    public class SidebarViewModel : ObservableObject
    {
        private readonly IMessenger Messenger;

        public SidebarViewModel(IMessenger messenger)
        {
            Messenger = messenger;
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
            Debug.WriteLine("SIDEBAR RECEIVED");
            Debug.WriteLine(message.Value.Count);
        }
    }
}
