using System;
using Xamarin.Forms;

namespace XamarinSecurityTests
{
	public class OpenAudioPage : ContentPage
	{
		public OpenAudioPage ()
		{

			Title = "Audio";

			Button btnRecord = new Button { Text = "Record" };
			Button btnStop = new Button { Text = "Stop Recording", IsEnabled = false };
			Button btnPlay = new Button { Text = "Play", IsEnabled = false };
			ActivityIndicator aiWorking = new ActivityIndicator () { IsVisible = false };

			StackLayout myLayout = new StackLayout { Padding = new Thickness (20, 80, 20, 20),
				Spacing = 40,
				Children = { btnRecord, btnStop, btnPlay, aiWorking }
			};

			Content = myLayout;

			btnRecord.Clicked += (object sender, EventArgs e) => {
				DependencyService.Get<IMicPage>().StartRecording();
				btnStop.IsEnabled = true;
				btnRecord.IsEnabled = false;
				btnPlay.IsEnabled = false;
				aiWorking.IsVisible = true;
				aiWorking.IsRunning = true;
			};

			btnStop.Clicked += (object sender, EventArgs e) => {
				DependencyService.Get<IMicPage>().StopRecording();
				btnPlay.IsEnabled = true;
				btnStop.IsEnabled = false;
				btnRecord.IsEnabled = true;
				aiWorking.IsRunning = false;
				aiWorking.IsVisible = false;
			};

			btnPlay.Clicked += (object sender, EventArgs e) => {
				DependencyService.Get<IMicPage>().PlayRecording();
			};
		}
	}
}

