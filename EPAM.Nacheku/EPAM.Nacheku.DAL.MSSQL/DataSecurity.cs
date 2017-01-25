namespace EPAM.Nacheku.DAL.MSSQL
{
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Web.Configuration;
    using log4net;

    public class DataSecurity
    {
        private static readonly string ConnectionString;

        private static readonly ILog Log = LogManager.GetLogger(typeof (DataSecurity));

        static DataSecurity()
        {
            ConnectionString = WebConfigurationManager.ConnectionStrings["NachekuDB"].ConnectionString;
            Log.Info(ConnectionString);
        }

        public static List<string> GetAllRoles()
        {
            var roles = new List<string>();
            using (var connection = new SqlConnection(ConnectionString))
            {
                var command = connection.CreateCommand();
                command.CommandText = "SELECT Role FROM dbo.[Roles]";
                connection.Open();
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    roles.Add((string) reader["Role"]);
                }
            }
            return roles;
        }

        public static string[] GetRolesForUser(string userLoginName)
        {
            return userLoginName == "Admin@mail.ru" ? new[] {"Admin", "User"} : new[] {"User"};
        }
    }
}
