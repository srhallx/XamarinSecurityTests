using System;
using Xamarin.Forms;

namespace XamarinSecurityTests
{
	public class OpenAudioPage : ContentPage
	{
		public OpenAudioPage ()
		{


			Button btnRecord = new Button { Text = "1 - Record" };
			Button btnStop = new Button { Text = "2 - Stop Recording", IsEnabled = false };
			Button btnPlay = new Button { Text = "3 - Play", IsEnabled = false };

			StackLayout myLayout = new StackLayout { Padding = new Thickness (20, 80, 20, 20),
				Spacing = 60,
				Children = { btnRecord, btnStop, btnPlay }
			};

			Content = myLayout;

			btnRecord.Clicked += (object sender, EventArgs e) => {
				DependencyService.Get<IMicPage>().StartRecording();
				btnStop.IsEnabled = true;
				btnPlay.IsEnabled = true;
			};

			btnStop.Clicked += (object sender, EventArgs e) => {
				DependencyService.Get<IMicPage>().StopRecording();
			};

			btnPlay.Clicked += (object sender, EventArgs e) => {
				DependencyService.Get<IMicPage>().PlayRecording();
			};
		}
	}
}

