using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

using Xamarin.Forms;
using Plugin.Contacts;

namespace XamarinSecurityTests
{
	public partial class ContactsPage : ContentPage
	{
		public ContactsPage ()
		{
			InitializeComponent ();


		}

		protected override void OnAppearing ()
		{
			base.OnAppearing ();

			GetAddresses ();

			AddContact.Clicked += (object sender, EventArgs e) => {
				if (CrossContacts.Current.IsReadOnly == true) {
					DisplayAlert("Contacts", "Contacts Book is marked read-only", "Cancel");
				}
				else {
					//App.ContactsBook.
				}
			};
				
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

