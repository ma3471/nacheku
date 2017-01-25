using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EPAM.Nacheku.Entities
{
    public class UserAccount
    {
        public string LoginName { get; private set; }
        public string Password { get; private set; }
        public int UserId { get; private set; }
        public DateTime LastActivityDateTime { get; private set; }

        //public UserAccount()
        //{
            
        //}

        public UserAccount(string loginName, string password, int userId, DateTime lastActivityDateTime)
        {
            this.LoginName = loginName;
            this.Password = password;
            this.UserId = userId;
            this.LastActivityDateTime = lastActivityDateTime;
        }
    }
}
