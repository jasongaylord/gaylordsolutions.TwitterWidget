# cloudscribe.TwitterWidget
A TwitterWidget for the @cloudscribe project

The documentation for the TwitterWidget is lacking. However, the first version (0.2) can be used in your cloudscribe project. As of the time of this writing, there are a few items to manually change in your cloudscribe project:

1. Until I publish the NuGet package, you must manually add the binary as a dependency.
2. In the cloudscribe project's config folder, find the cloudscribeFeatures.cs file. Modify the SetupCloudscribeFeatures function by including the following line: `services.AddTwitterWidget(config.GetSection("TwitterOptions"));` right above the return.
3. In the cloudscribe project's config folder, find the RoutingAndMVC.cs file. Just below the `services.AddMVC()` line and right above the `.AddRazorOptions...` line, add `.AddApplicationPart(typeof(cloudscribe.TwitterWidget.Controllers.TwitterWidgetController).GetTypeInfo().Assembly)`
4. In appsettings, add new configuration at the bottom of the file that resembles the below:
`  "TwitterOptions": {
    "Username": "",
    "TwitterConsumerKey": "",
    "TwitterConsumerSecret": ""
  }`
  
You will need to enter your appropriate username, key, and secret from the Twitter developers site. I'll document more of the features and functionality shortly.

To test this, you should be able to spin up your cloudscribe project. Then, using Postman or some other tool, creat a POST request for https://localhost:44300/twitter/gettweets.
