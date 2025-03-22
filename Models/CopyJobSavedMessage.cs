using CommunityToolkit.Mvvm.Messaging.Messages;
using Copier.Interfaces;

namespace Copier.Models
{
    public class CopyJobSavedMessage :ValueChangedMessage<List<IJob<CopyJobConfig>>>
    {
        public CopyJobSavedMessage(List<IJob<CopyJobConfig>> jobs) : base(jobs) { }
    }
}
