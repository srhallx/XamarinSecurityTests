using System;
using SQLite;
using System.IO;
using System.Collections;
using System.Collections.Generic;


namespace XamarinSecurityTests
{


	public class DataOperations : MobileOperations
	{
		public DataOperations ()
		{
		}



		public void CheckAndCreateDatabase (string dbName)
		{
			try 
			{
				using(SQLiteConnection db = new SQLiteConnection (GetDBPath (dbName)))
				{				
					db.DropTable<Person>();

					db.CreateTable<Person> ();
					Log ("Created Table.");

					// declare vars
					List<Person> people = new List<Person> ();

					// create a list of people that we're going to insert
					people.Add (CreatePerson ("Peter", "Gabriel"));
					people.Add (CreatePerson ("Thom", "Yorke"));
					people.Add (CreatePerson ("J", "Spaceman"));
					people.Add (CreatePerson ("Benjamin", "Gibbard"));

					// insert our people
					int rows = db.InsertAll (people);
					Log(String.Format("Inserted {0} rows.", rows));

					// close the connection
					db.Close ();
					Log("Closed db");
				}
			}
			catch (Exception ex) {
				Log (ex.Message);
			}
		}

		public void QueryDatabase(string dbName)
		{
			try
			{
				using (SQLiteConnection db = new SQLiteConnection (GetDBPath (dbName))) {

					Log("Querying DB");
					List<Person> people = new List<Person> (from p in db.Table<Person>() select p);
					Log(String.Format("Returned {0} rows",people.Count));

					db.Close();
				}

			}
			catch (Exception ex) {
				Log (ex.Message);
			}
		}

		public void DeleteRows(string dbName)
		{
			try
			{
				using (SQLiteConnection db = new SQLiteConnection (GetDBPath (dbName))) {

					Log("Deleting row in DB");
					List<Person> people = new List<Person> (from p in db.Table<Person>() select p);

					db.Delete(people.FindLast(p => p.LastName == "Spaceman"));

					db.Close();

					QueryDatabase(dbName);
				}

			}
			catch (Exception ex) {
				Log (ex.Message);
			}
		}


		Person CreatePerson(string firstname, string lastname) 
		{
			return new Person () { FirstName = firstname, LastName = lastname };
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

