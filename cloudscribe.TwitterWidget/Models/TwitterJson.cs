using System;
using System.Collections.Generic;
using System.Text;

namespace cloudscribe.TwitterWidget.Models
{
    public class TwitterJson
    {
        public string id_str { get; set; }
        public string text { get; set; }
        public string created_at { get; set; }
        public string in_reply_to_screen_name { get; set; }
        public UserJson user { get; set; }
    }
}
