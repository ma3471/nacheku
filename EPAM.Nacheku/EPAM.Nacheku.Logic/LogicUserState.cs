using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EPAM.Nacheku.DAL.MSSQL;
using EPAM.Nacheku.Entities;

namespace EPAM.Nacheku.Logic
{
    public class LogicUserState
    {
        public static void UpdateOnlineUserState(int userId, UserState state)
        {
            DataUserState.UpdateOnlineUserState(userId, state);
        }

        public static void RemoveUserFromOnlineList(int userId)
        {
            DataUserState.RemoveUserFromOnlineList(userId);
        }

        /// <summary>
        /// Get dictionary where KEY is userId and VALUE is UserState of last users activity
        /// </summary>
        /// <returns>Returns UserState collection of all online users </returns>
        public static Dictionary<int, UserState> GetAllOnlineUsersStates()
        {
            return DataUserState.GetAllOnlineUsersStates();
        }

        public static UserState GetOnlineUserState(int userId)
        {
            return DataUserState.GetOnlineUserState(userId);
        }

        public bool IsUserOnline(int userId)
        {
            return DataUserState.IsUserOnline(userId);
        }
    }
}
