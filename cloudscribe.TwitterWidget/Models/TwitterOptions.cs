using System.ComponentModel.DataAnnotations;

namespace cloudscribe.TwitterWidget.Models
{
    public class TwitterOptions
    {
        [Required]
        public string Username { get; set; } = "cloudscribeweb";
        public int Count { get; set; } = 5;
        public int CacheMinutes { get; set; } = 5;
        public bool ShowAvatars { get; set; } = false;
        public bool ShowUsername { get; set; } = true;
        public bool ShowTimestamps { get; set; } = true;
        public bool ShowMentionsAsLinks { get; set; } = true;
        public bool ShowHashtagsAsLinks { get; set; } = true;
        public bool ShowTimestampsAsLinks { get; set; } = true;
        public string HashTagsFilter { get; set; } = "";
        public bool ShowReplies { get; set; } = false;
        [Required]
        public string TwitterConsumerKey { get; set; } = "";
        [Required]
        public string TwitterConsumerSecret { get; set; } = "";
    }
}
