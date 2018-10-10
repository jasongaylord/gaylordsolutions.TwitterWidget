using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace cloudscribe.LatestTweets.Models
{
    public class TwitterOptions
    {
        [Required]
        public string Username { get; set; }
        public int Count { get; set; } = 5;
        public int CacheMinutes { get; set; } = 5;
        public bool ShowAvatars { get; set; } = false;
        public bool ShowUsername { get; set; } = false;
        public bool ShowTimestamps { get; set; } = false;
        public bool ShowMentionsAsLinks { get; set; } = false;
        public bool ShowHashtagsAsLinks { get; set; } = false;
        public bool ShowTimestampsAsLinks { get; set; } = false;
        public string HashTagsFilter { get; set; } = "";
        public bool ShowReplies { get; set; } = false;
        [Required]
        public string TwitterConsumerKey { get; set; } = "";
        [Required]
        public string TwitterConsumerSecret { get; set; } = "";
    }
}
