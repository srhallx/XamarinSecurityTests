using System;
using Xamarin.Forms;
using Xamarin.Media;
using Xamarin.Geolocation;
using System.Net.Http;
using ModernHttpClient;
using System.Threading.Tasks;

namespace XamarinSecurityTests
{
	public class App
	{
		public static Geolocator GpsPositionLocator { get; set; }


		public static Page GetMainPage ()
		{	
			ContentPage myPage = new ContentPage {
				Title = "Xamarin Test Suite"
			};


			//Setup functional buttons
			Button excerciseSQL = new Button {
				Text = "SQL",
				Image = "database.png"
			};		

			Button camera = new Button {
				Text = "Cam",
				Image = "camera.png"
			};

			Button web = new Button {
				Text = "Web",
				Image = "earth.png"
			};

			Button geo = new Button {
				Text = "Geo On",
				Image = "compass.png"
			};

			Button ws = new Button {
				Text = "WS",
				Image = "ws.png"
			};

			Button bluetooth = new Button {
				Text = "BT",
				Image = "bluetooth.png", IsEnabled = false
			};

			Button print = new Button {
				Text = "Print",
				Image = "print.png",
				IsEnabled = false
			};

			Button share = new Button {
				Text = "Share",
				Image = "share.png"
			};

			Button audio = new Button {
				Text = "Audio",
				Image = "audio.png"
			};

			Button internalWS = new Button {
				Text = "InternalWS"
			};

			Editor results = new Editor () {
				VerticalOptions = LayoutOptions.FillAndExpand
			};


			// Event handlers for buttons
			excerciseSQL.Clicked += (object sender, EventArgs e) => {
				results.Text = "";
				DataOperations dao = new DataOperations();

				dao.MobileOpsEvent += (object o, MobileOpsEventArgs evt) => {
					results.Text += evt.Message + "\n";
				};

				dao.CheckAndCreateDatabase("testdb.db3");
				dao.QueryDatabase("testdb.db3");
				dao.DeleteRows("testdb.db3");
			};

			internalWS.Clicked += (object sender, EventArgs e) => {
				myPage.Navigation.PushAsync(new InternalWSPage());
			};

			camera.Clicked += (object sender, EventArgs e) => {
				DependencyService.Get<ICameraPage>().OpenCamera();
			};

			web.Clicked += (object sender, EventArgs e) => {

				myPage.Navigation.PushAsync(new WebViewer());
			};

			print.Clicked += (object sender, EventArgs e) => {
				DependencyService.Get<IPrintPage>().OpenPrinter();
			};

			share.Clicked += (object sender, EventArgs e) => {
				myPage.Navigation.PushAsync(new OpenDocPage());
			};

			bluetooth.Clicked += (object sender, EventArgs e) => {
				myPage.Navigation.PushAsync(new BTConnectPage());
			};
				
			bool geoEnabled = false;
		
			geo.Clicked += (object sender, EventArgs e1) => {

				if (geoEnabled) {
					GpsPositionLocator.StopListening();
					geoEnabled = false;
					geo.Text = "Geo On";
					GpsPositionLocator = null;
					return;
				}

				DependencyService.Get<ICameraPage>().OpenGPS();
				GpsPositionLocator.StartListening(500, 5);
				geoEnabled = true;
				geo.Text = "Geo Off";

				GpsPositionLocator.GetPositionAsync (timeout: 10000).ContinueWith (t => {
					Console.WriteLine ("Position Status: {0}", t.Result.Timestamp);
					Console.WriteLine ("Position Latitude: {0}", t.Result.Latitude);
					Console.WriteLine ("Position Longitude: {0}", t.Result.Longitude);
				}, TaskScheduler.FromCurrentSynchronizationContext());
					
				GpsPositionLocator.PositionChanged += (object sender1, PositionEventArgs e) => {
					results.Text = String.Format("GPS: lat {0} lon {1}", e.Position.Latitude.ToString(), e.Position.Longitude.ToString()) + "\n";
				};
			};

			audio.Clicked += (object sender, EventArgs e) => {
				myPage.Navigation.PushAsync(new OpenAudioPage());
			};

			ws.Clicked += (object sender, EventArgs e) => {
				var httpClient = new HttpClient(new NativeMessageHandler());

				Task<HttpResponseMessage> getResponse = httpClient.GetAsync("http://wsf.cdyne.com/WeatherWS/Weather.asmx/GetCityForecastByZIP?ZIP=76092");
				HttpResponseMessage msg = getResponse.Result;
				Task<string> finalMsg = msg.Content.ReadAsStringAsync();
				results.Text = "HTTP\n" + finalMsg.Result.Substring(0, 300) + "...\n";

				//api key is limited to 1000 requests/day
				Task<HttpResponseMessage> getResponse2 = httpClient.GetAsync("https://api.forecast.io/forecast/f0fc68cd396162493bc12640cdbfdde0/37.8267,-122.423");
				HttpResponseMessage msg2 = getResponse2.Result;
				Task<string> finalMsg2 = msg2.Content.ReadAsStringAsync();
				results.Text += "\nHTTPS\n" + finalMsg2.Result.Substring(0,300) + "...\n";
			};

			Grid buttonGrid = new Grid {
				RowDefinitions = {
					new RowDefinition {Height = GridLength.Auto},
					new RowDefinition {Height = GridLength.Auto},
					new RowDefinition {Height = GridLength.Auto},
					new RowDefinition {Height = GridLength.Auto},
					new RowDefinition {Height = GridLength.Auto}
				},
				ColumnDefinitions = {
					new ColumnDefinition {Width = new GridLength(1, GridUnitType.Star)},
					new ColumnDefinition {Width = new GridLength(1, GridUnitType.Star)}
				}
			};

			//Add buttons to grid layout
			buttonGrid.Children.Add (excerciseSQL, 0, 0);
			buttonGrid.Children.Add (camera, 0, 1);
			buttonGrid.Children.Add (web, 0, 2);
			buttonGrid.Children.Add (geo, 0, 3);
			buttonGrid.Children.Add (audio, 0, 4);
			buttonGrid.Children.Add (ws, 1, 0);
			buttonGrid.Children.Add (bluetooth, 1, 1);
			buttonGrid.Children.Add (share, 1, 2);
			buttonGrid.Children.Add (print, 1, 3);
			buttonGrid.Children.Add (internalWS, 1, 4);



			myPage.Content = new StackLayout {
				Children = { 
					buttonGrid,
					results
				},
				Padding = new Thickness (10, Device.OnPlatform (20, 0, 0), 10, 5)
			};

			return new NavigationPage (myPage);
		}



	}
}

