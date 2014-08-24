using System;
using Android;
using Xamarin.Media;
using Xamarin.Forms;
using XamarinSecurityTests.Android;
using Xamarin.Geolocation;
using System.Threading.Tasks;
using System.Net.Http;
using OkHttp;



[assembly: Dependency (typeof (CameraPage_Android))]
namespace XamarinSecurityTests.Android
{
	public class CameraPage_Android : ICameraPage
	{
		public CameraPage_Android ()
		{
		
		}

		public event EventHandler GPSUpdated;

		#region ICameraPage implementation

	
		public void OpenCamera ()
		{
			var picker = new MediaPicker (Forms.Context);
			if (!picker.IsCameraAvailable)
				Console.WriteLine ("No camera!");
			else {
				var intent = picker.GetTakePhotoUI (new StoreCameraMediaOptions {
					Name = "test.jpg",
					Directory = "MediaPickerSample"
				});
				Forms.Context.StartActivity (intent);
			}
		}
			
		public void OpenGPS ()
		{
			var locator = new Geolocator (Forms.Context){ DesiredAccuracy = 50 };
			locator.GetPositionAsync (timeout: 10000).ContinueWith (t => {
				Position p = new Position();
				p.Latitude = t.Result.Latitude;
				p.Longitude = t.Result.Longitude;
				p.Altitude = t.Result.Altitude;

				if (GPSUpdated != null)
					GPSUpdated(p, new EventArgs());
			}, TaskScheduler.FromCurrentSynchronizationContext());
		}
		#endregion
	}
}

