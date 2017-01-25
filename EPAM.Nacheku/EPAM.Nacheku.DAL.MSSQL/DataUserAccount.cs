using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using EPAM.Nacheku.Entities;
using log4net;

namespace EPAM.Nacheku.DAL.MSSQL
{
    public class DataUserAccount
    {
        private static readonly string ConnectionString;

        private static readonly ILog Log = LogManager.GetLogger(typeof(DataUserAccount));

        static DataUserAccount()
        {
            ConnectionString = WebConfigurationManager.ConnectionStrings["NachekuDB"].ConnectionString;
            Log.Info(ConnectionString);
        }

        public static bool AddUserAccount(string login, string password, int userId, DateTime lastActivityDate)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var command = new SqlCommand("dbo.AddAccount", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                command.Parameters.Add(new SqlParameter("@Login", login));
                command.Parameters.Add(new SqlParameter("@Password", password));
                command.Parameters.Add(new SqlParameter("@UserId", userId));
                command.Parameters.Add(new SqlParameter("@LastActivityDate", lastActivityDate));

                connection.Open();
                var result = command.ExecuteNonQuery();
                return result > 0;
            }
        }

        public static UserAccount GetUserAccount(string login)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var command = new SqlCommand("dbo.GetAccountByLogin", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                command.Parameters.Add(new SqlParameter("@Login", login));

                connection.Open();

                var reader = command.ExecuteReader();
                if (reader.Read())
                {

                    var password = (string)reader["Password"];
                    var userId = (int)reader["UserId"];
                    var lastActivityDate = Convert.ToDateTime(reader["LastActivityDate"]);
                    return new UserAccount(login, password, userId, lastActivityDate);
                }

                return null;
            }
        }

        public static UserAccount GetUserAccount(int userId)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var command = new SqlCommand("dbo.GetAccountById", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                command.Parameters.Add(new SqlParameter("@UserId", userId));

                connection.Open();

                var reader = command.ExecuteReader();
                if (reader.Read())
                {
                    var loginName = (string)reader["Login"];
                    var password = (string)reader["Password"];
                    var lastActivityDate = Convert.ToDateTime(reader["LastActivityDate"]);
                    return new UserAccount(loginName, password, userId, lastActivityDate);
                }

                return null;
            }
        }

        public static bool UpdateLastActivityDate(string login, DateTime lastActivityDate)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var command = new SqlCommand("dbo.UpdateLastActivityDate", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                command.Parameters.Add(new SqlParameter("@Login", login));
                command.Parameters.Add(new SqlParameter("@LastActivityDate", lastActivityDate));

                connection.Open();
                var result = command.ExecuteNonQuery();
                return result > 0;
            }
        }

        public static bool ChangePassword(string login, string newPassword)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var command = new SqlCommand("dbo.UpdatePassword", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                command.Parameters.Add(new SqlParameter("@Login", login));
                command.Parameters.Add(new SqlParameter("@Password", newPassword));

                connection.Open();
                var result = command.ExecuteNonQuery();
                return result > 0;
            }
        }

        public static bool ChangeLogin(string previousLogin, string newLogin)
        {
            var account = GetUserAccount(previousLogin);
            if (account == null)
            {
                return false;
            }

            using (var connection = new SqlConnection(ConnectionString))
            {
                var command = new SqlCommand("dbo.ChangeLogin", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                command.Parameters.Add(new SqlParameter("@Login", newLogin));
                command.Parameters.Add(new SqlParameter("@UserId", account.UserId));

                connection.Open();
                var result = command.ExecuteNonQuery();
                return result > 0;
            }
        }
    }
}
