# gaylordsolutions.TwitterWidget
A TwitterWidget for the @cloudscribe project

The documentation for the TwitterWidget is lacking. However, the first version can be used in your cloudscribe project. As of the time of this writing, there are two items to manually change in your cloudscribe project:

1. In the cloudscribe project's config folder, find the cloudscribeFeatures.cs file. Modify the SetupCloudscribeFeatures function by including the following line: `services.AddTwitterWidget(config.GetSection("TwitterOptions"));` right above the return.
2. In appsettings, add new configuration at the bottom of the file that resembles the below:
`  "TwitterOptions": {
    "Username": "",
    "TwitterConsumerKey": "",
    "TwitterConsumerSecret": ""
  }`
  
You will need to enter your appropriate username, key, and secret from the Twitter developers site. I'll document more of the features and functionality shortly.

To test this, you should be able to spin up your cloudscribe project. Then, using Postman or some other tool, creat a POST request for https://localhost:44300/twitter/gettweets.

## Comsuming the gettweets Response
You can consume the JSON results above or use the ViewComponent. If you choose the ViewComponent, you'll need to include a Default.cshtml view in your theme's Views\Shared\Components\RetrieveTweets folder. This project includes a sample to be used. The compiled view is not available in the cloudscribe project quite yet due to the way the cloudscribe middleware injects its own views.
