﻿using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using Xamarin.Geolocation;
using Xamarin.Forms;
using ModernHttpClient;

namespace XamarinSecurityTests
{
	public partial class SecurityMainPage : ContentPage
	{
		public static Geolocator GpsPositionLocator { get; set; }

		public SecurityMainPage ()
		{
			InitializeComponent ();

		}

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

		void GpsToggled(object sender, EventArgs evt)
		{
			
			if (!GpsSwitch.IsToggled) {
				GpsPositionLocator.StopListening();
				GpsPositionLocator = null;
				return;
			}

			//DependencyService.Get<ICameraPage>().OpenGPS();
			GpsPositionLocator = new Geolocator ();
			GpsPositionLocator.StartListening(500, 5);

			GpsPositionLocator.GetPositionAsync (timeout: 10000).ContinueWith (t => {
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

			Task<HttpResponseMessage> getResponse = httpClient.GetAsync("http://wsf.cdyne.com/WeatherWS/Weather.asmx/GetCityForecastByZIP?ZIP=76092");
			HttpResponseMessage msg = getResponse.Result;
			Task<string> finalMsg = msg.Content.ReadAsStringAsync();
			Output.Text = "HTTP\n" + finalMsg.Result.Substring(0, 300) + "...\n";

			//api key is limited to 1000 requests/day
			Task<HttpResponseMessage> getResponse2 = httpClient.GetAsync("https://api.forecast.io/forecast/f0fc68cd396162493bc12640cdbfdde0/37.8267,-122.423");
			HttpResponseMessage msg2 = getResponse2.Result;
			Task<string> finalMsg2 = msg2.Content.ReadAsStringAsync();
			Output.Text += "\nHTTPS\n" + finalMsg2.Result.Substring(0,300) + "...\n";
		}
	}
}
