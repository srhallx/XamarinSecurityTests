using System;
using Xamarin.Forms;
using XamarinSecurityTests.iOS;
using Xamarin.Media;
using Xamarin.Geolocation;
using System.Threading.Tasks;
using System.Net.Http;



[assembly: Dependency (typeof (CameraPage_iOS))]
namespace XamarinSecurityTests.iOS
{
	public class CameraPage_iOS : ICameraPage
	{
		public CameraPage_iOS ()
		{

		}

		public event EventHandler GPSUpdated;			

		#region ICameraPage implementation

		public void OpenCamera ()
		{
			var picker = new MediaPicker();
			StoreCameraMediaOptions scmo = new StoreCameraMediaOptions ();
			scmo.Name = "tempphoto";
			picker.TakePhotoAsync (scmo);
		}
			

		public void OpenGPS ()
		{
			var locator = new Geolocator { DesiredAccuracy = 50 };
			//            new Geolocator (this) { ... }; on Android
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

