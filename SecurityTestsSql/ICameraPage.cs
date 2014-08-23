using System;

namespace SecurityTestsSql
{
	public interface ICameraPage
	{
		void OpenCamera();

		void OpenGPS();

		event EventHandler GPSUpdated;

	
	}
}

