using System;

namespace GaussLink.Data.Events
{
    public static class EventAggregator
    {
        public static void BroadCast(string message)
        {
            if (OnMessageTransmitted != null)
                OnMessageTransmitted(message);
        }

        public static Action<string> OnMessageTransmitted;
    }
}

