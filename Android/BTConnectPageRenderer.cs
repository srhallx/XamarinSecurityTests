using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;


[assembly:ExportRenderer(typeof(XamarinSecurityTests.BTConnectPage), typeof(XamarinSecurityTests.BTConnectPageRenderer))]
namespace XamarinSecurityTests
{
	public class BTConnectPageRenderer : PageRenderer
	{
		protected override void OnElementChanged (ElementChangedEventArgs<Page> e)
		{
			base.OnElementChanged (e);

			// this is a ViewGroup - so should be able to load an AXML file and FindView<>
			var activity = this.Context as Activity;

			var docActivity = new Intent (activity, typeof (XamarinSecurityTests.Android.BTConnectActivity));
			activity.StartActivity (docActivity);

		}
	}
}

