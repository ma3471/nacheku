using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EPAM.Nacheku.UI.WebPages.App_Code.Security
{
    using System.Web;
    using System.Web.Security;
    using EPAM.Nacheku.Logic;

    public class LogicSecurity
    {

        public static bool Login(string login, string password, bool toRemember)
        {
            //return false;//DELETE!!!!!!!
            if (!LogicUserAccount.AreValidCredentials(login, password)) return false;
            FormsAuthentication.SetAuthCookie(login, toRemember);

            return true;
        }

        public static void LogOutAndRedirectToLoginPage()
        {
            LogicUserState.RemoveUserFromOnlineList(LogicUserAccount.GetAccountByLogin(HttpContext.Current.User.Identity.Name).UserId);
            FormsAuthentication.SignOut();
            FormsAuthentication.RedirectToLoginPage();
        }
    }
}