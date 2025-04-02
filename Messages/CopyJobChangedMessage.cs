using CommunityToolkit.Mvvm.Messaging.Messages;
using Copier.Models;

namespace Copier.Messages
{
    class CopyJobChangedMessage : ValueChangedMessage<CopyJob>
    {
        public CopyJobChangedMessage(CopyJob job) : base(job) { }
    }
}
