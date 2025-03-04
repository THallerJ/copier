using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Copier.Interfaces;
using Copier.Models;

namespace Copier.ViewModels
{
    public partial class ActionPanelViewModel
    {
        private readonly ICopyManager CopyManager;
        private readonly IMessenger Messenger;

        public ActionPanelViewModel(ICopyManager copyManager, IMessenger messenger)
        {
            CopyManager = copyManager;
            Messenger = messenger;
        }

        [RelayCommand]
        public void CopyFiles()
        {
            CopyManager.runCopy();
            SendMessage();
        }

        private void SendMessage()
        {
            Messenger.Send(new FilesCopiedMessage());
        }
    }
}
