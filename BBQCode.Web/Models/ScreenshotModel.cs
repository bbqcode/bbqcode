using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BBQCode.Web.Models
{
    public class ScreenshotModel
    {

        public string ImagePath { get; set; }
        public string AltTextResourceName { get; set; }
        public int Height { get; set; }

        /// <summary>
        /// Default is 540px
        /// </summary>
        public int Width { get; set; }

        public ScreenshotModel()
        {
            Width = 530;
        }
    }
}