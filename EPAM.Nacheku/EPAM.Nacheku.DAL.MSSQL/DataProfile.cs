using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using  EPAM.Nacheku.Entities;
using log4net;

namespace EPAM.Nacheku.DAL.MSSQL
{
    public class DataProfile
    {
        private static readonly string ConnectionString;

        private static readonly ILog Log = LogManager.GetLogger(typeof (DataProfile));
		
		static DataProfile()
		{
			ConnectionString = WebConfigurationManager.ConnectionStrings["NachekuDB"].ConnectionString;
            Log.Info(ConnectionString);
		}

        public static bool AddProfile(Profile profile)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var command = new SqlCommand("dbo.AddProfile", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                command.Parameters.Add(new SqlParameter("@UserId", profile.UserId));
                command.Parameters.Add(new SqlParameter("@FirstName", profile.FirstName));
                command.Parameters.Add(new SqlParameter("@MiddleName", profile.MiddleName));
                command.Parameters.Add(new SqlParameter("@LastName", profile.LastName));
                command.Parameters.Add(new SqlParameter("@BirthDay", profile.BirthDay));
                command.Parameters.Add(new SqlParameter("@Location", String.IsNullOrWhiteSpace(profile.Location) ? DBNull.Value : (object)profile.Location));
                command.Parameters.Add(new SqlParameter("@AvatarId", profile.AvatarId == null ? DBNull.Value : (object)profile.AvatarId));
                command.Parameters.Add(new SqlParameter("@Skill", String.IsNullOrWhiteSpace(profile.Skill) ? DBNull.Value : (object)profile.Skill));
                //command.Parameters.Add(new SqlParameter("@MiddleName", profile.MiddleName));
                connection.Open();
                var result = command.ExecuteNonQuery();
                return result > 0;
            }
        }

        public static bool UpdateProfile(Profile profile)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var command = new SqlCommand("dbo.UpdateProfile", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                command.Parameters.Add("@UserId", SqlDbType.Int).Value = profile.UserId;
                command.Parameters.Add("@FirstName", SqlDbType.NVarChar).Value = profile.FirstName;
                command.Parameters.Add("@MiddleName", SqlDbType.NVarChar).Value = profile.MiddleName;
                command.Parameters.Add("@LastName", SqlDbType.NVarChar).Value = profile.LastName;
                command.Parameters.Add("@BirthDay", SqlDbType.DateTime).Value = profile.BirthDay;
                command.Parameters.Add("@Location", SqlDbType.NVarChar).Value = String.IsNullOrWhiteSpace(profile.Location)? DBNull.Value: (object)profile.Location;
                command.Parameters.Add("@AvatarId", SqlDbType.UniqueIdentifier).Value = profile.AvatarId == null? DBNull.Value: (object)profile.AvatarId;               
                command.Parameters.Add("@Skill", SqlDbType.NVarChar).Value = String.IsNullOrWhiteSpace(profile.Skill) ? DBNull.Value : (object)profile.Skill;
                
                connection.Open();
                var result = command.ExecuteNonQuery();
                return result > 0;
            }
        }

        public static Profile GetProfileByUserId(int userId)
        {
            using (var connection = new SqlConnection(ConnectionString))
            {
                var command = new SqlCommand("dbo.GetProfileById", connection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                command.Parameters.Add(new SqlParameter("@UserId", userId));

                connection.Open();

                var reader = command.ExecuteReader();
                if (!reader.Read())
                {
                    return null;
                }

                var firstName = (string)reader["FirstName"];
                var middleName = (string)reader["MiddleName"];
                var lastName = (string)reader["LastName"];
                var birthDay = (DateTime)reader["BirthDay"];
                var location = reader["Location"] == DBNull.Value? null: (string)reader["Location"];
                Guid? avatarId = reader["AvatarId"] == DBNull.Value? null:  (Guid?)reader["AvatarId"];
                var skill = reader["Skill"] == DBNull.Value? null: (string)reader["Skill"];

                return new Profile(userId, firstName, middleName, lastName, birthDay, location, avatarId, skill);
            }
        }
    }
}
