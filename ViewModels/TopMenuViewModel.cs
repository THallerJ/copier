using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Copier.Interfaces;
using Copier.Messages;

namespace Copier.ViewModels
{
    public partial class TopMenuViewModel
    {
        private readonly IFileCopyManager FileCopyManager;
        private readonly IMessenger Messenger;

        public TopMenuViewModel(IFileCopyManager fileCopyManager, IMessenger messenger)
        {
            FileCopyManager = fileCopyManager;
            Messenger = messenger;
        }

        [RelayCommand]
        public void ClearData()
        {
            FileCopyManager.Clear();
            SendClearDataMessage();
        }

        private void SendClearDataMessage()
        {
            Messenger.Send(new ClearDataMessage());
        }
    }
}