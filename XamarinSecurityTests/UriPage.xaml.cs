using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace XamarinSecurityTests
{
	public partial class UriPage : ContentPage
	{
		public UriPage ()
		{
			InitializeComponent ();
		
		}

		void PickerChanged(object sender, EventArgs e)
		{
			switch (UriPicker.Items [UriPicker.SelectedIndex]) {
			case "Facetime":
				UriText.Text = "facetime://5555555555";
				break;
			case "SMS":
				UriText.Text = "sms://5555555555";
				break;
			case "Web":
				UriText.Text = "http://www.xamarin.com";
				break;
			case "Maps":
				UriText.Text = "maps://";
				break;
			case "Phone":
				UriText.Text = "tel:5555555555";
				break;
			case "Mail":
				UriText.Text = "mailto:me@fubar.com";
				break;
			case "Music":
				UriText.Text = "music:";
				break;
			case "YouTube":
				UriText.Text = "http://www.youtube.com";
				break;
			case "Facebook":
				UriText.Text = "fb:";
				break;
			case "Twitter":
				UriText.Text = "twitter:";
				break;
			}
		}

		void OpenUri(object sender, EventArgs e)
		{
			Device.OpenUri (new Uri(UriText.Text));
		}
	}
}

