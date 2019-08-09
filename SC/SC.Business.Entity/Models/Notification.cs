
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace SC.Business.Entity.Models
{
    public class Notification
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Time { get; set; }

        public string TimeRange { get; set; }

        public string Url { get; set; }

        public string InfoLevel { get; set; }
    }
}