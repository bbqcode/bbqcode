using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Reflection;
using System.Xml.Linq;

namespace BBQCode.Web.Helpers
{
    public static class Extensions
    {
        private const string ResourcesReflectionFormat = "BBQCode.Resources.{0},BBQCode.Resources";

        public static string Localized(this HtmlHelper html, string resourceName)
        {
            string resourceValue = null;
            var type = Type.GetType(String.Format(ResourcesReflectionFormat, html.ActionName()), false, true);
            // First try to get the resource value within a type the same name as the view.
            if (type != null)
            {
                var property = type.GetProperty(resourceName);
                if (property != null) { resourceValue = (string)property.GetValue(null, null); }
            }
            // If still has not found value, try to fetch from shared resource file.
            if (resourceValue == null)
            {
                var property = typeof(BBQCode.Resources.Shared).GetProperty(resourceName);
                if (property != null) { resourceValue = (string)property.GetValue(null, null); }
            }
            
            if (resourceValue == null) throw new Exception(String.Format("Could not find resource with a name of {0} in either {1} or Shared ResX", resourceName, html.ActionName()));
            return resourceValue;
        }

        public static MvcHtmlString LocalizedTitle(this HtmlHelper html, string resourceName, bool invert = false)
        {
            var resourceValue = Localized(html, resourceName);
            return html.FirstWordTitle(resourceValue, invert);
        }

        public static MvcHtmlString FirstWordTitle(this HtmlHelper html, string text, bool invert = false)
        {
            var word = text.Split(' ');
            if (invert)
            {
                word[word.Length - 1] = "<span class='firstWordTitle'>" + word[word.Length - 1] + "</span>";
            }
            else
            {
                word[0] = "<span class='firstWordTitle'>" + word[0] + "</span>";
            }
            return new MvcHtmlString(string.Join(" ", word));
        }

        public static IHtmlString RawLocalized(this HtmlHelper html, string resourceName)
        {
            return html.Raw(html.Localized(resourceName));
        }

        public static MvcHtmlString CultureLink(this HtmlHelper html, string culture, string resourceName)
        {
            var linkText = html.Localized(resourceName);
            var currentCulture = html.CultureName();
            if (String.Equals(culture, currentCulture, StringComparison.OrdinalIgnoreCase))
            {
                return html.ActionLink(linkText, html.ActionName(), new { culture = culture, id = html.CurrentId() }, new { @class = "current" });
            }
            else
            {
                return html.ActionLink(linkText, html.ActionName(), new { culture = culture, id = html.CurrentId() });
            }
        }

        public static MvcHtmlString LocalizedLink(this HtmlHelper html, string action, string resourceName, bool isRoot = false)
        {
            var resourceValue = html.Localized(resourceName);
            var currentId = "";
            if (String.Equals(action, "project", StringComparison.OrdinalIgnoreCase)) currentId = html.CurrentId();
            var currentAction = html.ActionName();
            if (currentAction == "project")
                currentAction = "projects";
            if (String.Equals(action, currentAction, StringComparison.OrdinalIgnoreCase) || (String.Equals("Index", currentAction, StringComparison.OrdinalIgnoreCase) && isRoot))
            {
                return html.ActionLink(resourceValue, action, new { culture = html.CultureName(), id = currentId}, new { @class = "current" });
            }
            else
            {
                return html.ActionLink(resourceValue, action, new { culture = html.CultureName(), id = currentId });
            }
        }

        public static String LocalizedProjectLink(this HtmlHelper html, string project)
        {
            return "/"+html.CultureName()+"/project/"+project;
        }

        public static MvcHtmlString LocalizedLink(this HtmlHelper html, string action, string resourceName, string anchor)
        {
            var resourceValue = html.Localized(resourceName);
            return html.ActionLink(resourceValue, action, "Home", null, null, anchor, new { culture = html.CultureName() }, null);
        }

        public static MvcHtmlString TwitterFeed(this HtmlHelper html, int maxTweet)
        {
            var doc = Twitter.Feed;
            var rssFeed = from el in doc.Elements("rss").Elements("channel").Elements("item")
                          select new
                          {
                              Title = el.Element("title").Value,
                              Link = el.Element("link").Value,
                              Description = el.Element("description").Value,
                              Date = el.Element("pubDate").Value
                          };

            var hs = "";

            foreach (var feed in rssFeed.Take(maxTweet))
            {
                var date = DateTime.Parse(feed.Date);
                var tweet = feed.Title.Remove(0, feed.Title.IndexOf(' '));
                hs += String.Format("<a class='tweetLink' href='{0}' target='_blank'><div class='tweetWrapper'><div class='tweetDate'>{1}</div><div class='tweet'>{2}</div><div class='clear'></div></div></a>", feed.Link, date.ToString("MMM"), tweet);
            }
            return new MvcHtmlString(hs);
        }

        public static string ActionName(this HtmlHelper html)
        {
            return html.ViewContext.RouteData.GetRequiredString("action");
        }

        public static string CultureName(this HtmlHelper html)
        {
            return System.Threading.Thread.CurrentThread.CurrentUICulture.TwoLetterISOLanguageName;
        }

        public static string CurrentId(this HtmlHelper html)
        {
            var o = html.ViewContext.RouteData.Values["id"];
            if (o == null) return "";
            return o.ToString();
        }


        public static bool IsDebug(this HtmlHelper html)
        {
            return HttpContext.Current.IsDebuggingEnabled;
        }
    }
}