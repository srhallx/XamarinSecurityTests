using System;
using Android;
using Xamarin.Media;
using Xamarin.Forms;
using XamarinSecurityTests.Android;
using Xamarin.Geolocation;
using System.Threading.Tasks;
using System.Net.Http;
using OkHttp;
using System.Threading;



[assembly: Dependency (typeof (CameraPage_Android))]
namespace XamarinSecurityTests.Android
{
	public class CameraPage_Android : ICameraPage
	{
		public CameraPage_Android ()
		{
		
		}

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


		#endregion
	}
}

