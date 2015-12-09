using System;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin;
using Xamarin.Forms.Platform.Android;
using Xamarin.Contacts;
using Xamarin.Geolocation;

namespace XamarinSecurityTests.Android
{
	[Activity (Label = "XamarinSecurityTests.Android.Android", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : FormsApplicationActivity
	{
		public static Activity ActivityInstance;

		protected override void OnCreate (Bundle bundle)
		{
			Xamarin.Insights.Initialize (XamarinInsights.ApiKey, this);
			base.OnCreate (bundle);
			ActivityInstance = this;
			Insights.Initialize("48e4e4b9b56bb7789f883b4b9aeeb3f1f2a55836", this);
			App.ContactsBook = new Xamarin.Contacts.AddressBook (this);
			Xamarin.Forms.Forms.Init (this, bundle);
			SetPage (App.GetMainPage ());
		}
	}
}
