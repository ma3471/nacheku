namespace EPAM.Nacheku.UI.WebPages
{
    using System;
    using System.Net;
    using System.Web;
    using System.Web.Caching;

    /// <summary>
    /// Run scheduled jobs 24x7 using ASP.NET without requiring a Windows Service
    /// </summary>
    public class WindowsServiceSimulator
    {
        private const string DummyCacheItemKey = "DoesNotMatterWhatItIs";
        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(WindowsServiceSimulator));
        private static string _dummyPageName = "WindowsServiceSimulatorFakePage.cshtml";

        static WindowsServiceSimulator()
        {
            //Log.Info("I am in static constructor");//ser.Url;.ServerVariables["URL"]
            //string.Format(myUri.AbsoluteUri.Replace(myUri.AbsolutePath, "/"), _dummyPageName); Log.Info(DummyPageUri);
            //Request.Url.AbsoluteUri.Replace(Request.Url.AbsolutePath, "") & Page.ResolveUrl("~/")
            DummyPageUri = string.Format("{0}://{1}:{2}/{3}", Global.MyUri.Scheme, Global.MyUri.Host, Global.MyUri.Port,
                _dummyPageName);
            Log.Info(DummyPageUri);
        }

        public static string DummyPageUri { get; private set; }

        public void DoWork()
        {
            //Log.Info("Here I can do what I need periodically");
            //MailMessage msg = new MailMessage();
            //msg.From = "abc@cde.fgh";
            //msg.To = "ijk@lmn.opq";
            //msg.Subject = "Reminder: " + DateTime.Now.ToString();
            //msg.Body = "This is a server generated message";

            //SmtpClient.Send(msg);
        }

        public void CacheItemRemovedCallback(string key, object value, CacheItemRemovedReason reason)
        {
            //Log.Info("I am in windows Service simulator");
           // HitPage();
            DoWork();
        }

        public bool RegisterCacheEntry()
        {
            if (HttpContext.Current.Cache[DummyCacheItemKey] != null)
            {
                return false;
            }

            HttpContext.Current.Cache.Add(
                DummyCacheItemKey,
                "Test",
                null,
                DateTime.MaxValue,
                TimeSpan.FromMinutes(1),
                CacheItemPriority.Normal,
                new CacheItemRemovedCallback(CacheItemRemovedCallback));
            return true;
        }

        private static void HitPage()
        {
            WebClient client = new WebClient();
            //client.Credentials = System.Net.CredentialCache.DefaultCredentials;
            Log.Info("the request made by WSS");
            client.DownloadData(DummyPageUri);
            Log.Info("just to check how webclient works");
        }
    }
}