using System;
using Xamarin.Forms;
using System.Net.Http;
using ModernHttpClient;
using System.Threading.Tasks;
using Plugin.Contacts;

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

