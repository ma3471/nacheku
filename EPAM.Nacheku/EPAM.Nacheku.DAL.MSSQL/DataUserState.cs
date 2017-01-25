using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using System.Web.Caching;
using System.Web.Configuration;
using EPAM.Nacheku.Entities;
using log4net;

namespace EPAM.Nacheku.DAL.MSSQL
{
    public class DataUserState
    {
        private static bool UseCacheNotApplicationToKeepOnlineUsers;
        private static double SlidingExpirationForOnlineUserCache;
        private static readonly Object ThisLockForUpdateOnlineUser = new Object();
        private static readonly ILog Log = LogManager.GetLogger(typeof(DataUserState));

        static DataUserState()
        {
            Boolean.TryParse(WebConfigurationManager.AppSettings["UseCacheNotApplicationToKeepOnlineUsers"],
                out UseCacheNotApplicationToKeepOnlineUsers);
            Double.TryParse(WebConfigurationManager.AppSettings["SlidingExpirationForOnlineUserCache"],
                out SlidingExpirationForOnlineUserCache);
            if (Log.IsInfoEnabled)
            {
                Log.Info(String.Format("UseCacheNotApplicationToKeepOnlineUsers = {0}", UseCacheNotApplicationToKeepOnlineUsers));
                Log.Info(String.Format("SlidingExpirationForOnlineUserCache = {0}", SlidingExpirationForOnlineUserCache));
            }
        }

        /// <summary>
        /// Update if exist or Add new if do not
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public static bool UpdateOnlineUserState(int userId, UserState state)
        {
            if (UseCacheNotApplicationToKeepOnlineUsers)
            {
                if (ReferenceEquals( null, state))
                {
                    return false;
                }

                HttpRuntime.Cache.Insert(
                    String.Format("UserId{0}",userId), 
                    state,
                    null,
                    Cache.NoAbsoluteExpiration, 
                    TimeSpan.FromMinutes(SlidingExpirationForOnlineUserCache),
                    CacheItemPriority.NotRemovable,
                    null);
                return true;
            }

            if (HttpContext.Current == null)
            {
                return false;
            }

            lock (ThisLockForUpdateOnlineUser)
            {
                var usersOnline = (Dictionary<int, UserState>)HttpContext.Current.Application["UsersOnline"] ??
                              new Dictionary<int, UserState>();

                usersOnline.Remove(userId);
                usersOnline.Add(userId, state);
                HttpContext.Current.Application["UsersOnline"] = usersOnline;
            }

            return true;
        }

        public static Dictionary<int, UserState> GetAllOnlineUsersStates()
        { 
            var usersOnline = new Dictionary<int, UserState>();

            if (UseCacheNotApplicationToKeepOnlineUsers)
            {
                var enumerator = HttpRuntime.Cache.GetEnumerator();
                var pattern = new Regex(@"(?<=UserId)\d+");
                while (enumerator.MoveNext())
                {
                    var key = (string)enumerator.Key;
                    if (!pattern.IsMatch(key))
                    {
                        continue;
                    }

                    int userId;
                    if (int.TryParse(pattern.Match(key).ToString(), out userId))
                    {
                        usersOnline.Add(userId, (UserState)enumerator.Value);
                    }
                }

                return usersOnline;
            }
            
            if (HttpContext.Current == null)
            {
                return usersOnline;
            }

            usersOnline = (Dictionary<int, UserState>)HttpContext.Current.Application["UsersOnline"];
            if (usersOnline != null)
            {
                return usersOnline;
            }

            usersOnline = new Dictionary<int, UserState>();
            HttpContext.Current.Application["UsersOnline"] = usersOnline;
            return usersOnline;
        }

        public static void RemoveUserFromOnlineList(int userId)
        {
            if (UseCacheNotApplicationToKeepOnlineUsers)
            {
                var key = String.Format("UserId{0}", userId);
                if (HttpRuntime.Cache[key] != null)
                {
                    HttpRuntime.Cache.Remove(key);
                }
                return;
            }

            var usersOnline = GetAllOnlineUsersStates();
    
            usersOnline.Remove(userId);
            //HttpContext.Current.Application["UsersOnline"] = usersOnline;      
        }

        public static bool IsUserOnline(int userId)
        {
            if (UseCacheNotApplicationToKeepOnlineUsers)
            {
                var key = String.Format("UserId{0}", userId);
                return HttpRuntime.Cache[key] != null;
            }

            return GetAllOnlineUsersStates().ContainsKey(userId);
        }

        public static UserState GetOnlineUserState(int userId)
        {
            if (UseCacheNotApplicationToKeepOnlineUsers)
            {
                var key = String.Format("UserId{0}", userId);
                if (HttpRuntime.Cache[key] != null)
                {
                    return (UserState)HttpRuntime.Cache[key];
                }
                return null;
            }

            UserState state = GetAllOnlineUsersStates().TryGetValue(userId, out state) ? state : null;
     
            return state;
            
        }
    }
}
