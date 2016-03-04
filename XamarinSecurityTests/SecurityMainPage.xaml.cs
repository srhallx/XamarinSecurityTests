using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using Plugin.Geolocator;
using Xamarin.Forms;
using ModernHttpClient;
using Plugin.Geolocator.Abstractions;

namespace XamarinSecurityTests
{
	public partial class SecurityMainPage : ContentPage
	{
		public static IGeolocator GpsPositionLocator { get { return CrossGeolocator.Current; }}

		public SecurityMainPage ()
		{
			InitializeComponent ();

		}

//		- Ability to use FaceTime (or Video chat) - Complete
//		- Ability to use Instant Messaging. - Complete
//		- Print & Bluetooth button were disabled on both containerized & non-containerized app. Enable it.
//		- Option to pull contact from device - Complete
//		- Option to save contact to device
//		- To send email using the configured native email - Complete
//			- To save clicked photo into Device Gallery.
//			- Improve Share button rendering. Currently it lists the app however the doesn't appear correctly on viewable area. Tried device rotation as well [refer screenshot]. - Works for me
//

		// Event handlers for buttons
		void SqlClicked(object sender, EventArgs e)
		{
			Output.Text = "";
			DataOperations dao = new DataOperations();

			dao.MobileOpsEvent += (object o, MobileOpsEventArgs evt) => {
				Output.Text += evt.Message + "\n";
			};

			dao.CheckAndCreateDatabase("testdb.db3");
			dao.QueryDatabase("testdb.db3");
			dao.DeleteRows("testdb.db3");
		}

		void FilesClicked(object sender, EventArgs e){
			this.Navigation.PushAsync (new FileOperationsPage ());
		}

		void InternalWSClicked(object sender, EventArgs e)
		{
			
			this.Navigation.PushAsync(new InternalWSPage());
		}

		void CameraClicked(object sender, EventArgs e)
		{
			try {
				DependencyService.Get<ICameraPage>().OpenCamera();
			} catch (Exception ex)
			{
				Output.Text = ex.Message;
			}
		}

		void WebClicked(object sender, EventArgs e)
		{

			this.Navigation.PushAsync(new WebViewer());
		}

		void PrintClicked(object sender, EventArgs e)
		{
			DependencyService.Get<IPrintPage>().OpenPrinter();
		}

		void ShareClicked(object sender, EventArgs e)
		{
			this.Navigation.PushAsync(new OpenDocPage());
		}

		void UriClicked(object sender, EventArgs e)
		{
			this.Navigation.PushAsync (new UriPage ());
		}

		void ContactsClicked(object sender, EventArgs e)
		{
			this.Navigation.PushAsync (new ContactsPage ());
		}

		void BluetoothClicked(object sender, EventArgs e)
		{
			this.Navigation.PushAsync(new BTConnectPage());
		}

		async void GpsToggled(object sender, EventArgs evt)
		{
			
			if (!GpsSwitch.IsToggled) {
				await GpsPositionLocator.StopListeningAsync();
				return;
			}

			await GpsPositionLocator.StartListeningAsync(500, 5);

			GpsPositionLocator.GetPositionAsync (10000).ContinueWith (t => {
				Console.WriteLine ("Position Status: {0}", t.Result.Timestamp);
				Console.WriteLine ("Position Latitude: {0}", t.Result.Latitude);
				Console.WriteLine ("Position Longitude: {0}", t.Result.Longitude);
			}, TaskScheduler.FromCurrentSynchronizationContext());

			GpsPositionLocator.PositionChanged += (object sender1, PositionEventArgs e) => {
				Output.Text = String.Format("GPS: lat {0} lon {1}", e.Position.Latitude.ToString(), e.Position.Longitude.ToString()) + "\n";
			};
		}

		void AudioClicked(object sender, EventArgs e)
		{
			this.Navigation.PushAsync(new OpenAudioPage());
		}

		void WebServiceClicked(object sender, EventArgs e)
		{
			var httpClient = new HttpClient(new NativeMessageHandler());

			//api key is limited to 1000 requests/day
			Task<HttpResponseMessage> getResponse2 = httpClient.GetAsync("https://api.forecast.io/forecast/f0fc68cd396162493bc12640cdbfdde0/37.8267,-122.423");
			HttpResponseMessage msg2 = getResponse2.Result;
			Task<string> finalMsg2 = msg2.Content.ReadAsStringAsync();
			Output.Text += "\nHTTPS\n" + finalMsg2.Result.Substring(0,300) + "...\n";
		}


	}
}

