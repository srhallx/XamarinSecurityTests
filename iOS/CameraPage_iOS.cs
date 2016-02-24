using System;
using Xamarin.Forms;
using XamarinSecurityTests.iOS;
using Plugin.Media;
using Plugin.Geolocator;
using System.Threading.Tasks;
using System.Net.Http;
using System.Threading;
using Plugin.Media;
using Plugin.Media.Abstractions;



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

		public async void OpenCamera ()
		{
			StoreCameraMediaOptions scmo = new StoreCameraMediaOptions ();
			scmo.Name = "tempphoto";
		 	await CrossMedia.Current.TakePhotoAsync (scmo);
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

