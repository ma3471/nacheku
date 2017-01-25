using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPAM.Nacheku.Entities;
using EPAM.Nacheku.DAL.MSSQL;

namespace EPAM.Nacheku.Logic
{
    public  class LogicMessage
    {
        private static Message _message;

        public static void Send(int toUserId, int fromUserId, string body, MessageType type)
        {
            _message = new Message(toUserId, fromUserId, body, DateTime.Now, type);

            DataMessage.Add(_message);
        }
    }
}
