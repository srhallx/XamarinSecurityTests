using System;
using MonoTouch.UIKit;
using Xamarin.Forms;
using XamarinSecurityTests.iOS;


[assembly: Dependency (typeof (PrintPage_iOS))]
namespace XamarinSecurityTests.iOS
{
	public class PrintPage_iOS : IPrintPage
	{
		

		public void OpenPrinter()
		{
			var printInfo = UIPrintInfo.PrintInfo;
			printInfo.OutputType = UIPrintInfoOutputType.General;
			printInfo.JobName = "My first Print Job";

			var textFormatter = new UISimpleTextPrintFormatter ("Once upon a time...") {
				StartPage = 0,
				ContentInsets = new UIEdgeInsets (72, 72, 72, 72),
				MaximumContentWidth = 6 * 72,
			};

			var printer = UIPrintInteractionController.SharedPrintController;
			printer.PrintInfo = printInfo;
			printer.PrintFormatter = textFormatter;
			printer.ShowsPageRange = true;
			printer.Present (true, (handler, completed, err) => {
				if (!completed && err != null) {
					Console.WriteLine ("error");
				}
			});
		}

	}
}

