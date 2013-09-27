using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml.Linq;
using System.Configuration;

namespace BBQCode.Web.Helpers
{
    public class Twitter
    {
        private static int CachedMinutes { get { return int.Parse(ConfigurationManager.AppSettings["Twitter.CachedMinutes"]); } }
        private static string UriFeed { get { return ConfigurationManager.AppSettings["Twitter.UriFeed"]; } }
        private static DateTime RefreshAt { get; set; }

        private static XDocument _Feed;
        public static XDocument Feed
        {
            get
            {
                if (_Feed == null || RefreshAt < DateTime.Now)
                {
                    _Feed = XDocument.Load(UriFeed);
                    RefreshAt = DateTime.Now.AddMinutes(CachedMinutes);
                }
                return _Feed;
            }
        }
    }
}