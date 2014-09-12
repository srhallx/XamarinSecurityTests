using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Android.App;
using Android.Bluetooth;
using Android.Content;
using Android.Content.PM;
using Android.Database;
using Android.Graphics;
using Android.Net;
using Android.Net.Http;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.Lang;
using Java.Util;
using Mono;

[assembly: UsesPermission(Android.Manifest.Permission.Bluetooth)]
namespace XamarinSecurityTests.Android
{
	[Activity (Label = "BTConnectActivity")]
	public class BTConnectActivity : Activity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			SetContentView (Resource.Layout.OpenDocRenderer);


		}

		private const int REQUEST_ENABLE_BT = 2;

		private BluetoothAdapter bluetoothAdapter = null;

		//private static UUID MY_UUID = UUID.FromString ("fa87c0d0-afac-11de-8a39-0800200c9a66");



		[MethodImpl(MethodImplOptions.Synchronized)]
		public void Connect (BluetoothDevice device)
		{

			bluetoothAdapter = BluetoothAdapter.DefaultAdapter;
			if (bluetoothAdapter == null) {
				Toast.MakeText (this, "Bluetooth is not available", ToastLength.Long).Show ();
				Finish ();
				return;
			}

			if (!bluetoothAdapter.IsEnabled) {
				Intent enableIntent = new Intent (BluetoothAdapter.ActionRequestEnable);
				StartActivityForResult (enableIntent, REQUEST_ENABLE_BT);
			} 

		}
	}
}

