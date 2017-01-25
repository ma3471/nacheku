using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Web.Routing;
using EPAM.Nacheku.Entities;

namespace EPAM.Nacheku.UI.WebPages
{
    public class Global : HttpApplication
    {
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof (Global));
        private static bool _firstRequest = true;
        private static EPAM.Nacheku.UI.WebPages.WindowsServiceSimulator _wss;

        public static Uri MyUri { get; private set; }

        protected void Application_Start(object sender, EventArgs e)
        {
            log4net.Config.XmlConfigurator.Configure(new System.IO.FileInfo(Server.MapPath("~/App_Code/Security/log4net.config"))); 
            Log.Info("Startup application.");
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            if (Context.Request.IsAuthenticated)
            {
                Log.Info("Session start for " + HttpContext.Current.User.Identity.Name);
                var userId = Logic.LogicUserAccount.GetAccountByLogin(HttpContext.Current.User.Identity.Name).UserId;
                Session.Remove("UserId");
                Session.Add("UserId", userId);
                var state = new UserState(StateEnum.LooksPage, userId);
                Logic.LogicUserState.UpdateOnlineUserState(userId, state);
            }
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {   Log.Info("Begin request");
            if (_firstRequest)
            { 
                MyUri = HttpContext.Current.Request.Url;
                _wss = new WindowsServiceSimulator();
                _firstRequest = false; Log.Info("I am in First request");
                _wss.RegisterCacheEntry();
            }
            //you can use Request.Url.GetComponents(UriComponents.SchemeAndServer, UriFormat.Unescaped);. This will return the sever starting from http.... till the server name and port.
            if (HttpContext.Current.Request.Url.ToString() == WindowsServiceSimulator.DummyPageUri)
            {
                _wss.RegisterCacheEntry();
                Log.Info("Global app_beginRequest invoke WSS registerCacheEntry");
                //Log.Info(HttpContext.Current.Request.ServerVariables["URL"]);
            }
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            if (HttpContext.Current.User == null)
            {
                return;
            }

            if (Context.Request.IsAuthenticated)
            {
                Log.Info("Authentication in progress for " + HttpContext.Current.User.Identity.Name);
                //var userId = Logic.LogicSecurity.GetAccountByLogin(HttpContext.Current.User.Identity.Name).UserId;
                //Session.Add("UserId", userId);
                //var state = new UserState(state: StateEnum.LooksPage, corrUserId: userId);
                //Logic.LogicUserState.UpdateOnlineUserState(userId, state);
            }
            //// look if any security information exists for this request
            //if (HttpContext.Current.User == null) return;

            //// see if this user is authenticated, any authenticated cookie (ticket) exists for this user
            //if (!HttpContext.Current.User.Identity.IsAuthenticated) return;

            //// see if the authentication is done using FormsAuthentication
            //if (!(HttpContext.Current.User.Identity is FormsIdentity)) return;

            //// Get the roles stored for this request from the ticket
            //// get the identity of the user
            //FormsIdentity FID = (FormsIdentity)HttpContext.Current.User.Identity;

            //// get the forms authetication ticket of the user
            //FormsAuthenticationTicket Ticket = FID.Ticket;

            //// get the roles stored as UserData into the ticket
            ////Get the stored user-data, in this case, Page Client ID and UserName
            //string userData = Ticket.UserData;
            //string[] roles = userData.Split(',');

            //// create generic principal and assign it to the current request
            //HttpContext.Current.User = new System.Security.Principal.GenericPrincipal(FID, roles);
            
        }
        /// <summary>
        /// RoleProvider replacement. Light version
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Application_PostAuthenticateRequest(object sender, EventArgs e)
        {
            var ctx = HttpContext.Current;
            if (ctx.Request.IsAuthenticated)
            {
                string[] roles = Logic.LogicSecurity.GetRolesForUser(ctx.User.Identity.Name);
                var newUser = new GenericPrincipal(ctx.User.Identity, roles);
                ctx.User = Thread.CurrentPrincipal = newUser;
            }
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            var objErr = Server.GetLastError();// <-- Returns null in Error500.aspx
           
            var errorMessage = ReferenceEquals(objErr, null)
                ? " probably 500 error"
                : objErr.GetBaseException().Message;
            var stackTrace = objErr == null ? "No Error no tracks ...sorry" : objErr.GetBaseException().StackTrace;
            if (Log.IsErrorEnabled)
            {
                Log.Error(errorMessage);  
                Log.Error(stackTrace);
            }

            //HttpContext.Current.Server.ClearError(); Error Pages become clear!!!!!!!!!!!
        }

        protected void Session_End(object sender, EventArgs e)
        {
            if (Session["UserId"] != null)
            {
                Logic.LogicUserState.RemoveUserFromOnlineList((int)Session["UserId"]);
                return;
            }

            Log.Warn("User had not been not deleted from online users list!!!");
            if (HttpContext.Current == null)
            {
                return;
            }

            Logic.LogicUserAccount.UpdateLastActivityDate(HttpContext.Current.User.Identity.Name, DateTime.Now);
             
        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}