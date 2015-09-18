# Getting Started with Xamarin.Insights

## Referencing Xamarin.Insights components in your solution

If you had acquired Xamarin.Insights components through the Xamarin component store interface from within your IDE, then after adding the components to your Xamarin.iOS, Xamarin.Android and Windows Phone projects through the Component manager, you will still need to manually reference the PCL (Portable Class Library) assemblies in the Xamarin.Forms PCL project in your solution. You can do this by manually adding the relevant PCL assembly references to your PCL project contained in the following path inside of your solution folder

Components/xamarin-insights-version/lib/pcl/


## API

### Initialize 

```
using Xamarin;
Insights.Initialize("Your API key");
```

If you are using Android

```
using Xamarin;
Insights.Initialize("Your API key", yourAppContext);
```

the Initialize call should happen as soon as possible, ideally at app start-up.

### Reporting

```
using Xamarin;
Insights.Report(exception);
```

This API should be used to report exceptions that you have caught in a try{}catch{} statement that you feel is worth sending to Insights. 
In addition you can add extra data to exceptions which will be reported back to Insights

```
using Xamarin;
Insights.Report(exception, new Dictionary<string, string> { 
	{"Some additional info", "foobar"}
});
```

If you just wish to add additional information to an exception but still throw the exception, you can use the Exception.Data property.

```
try {
	ExceptionThrowingFunction();
} 
catch (Exception exception) {
	exception.Data["This is some extra data"] = "A cat's field of vision is about 200 degrees."
	throw exception;
}
```

### Identify

Identify is used to identify information about your users

```
using Xamarin;
Insights.Identify("YourUsersUniqueId", "Email", "njpatel@catfacts.com");
var manyInfos = new Dictionary<string, string> {
	{"Email", "njpatel@catfacts.com"},
	{"CatTeethFact", "Cats have 30 teeth (12 incisors, 10 premolars, 4 canines, and 4 molars), while dogs have 42. Kittens have baby teeth, which are replaced by permanent teeth around the age of 7 months."}
}
Insights.Identify("YourUsersUniqueId", manyInfos);
```

### Track
Track is used to track the various comings and goings of your app, you can track whatever you like. Track comes in two variations, first of all the event style track.

```
using Xamarin;
Insights.Track("MusicTrackPlayed", new Dictionary<string, string> { 
	{"TrackID", "E1D8AB93"}, 
	{"Length", "183"} 
});
```

You can also use Track to track timed events, for example

```
using Xamarin;
using (var handle = Insights.TrackTime("TimeToLogin")) {
	await SubmitLoginInformation("myuserid", "mypassword");
	// ... more code goes here ...
}

// or if you do not wish to use the using syntax
var handle = Insights.TrackTime("TimeToLogin");
handle.Start();
await SubmitLoginInformation("myuserid", "mypassword");
// ... more code goes here ...
handle.Stop();
```