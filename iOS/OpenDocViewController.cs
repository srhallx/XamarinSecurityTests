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

			UILabel label = new UILabel (new RectangleF (0, 100, 320, 150));
			label.Text = "If share is enabled, you should see the Open In dialog on top of this window.  ";
			label.Lines = 4;

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

