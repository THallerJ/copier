using CommunityToolkit.Mvvm.Messaging.Messages;
using Copier.Interfaces;
using Copier.Models;

namespace Copier.Messages
{
    public class CopyJobSavedMessage :ValueChangedMessage<List<IJob<CopyJobConfig>>>
    {
        public CopyJobSavedMessage(List<IJob<CopyJobConfig>> jobs) : base(jobs) { }
    }
}
