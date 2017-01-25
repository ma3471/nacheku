using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Security;
using EPAM.Nacheku.Entities;
using EPAM.Nacheku.DAL.MSSQL;
using EPAM.Nacheku.Logic.SecurityHelper;
using log4net;

namespace EPAM.Nacheku.Logic
{
    public class LogicUserAccount
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(LogicUserAccount));

        private static readonly Object ThisLockForAddNewAccountAndProfile = new Object();

        public static bool AddNewAccountAndProfile(string login, string password, string firstName, string middleName, string lastName, DateTime birthDay)
        {
            if (UserExist(login))
            {
                return false;
            }
           
            lock (ThisLockForAddNewAccountAndProfile)
            {    
                int userId;            
                do
                {   //Generate new random userId and check for uniqueness
                    userId = ConstantLengthIntegerId.CreateRandomId();Log.Info(userId.ToString());
                } while (!ReferenceEquals(null, DataUserAccount.GetUserAccount(userId)));

                if (!DataUserAccount.AddUserAccount(login, PasswordHash.CreateHash(password), userId, DateTime.Now))
                {
                    return false;
                }

                var profile = new Profile(userId, firstName, middleName, lastName, birthDay);
                LogicProfile.AddNewProfile(profile);
            }  
          
            return true;
        }

        public static void UpdateLastActivityDate(string login, DateTime lastActivityDate)
        {
            DataUserAccount.UpdateLastActivityDate(login, lastActivityDate);
        }
      
        public static void ChangePassword(string login, string newPassword)
        {
            DataUserAccount.ChangePassword(login, PasswordHash.CreateHash(newPassword));
        }

        public static void ChangeLogin(string previousLogin, string newLogin)
        {
            DataUserAccount.ChangeLogin(previousLogin, newLogin);
        }

        public static bool UserExist(string login)
        {
            return !ReferenceEquals(null, DataUserAccount.GetUserAccount(login));
        }

        public static bool AreValidCredentials(string login, string password)
        {
            var userAcount = DataUserAccount.GetUserAccount(login);

            return !ReferenceEquals(null, userAcount) &&
                   PasswordHash.ValidatePassword(password, userAcount.Password);
        }

        public static UserAccount GetAccountByLogin(string login)
        {
            return DataUserAccount.GetUserAccount(login);
        }
    }
}
