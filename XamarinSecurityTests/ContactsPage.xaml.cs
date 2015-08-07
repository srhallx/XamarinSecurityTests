using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using Xamarin.Forms;
using Xamarin.Contacts;

namespace XamarinSecurityTests
{
	public partial class ContactsPage : ContentPage
	{
		public ContactsPage ()
		{
			InitializeComponent ();

			GetAddresses ();

		}

		private async void GetAddresses() 
		{
			await App.ContactsBook.RequestPermission().ContinueWith (t => {
				if (!t.Result) {
					
					return;
				}

				EmployeeView.ItemsSource = App.ContactsBook.OrderBy(c=>c.LastName);

			}, TaskScheduler.FromCurrentSynchronizationContext());

		}
	}
}

