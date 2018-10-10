using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
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
