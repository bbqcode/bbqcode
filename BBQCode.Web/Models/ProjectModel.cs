using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BBQCode.Web.Models
{
    public class ProjectModel
    {
        public List<ScreenshotModel> Screenshots { get; set; }
        public ScreenshotModel NavigationLogo { get; set; }
        public ScreenshotModel NavigationLogoAlt { get; set; }
        public string Title { get; set; }
        public string Id { get; set; }

        public List<string> TextingResourceNames { get; set; }

        public ProjectModel()
        {
            Screenshots = new List<ScreenshotModel>();
            TextingResourceNames = new List<string>();
        }
    }
}