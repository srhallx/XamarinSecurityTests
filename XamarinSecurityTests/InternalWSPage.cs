using System;
using SQLite;
using Xamarin.Forms;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using ModernHttpClient;
using System.Threading.Tasks;
using System.Linq;



namespace XamarinSecurityTests
{
	public class InternalWSPage : ContentPage
	{
		string dbName = "settingsdb.db3";


		public InternalWSPage ()
		{

			Title = "Internal WS";

			var webServiceURI = new Entry { Placeholder = "Enter GET URI" };
			var execWS = new Button { Text = "Exec WS" };
			var response = new Label ();


			webServiceURI.TextChanged += (object sender, TextChangedEventArgs e) => {
				StoreURI(webServiceURI.Text);
			};



			execWS.Clicked += (object sender, EventArgs e) => {
				var httpClient = new HttpClient(new NativeMessageHandler());


				Task<HttpResponseMessage> getResponse = httpClient.GetAsync(webServiceURI.Text);
				HttpResponseMessage msg = getResponse.Result;

				Task<string> finalMsg = msg.Content.ReadAsStringAsync();
				response.Text = "HTTP (" + msg.StatusCode.ToString() + ")\n" + finalMsg.Result.Substring(0, 1000);
			
			};

			Content = new StackLayout { 

				Children = {
					webServiceURI, 
					execWS, 
					response
				}

			};

			string uri = GetURI ();
			if (uri != null)
				webServiceURI.Text = uri;
		}


		public string GetURI(){

			try
			{
				using (SQLiteConnection db = new SQLiteConnection (GetDBPath (dbName))) {

					List<Settings> settings = new List<Settings> (from p in db.Table<Settings>() select p);

					db.Close();

					if (settings.Count > 0)
					{
						string val = settings.Where(x => x.Property.Equals("wsuri")).First().Value;

						return val;
					}
				}
			}
			catch (Exception ex) {
				Console.WriteLine (ex.Message);
			}

			return null;
		}

		public void StoreURI(string uri) 
		{

			try 
			{
				SQLiteConnection db = new SQLiteConnection (GetDBPath (dbName));
				

				db.DropTable<Settings>();

				db.CreateTable<Settings> ();

				// declare vars
				List<Settings> settings = new List<Settings> ();

				settings.Add (new Settings ("wsuri", uri));

				int rows = db.InsertAll (settings);

				// close the connection
				db.Close ();

			}
			catch (Exception ex) {
				Console.WriteLine (ex.Message);
			}
		}

		protected string GetDBPath (string dbName)
		{
			// get a reference to the documents folder
			var documents = Environment.GetFolderPath (Environment.SpecialFolder.Personal);

			// create the db path
			string db = Path.Combine (documents, dbName);

			return db;
		}
			
	}
}


