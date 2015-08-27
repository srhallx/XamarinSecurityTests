using System;
using System.Collections.Generic;
using System.Linq;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Xamarin;
using Xamarin.Forms;
using Xamarin.Contacts;


namespace XamarinSecurityTests.iOS
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		UIWindow window;

		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			Forms.Init ();

			//Insights.Initialize("48e4e4b9b56bb7789f883b4b9aeeb3f1f2a55836");

			App.ContactsBook = new Xamarin.Contacts.AddressBook ();

			window = new UIWindow (UIScreen.MainScreen.Bounds);
			
			window.RootViewController = App.GetMainPage ().CreateViewController ();
			window.MakeKeyAndVisible ();
			
			return true;
		}
	}
}

