using System;
using Xamarin.Forms;
using XamarinSecurityTests.iOS;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.AVFoundation;
using System.Diagnostics;
using System.IO;

[assembly: Dependency (typeof (MicPage_iOS))]
namespace XamarinSecurityTests.iOS
{
	public class MicPage_iOS : IMicPage
	{
		public MicPage_iOS ()
		{
		}

		#region IMicPage implementation

		AVAudioRecorder recorder;
		AVPlayer player;
		NSUrl audioFilePath = null;
		NSObject observer;

		public void StartRecording ()
		{
			Console.WriteLine("Begin Recording");

			var session = AVAudioSession.SharedInstance();

			NSError error = null;
			session.SetCategory(AVAudioSession.CategoryRecord, out error);
			if (error != null)
			{
				Console.WriteLine(error);
				return;
			}

			session.SetActive(true, out error);
			if (error != null)
			{
				Console.WriteLine(error);
				return;
			}

			if (!PrepareAudioRecording())
			{
				return;
			}

			if (!recorder.Record())
			{
				return;
			}

		}

		public void StopRecording ()
		{
			if (recorder != null)
				this.recorder.Stop();
		}

		public void PlayRecording ()
		{
			try
			{
				Console.WriteLine("Playing Back Recording " + this.audioFilePath.ToString());

				// The following line prevents the audio from stopping 
				// when the device autolocks. will also make sure that it plays, even
				// if the device is in mute
				NSError error = null;
				AVAudioSession.SharedInstance().SetCategory(AVAudioSession.CategoryPlayback, out error);
				if (error != null)
				{
					throw new Exception(error.DebugDescription);
				}
				//AudioSession.Category = AudioSessionCategory.MediaPlayback;

				this.player = new AVPlayer(this.audioFilePath);
				this.player.Play();
			}
			catch (Exception ex)
			{
				Console.WriteLine("There was a problem playing back audio: ");
				Console.WriteLine(ex.Message);
			}
		
		}

		#endregion

		bool PrepareAudioRecording()
		{
			//Declare string for application temp path and tack on the file extension
			string fileName = string.Format("Myfile{0}.aac", DateTime.Now.ToString("yyyyMMddHHmmss"));

			string tempRecording = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), fileName);

			Console.WriteLine(tempRecording);
			this.audioFilePath = NSUrl.FromFilename(tempRecording);

			var audioSettings = new AudioSettings()
			{
				SampleRate = 44100.0f, 
				Format = MonoTouch.AudioToolbox.AudioFormatType.MPEG4AAC,
				NumberChannels = 1,
				AudioQuality = AVAudioQuality.High
			};

			//Set recorder parameters
			NSError error;
			recorder = AVAudioRecorder.Create(this.audioFilePath, audioSettings, out error);
			if ((recorder == null) || (error != null))
			{
				Console.WriteLine(error);
				return false;
			}

			//Set Recorder to Prepare To Record
			if (!recorder.PrepareToRecord())
			{
				recorder.Dispose();
				recorder = null;
				return false;
			}

			recorder.FinishedRecording += delegate (object sender, AVStatusEventArgs e)
			{
				recorder.Dispose();
				recorder = null;
				Console.WriteLine("Done Recording (status: {0})", e.Status);
			};

			return true;
		}
	}
}

