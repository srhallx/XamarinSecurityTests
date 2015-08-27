using System;
using Android;
using Android.Media;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Xamarin.Media;
using Xamarin.Forms;
using XamarinSecurityTests.Android;
using Xamarin.Geolocation;
using System.Threading.Tasks;
using System.Net.Http;
using OkHttp;
using System.IO;


[assembly: Dependency (typeof (MicPage_Android))]
namespace XamarinSecurityTests.Android
{
	public class MicPage_Android : IMicPage
	{
		static string filePath = "/data/data/XamarinSecurityTests.Android/files/testAudio.mp4";
		public MicPage_Android ()
		{
	
		}
			
		#region IMicPage implementation
		protected MediaRecorder recorder;
		protected MediaPlayer player;

		public void StartRecording ()
		{

			try {
				if (File.Exists (filePath)) {
					File.Delete (filePath);
				}

				if (recorder == null) {
					recorder = new MediaRecorder (); // Initial state.
				}

				recorder.Reset ();
				recorder.SetAudioSource (AudioSource.Mic);
				recorder.SetOutputFormat (OutputFormat.Mpeg4);
				recorder.SetAudioEncoder (AudioEncoder.AmrNb);
				// Initialized state.
				recorder.SetOutputFile (filePath);
				// DataSourceConfigured state.
				recorder.Prepare (); // Prepared state
				recorder.Start (); // Recording state.

			} catch (Exception ex) {
				Console.Out.WriteLine( ex.StackTrace);
			}
		}

		public void StopRecording()
		{
			if (recorder != null) {
				recorder.Stop();

				recorder.Reset();
				recorder.Release();
				recorder = null;
			}
		}

		public void PlayRecording()
		{
			if (player == null) {
				player = new MediaPlayer();
			} 

			if (File.Exists (filePath)) {
				player.Reset ();
				player.SetDataSource (filePath);
				player.Prepare ();
				player.Start ();


			}
		}			
		#endregion
	}
}

