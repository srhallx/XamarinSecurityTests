using System;
using MonoTouch;
using MonoTouch.UIKit;
using MonoTouch.Foundation;


namespace XamarinSecurityTests.iOS
{
	public class OpenDoc : IOpenDoc
	{
		public OpenDoc ()
		{
		}


		#region IOpenDoc implementation
		public void OpenPDF (string pdfName)
		{
			var viewer = UIDocumentInteractionController.FromUrl(NSUrl.FromFilename(pdfName));
			//viewer.PresentOpenInMenu (this.View.Frame, this.View, true);
		}
		#endregion
	}
}

