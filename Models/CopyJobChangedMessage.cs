using CommunityToolkit.Mvvm.Messaging.Messages;

namespace Copier.Models
{
    class CopyJobChangedMessage : ValueChangedMessage<CopyJob>
    {
        public CopyJobChangedMessage(CopyJob job) : base(job) { }
    }
}
