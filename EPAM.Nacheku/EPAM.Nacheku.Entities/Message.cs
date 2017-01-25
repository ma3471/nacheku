using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAM.Nacheku.Entities
{
    public class Message
    {
        public int ToUser { get; set; }

        public int FromUser { get; set; }

        public String Body { get; set; }

        public DateTime TimeStamp { get; set; }

        public Boolean Unread { get; set; }

        public MessageType Type { get; set; }

        public Message(int toUserId, int fromUserId, String body, DateTime when, MessageType type)
        {
            this.ToUser = toUserId;
            this.FromUser = fromUserId;
            this.Body = body;
            this.TimeStamp = when;
            this.Unread = true;
            this.Type = type;
        }
    }

    public enum MessageType
    {
        Chat,
        Forum,
        Notification,
        NotificationWithConfirmation
    }
}
