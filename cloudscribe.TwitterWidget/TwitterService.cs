using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using cloudscribe.TwitterWidget.Extensions;
using cloudscribe.TwitterWidget.Models;
using Newtonsoft.Json;

namespace cloudscribe.TwitterWidget
{
    public class TwitterService : ITwitterService
    {
        public async Task<List<TweetStruct>> RetrieveTweets(TwitterOptions options, string key)
        {
            var tweetList = new List<TweetStruct>();

            if (!string.IsNullOrEmpty(options.Username) && !string.IsNullOrEmpty(options.TwitterConsumerKey) && !string.IsNullOrEmpty(options.TwitterConsumerSecret))
            {
                var bearerToken = ObtainBearerToken(options.TwitterConsumerKey, options.TwitterConsumerSecret);

                var timelineUrl = string.Format("https://api.twitter.com/1.1/statuses/user_timeline.json?exclude_replies={0}&screen_name={1}", !options.ShowReplies ? "true" : "false", Uri.EscapeDataString(options.Username));
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(timelineUrl);
                request.Headers.Add("Authorization", "Bearer " + bearerToken);
                request.Method = "GET";
                request.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";
                request.AutomaticDecompression = DecompressionMethods.GZip;

                WebResponse response = await request.GetResponseAsync();
                string responseData = await new StreamReader(response.GetResponseStream()).ReadToEndAsync();

                var fromJsonArray = JsonConvert.DeserializeObject<IEnumerable<TwitterJson>>(responseData);

                string[] hashtagFilter = null;
                if (!string.IsNullOrEmpty(options.HashTagsFilter))
                {
                    hashtagFilter = options.HashTagsFilter.Split(',');
                }

                var tweets = from tweet in fromJsonArray
                             where
                                // Hashtags
                                (hashtagFilter == null || (hashtagFilter != null && hashtagFilter.Any(p => tweet.text.Contains(p))))
                                &&
                                // Show replies
                                (options.ShowReplies || String.IsNullOrEmpty(tweet.in_reply_to_screen_name))
                             select new TweetStruct()
                             {
                                 Message = tweet.text,
                                 Username = tweet.user.screen_name,
                                 Avatar = tweet.user.profile_image_url,
                                 Timestamp = (DateTime.ParseExact(tweet.created_at, "ddd MMM dd HH:mm:ss %zzzz yyyy", CultureInfo.InvariantCulture, DateTimeStyles.AssumeUniversal | DateTimeStyles.AdjustToUniversal)).ToFriendlyDate(),
                                 Id = tweet.user.id_str
                             };

                tweetList.AddRange(tweets.Take(options.Count > 0 ? options.Count : 10));
            }

            return tweetList;
        }

        public string ObtainBearerToken(string key, string secret)
        {
            ServicePointManager.Expect100Continue = false;

            var applicationAuthorization = Convert.ToBase64String(Encoding.UTF8.GetBytes(string.Format("{0}:{1}", Uri.EscapeDataString(key), Uri.EscapeDataString(secret))));

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://api.twitter.com/oauth2/token");
            request.Headers.Add("Authorization", "Basic " + applicationAuthorization);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded;charset=UTF-8";
            request.AutomaticDecompression = DecompressionMethods.GZip;
            using (StreamWriter writer = new StreamWriter(request.GetRequestStream()))
            {
                writer.Write("grant_type=client_credentials");
            }

            WebResponse response = request.GetResponse();
            var token = "";
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                token = reader.ReadToEnd();
            }

            return JsonConvert.DeserializeObject<TokenJson>(token).access_token;
        }
    }
}