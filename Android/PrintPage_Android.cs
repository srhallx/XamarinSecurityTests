using System;
using XamarinSecurityTests.Android;
using Android.Support.V4.Print;

using Android.Graphics;
using System.Threading;
using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Print;
using Android.Webkit;
using System.Runtime.CompilerServices;

[assembly: Xamarin.Forms.Dependency (typeof (PrintPage_Android))]
namespace XamarinSecurityTests.Android
{
	public class PrintPage_Android : IPrintPage
	{
		public PrintPage_Android ()
		{


		}

		#region IPrintPage implementation

		public void OpenPrinter ()
		{

			var printMgr = (PrintManager)MainActivity.ActivityInstance.GetSystemService(Context.PrintService);

			var webView = new  WebView (MainActivity.ActivityInstance);

			String summary = "<html><body><H1>Test Printing</H1></body></html>";
			webView.LoadData(summary, "text/html", null);
			printMgr.Print("Razor HMTL Hybrid", webView.CreatePrintDocumentAdapter("my test doc"), null);

		}

		#endregion
	}
}

