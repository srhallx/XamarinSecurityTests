using System;
using Xamarin.Forms;
using Xamarin.Media;
using Xamarin.Geolocation;
using System.Net.Http;
using ModernHttpClient;
using System.Threading.Tasks;
using Xamarin.Contacts;

namespace XamarinSecurityTests
{
	public class App
	{

		public static AddressBook ContactsBook { get; set; }

		public static Page GetMainPage ()
		{	
			return new NavigationPage (new SecurityMainPage());
		}

	}
}

