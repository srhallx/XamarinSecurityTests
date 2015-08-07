using System;
using Xamarin.Forms;
using XamarinSecurityTests.iOS;
using Xamarin.Media;
using Xamarin.Geolocation;
using System.Threading.Tasks;
using System.Net.Http;
using System.Threading;



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
			

//		public void OpenGPS ()
//		{
//			if (App.GpsPositionLocator == null) {
//				App.GpsPositionLocator = new Geolocator { DesiredAccuracy = 50 };
//			}
//		}
			

		#endregion
	}
}

