using System;
using System.Collections.Generic;
using System.Web.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using EPAM.Nacheku.Entities;
using log4net;
using log4net.Config;

namespace EPAM.Nacheku.DAL.MSSQL
{
    public class DataMessage
    {
		private static readonly string ConnectionString;

        private static readonly ILog Log = LogManager.GetLogger(typeof (DataMessage));
		
		static DataMessage()
		{
			ConnectionString = WebConfigurationManager.ConnectionStrings["NachekuDB"].ConnectionString;
		    //BasicConfigurator.Configure();
            Log.Info(ConnectionString);
		}
		
		
        public static void Add(Message message)
        {
            Log.Info("I am in DAL" + message.Body);
        }

        public static IEnumerable<Message> GetAllMessagesToUser(Guid userId)
        {
            return new List<Message>();
        }

        public static IEnumerable<Message> GetAllMessagesFromUser(Guid userId)
        {
            return new List<Message>();
        }

        public static IEnumerable<Message> GetUnreadMessages(Guid userId)
        {
            return new List<Message>();
        }



    }
}
