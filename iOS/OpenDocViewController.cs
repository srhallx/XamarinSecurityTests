using System;
using MonoTouch;
using MonoTouch.UIKit;
using MonoTouch.Foundation;
using System.Drawing;

namespace XamarinSecurityTests.iOS
{
	public class OpenDocViewController : UIViewController
	{
		public OpenDocViewController ()
		{
			var myView = new UIView (new RectangleF (0, 0, 320, 480));
			this.View.Add (myView);

			UILabel label = new UILabel (new RectangleF (0, 100, 320, 50));
			label.Text = "You must have a PDF app installed.";

			myView.Add (label);

		}

		public override void ViewDidAppear (bool animated)
		{
			base.ViewDidAppear (animated);

			var viewer = UIDocumentInteractionController.FromUrl(NSUrl.FromFilename("gettingstarted.pdf"));
			viewer.PresentOpenInMenu (this.View.Frame, this.View, true);
		}
	}
}

