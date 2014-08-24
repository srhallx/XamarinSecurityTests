using System;

namespace XamarinSecurityTests
{
	public interface ICameraPage
	{
		void OpenCamera();

		void OpenGPS();

		event EventHandler GPSUpdated;

	
	}
}

