using System;

namespace MailgunWrapper
{
    [Flags]
    public enum MailgunEventType
    {
        None = 0,
        Accepted = 1,
        Rejected = 2,
        Delivered = 4,
        Failed = 8,
        Opened = 16,
        Clicked = 32,
        Unsubscribed = 64,
        Complained = 128,
        Stored = 256
    }
}
