using System;
using System.Drawing;
using MonoTouch;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XamarinSecurityTests.iOS;


// This ExportRenderer command tells Xamarin.Forms to use this renderer
// instead of the built-in one for this page
[assembly:ExportRenderer(typeof(XamarinSecurityTests.OpenDocPage), typeof(XamarinSecurityTests.OpenDocPageRenderer))]

namespace XamarinSecurityTests
{
	/// <summary>
	/// Render this page using platform-specific UIKit controls
	/// </summary>
	public class OpenDocPageRenderer : PageRenderer
	{
		UIView myView;

		protected override void OnElementChanged (VisualElementChangedEventArgs e)
		{
			base.OnElementChanged (e);

			var hostViewController = ViewController;

			OpenDocViewController viewController = new OpenDocViewController ();

			hostViewController.AddChildViewController (viewController);
			hostViewController.View.Add (viewController.View);

			viewController.DidMoveToParentViewController (hostViewController);

		}
			
	}
}

