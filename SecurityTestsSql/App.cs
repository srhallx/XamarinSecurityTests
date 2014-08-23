using System;
using Xamarin.Forms;
using Xamarin.Media;
using Xamarin.Geolocation;
using System.Net.Http;
using ModernHttpClient;
using System.Threading.Tasks;

namespace SecurityTestsSql
{
	public class App
	{
		public static Page GetMainPage ()
		{	
			ContentPage myPage = new ContentPage {
				Title = "Xamarin Test Suite"
			};

			Button excerciseSQL = new Button {
				Text = "SQL",
				VerticalOptions = LayoutOptions.Start,
				Image = "database.png"
			};		

			Button camera = new Button {
				Text = "Cam",
				VerticalOptions = LayoutOptions.Start,
				Image = "camera.png"
			};

			Button web = new Button {
				Text = "Web",
				VerticalOptions = LayoutOptions.Start,
				Image = "earth.png"
			};

			Button geo = new Button {
				Text = "Geo",
				VerticalOptions = LayoutOptions.Start,
				Image = "compass.png"
			};

			Button ws = new Button {
				Text = "WS",
				VerticalOptions = LayoutOptions.Start,
				Image = "ws.png"
			};

			Button bluetooth = new Button {
				Text = "BT",
				VerticalOptions = LayoutOptions.Start,
				Image = "bluetooth.png"
			};

			Button send = new Button {
				Text = "Send",
				VerticalOptions = LayoutOptions.Start,
				Image = "send.png"
			};

			Button share = new Button {
				Text = "Share",
				VerticalOptions = LayoutOptions.Start,
				Image = "share.png"
			};

			Editor results = new Editor () {
				VerticalOptions = LayoutOptions.FillAndExpand
			};


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

			camera.Clicked += (object sender, EventArgs e) => {
				DependencyService.Get<ICameraPage>().OpenCamera();
			};

			web.Clicked += (object sender, EventArgs e) => {

				myPage.Navigation.PushAsync(new WebViewer());
			};

			geo.Clicked += (object sender, EventArgs e) => {
				results.Text = "";
				DependencyService.Get<ICameraPage>().OpenGPS();
				DependencyService.Get<ICameraPage>().GPSUpdated += (object s, EventArgs e1) => {
					Position gps = (Position)s;
					results.Text += String.Format("GPS: lat{0} lon{1} alt{2}", gps.Latitude, gps.Longitude, gps.Altitude) + "\n";
				};
			};
				
			ws.Clicked += (object sender, EventArgs e) => {
				var httpClient = new HttpClient(new NativeMessageHandler());

				Task<HttpResponseMessage> getResponse = httpClient.GetAsync("http://wsf.cdyne.com/WeatherWS/Weather.asmx/GetCityForecastByZIP?ZIP=76092");
				HttpResponseMessage msg = getResponse.Result;
				Task<string> finalMsg = msg.Content.ReadAsStringAsync();
				results.Text = finalMsg.Result;
			};

			Grid buttonGrid = new Grid {
				RowDefinitions = {
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
			buttonGrid.Children.Add (excerciseSQL, 0, 0);
			buttonGrid.Children.Add (camera, 0, 1);
			buttonGrid.Children.Add (web, 0, 2);
			buttonGrid.Children.Add (geo, 0, 3);
			buttonGrid.Children.Add (ws, 1, 0);
			buttonGrid.Children.Add (bluetooth, 1, 1);
			buttonGrid.Children.Add (share, 1, 2);
			buttonGrid.Children.Add (send, 1, 3);

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

