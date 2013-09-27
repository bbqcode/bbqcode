using System;
using System.Collections.Generic;
using System.Web;
using BBQCode.Web.Models;

namespace BBQCode.Web.Helpers
{
    public static class Application
    {
        private const string PROJECTS_KEY = "Projects";
        private const string MEMBERS_KEY = "Members";

        public static List<MemberModel> Members()
        {
            if (HttpContext.Current.Application[MEMBERS_KEY] == null)
            {
                var members = new List<MemberModel>();
                members.Add(new MemberModel() { FullName = "Martin Fournier", Job = "JobMartinFournier", PathImg = "av_martin_f", TextResourceName = "TextMartinFournier" });
                members.Add(new MemberModel() { FullName = "Elic Ng", Job = "JobElicNg", PathImg = "av_elic_ng", TextResourceName = "TextElicNg" });
                members.Add(new MemberModel() { FullName = "Alexis Leroux-Chartré", Job = "JobAlexisLerouxChartre", PathImg = "av_alexis_lc", TextResourceName = "TextAlexisLerouxChartre" });
                members.Add(new MemberModel() { FullName = "Samuel Paré", Job = "JobSamuelPare", PathImg = "av_samuel_p", TextResourceName = "TextSamuelPare" });
                members.Add(new MemberModel() { FullName = "Simon Godbout", Job = "JobSimonGodbout", PathImg = "av_simon_g", TextResourceName = "TextSimonGodbout" });

                HttpContext.Current.Application[MEMBERS_KEY] = members;
            }
            return HttpContext.Current.Application[MEMBERS_KEY] as List<MemberModel>;
        }

        public static Dictionary<String, ProjectModel> Projects()
        {
            if (HttpContext.Current.Application[PROJECTS_KEY] == null)
            {
                var models = new Dictionary<String, ProjectModel>();

                var questfeed = new ProjectModel();
                questfeed.Id = "questfeed";
                questfeed.Title = "Quest Feed";
                questfeed.Screenshots.Add(new ScreenshotModel() { ImagePath = "~/Content/img/projects/bbqcode_contactus.png", Height = 338, AltTextResourceName = "ContentText1" });
                questfeed.Screenshots.Add(new ScreenshotModel() { ImagePath = "~/Content/img/projects/bbqcode_coop.png", Height = 338, AltTextResourceName = "ContentText1" });
                questfeed.Screenshots.Add(new ScreenshotModel() { ImagePath = "~/Content/img/projects/bbqcode_slider.png", Height = 338, AltTextResourceName = "ContentText1" });
                questfeed.NavigationLogo = new ScreenshotModel() { ImagePath = "~/Content/img/projects/questfeed_logo.png" };
                questfeed.TextingResourceNames.Add("ContentText1");
                questfeed.TextingResourceNames.Add("ContentText1");
                models.Add(questfeed.Id, questfeed);

                var mediaservice = new ProjectModel();
                mediaservice.Id = "mediaservice";
                mediaservice.Title = "Media Service";
                mediaservice.Screenshots.Add(new ScreenshotModel() { ImagePath = "~/Content/img/projects/bbqcode_contactus.png", Height = 338, AltTextResourceName = "ContentText1" });
                mediaservice.Screenshots.Add(new ScreenshotModel() { ImagePath = "~/Content/img/projects/bbqcode_coop.png", Height = 338, AltTextResourceName = "ContentText1" });
                mediaservice.Screenshots.Add(new ScreenshotModel() { ImagePath = "~/Content/img/projects/bbqcode_slider.png", Height = 338, AltTextResourceName = "ContentText1" });
                mediaservice.NavigationLogo = new ScreenshotModel() { ImagePath = "~/Content/img/projects/mediaservice_logo.png" };
                mediaservice.TextingResourceNames.Add("ContentText1");
                mediaservice.TextingResourceNames.Add("ContentText1");
                models.Add(mediaservice.Id, mediaservice);

                var forevercenturion = new ProjectModel();
                forevercenturion.Id = "foreveracenturion";
                forevercenturion.Title = "Forever a Centurion";
                forevercenturion.Screenshots.Add(new ScreenshotModel() { ImagePath = "~/Content/img/projects/bbqcode_contactus.png", Height = 338, AltTextResourceName = "ContentText1" });
                forevercenturion.Screenshots.Add(new ScreenshotModel() { ImagePath = "~/Content/img/projects/bbqcode_coop.png", Height = 338, AltTextResourceName = "ContentText1" });
                forevercenturion.Screenshots.Add(new ScreenshotModel() { ImagePath = "~/Content/img/projects/bbqcode_slider.png", Height = 338, AltTextResourceName = "ContentText1" });
                forevercenturion.NavigationLogo = new ScreenshotModel() { ImagePath = "~/Content/img/projects/foreveracenturion_logo.png" };
                forevercenturion.TextingResourceNames.Add("ContentText1");
                forevercenturion.TextingResourceNames.Add("ContentText1");
                models.Add(forevercenturion.Id, forevercenturion);

                HttpContext.Current.Application[PROJECTS_KEY] = models;
            }
            return HttpContext.Current.Application[PROJECTS_KEY] as Dictionary<String, ProjectModel>;
        }
    }
}