using System;
using XamarinSecurityTests.Android;
using Android.Support.V4.Print;
using Xamarin.Forms;
using Android.Graphics;
using System.Threading;
using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

[assembly: Dependency (typeof (PrintPage_Android))]
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
			PrintHelper photoPrinter = new PrintHelper (MainActivity.ActivityInstance);		
			photoPrinter.ScaleMode = PrintHelper.ScaleModeFit;
			var bitmap = BitmapFactory.DecodeResource (Application.Context.ApplicationContext.Resources, Resource.Drawable.bluetooth);
		
			photoPrinter.PrintBitmap ("test print", bitmap);
		}

		#endregion
	}
}

