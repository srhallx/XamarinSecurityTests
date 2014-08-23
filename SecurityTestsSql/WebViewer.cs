using System;
using Xamarin.Forms;

namespace SecurityTestsSql
{
	public class WebViewer : ContentPage
	{


		public WebViewer ()
		{

			Title = "Web Viewer";

			Entry urlBar = new Entry {
				HorizontalOptions = LayoutOptions.FillAndExpand,
				Text = "http://www.google.com"
			};

			WebView viewer = new WebView {
				VerticalOptions = LayoutOptions.FillAndExpand,
				Source = new UrlWebViewSource { Url = urlBar.Text }
			};

			Button goButton = new Button {
				Text = "Go"
			};
				
			goButton.Clicked += (object sender, EventArgs e) => {
				string url = urlBar.Text;
				if (!url.StartsWith("http://"))
					url = "http://" + url;

				viewer.Source = new UrlWebViewSource { Url = url };
			};

			this.Content =
				new StackLayout {
					Children = {
					new StackLayout {
						Orientation = StackOrientation.Horizontal,
						Children = {
							urlBar,
							goButton
						}
					},
						viewer
					},
				Orientation = StackOrientation.Vertical,
				Padding = new Thickness (10, Device.OnPlatform (20, 0, 0), 10, 5)
			};
		}
	}
}

