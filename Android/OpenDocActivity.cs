using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Android.App;
using Android.Content;
using Android.Net;
using Android.Content.PM;
using Android.Database;
using Android.Graphics;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Mono;
using Android.Net.Http;


[assembly: UsesPermission(Android.Manifest.Permission.Internet)]
namespace XamarinSecurityTests.Android
{
	[Activity (Label = "OpenDocActivity")]			
	public class OpenDocActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.OpenDocRenderer);

			var button = FindViewById<Button> (Resource.Id.myButton);

			button.Click += (sender, e) => {
//				var printMgr = (Android.Print.PrintManager)GetSystemService(Context.PrintService);
//				printMgr.Print("Razor HMTL Hybrid", webView.CreatePrintDocumentAdapter(), null);

				Finish(); // back to the previous activity
			};
				

			var pdfFile = new Java.IO.File ("gettingstarted.pdf");
			var filepath = global::Android.Net.Uri.FromFile(pdfFile);

			if (!pdfFile.Exists ())
				Console.WriteLine ("Could not find gettingstarted.pdf");
			
			Intent intent = new Intent (Intent.ActionView);
			intent.SetDataAndType(filepath, "application/pdf");

			try
			{
				StartActivity (intent);
			}
			catch (Exception ex) 
			{
				Console.WriteLine ("COULD NOT START PDF ACTIVITY " + ex.Message);
			}

		}
	}
}

