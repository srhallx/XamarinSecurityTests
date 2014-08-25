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


		/// <summary>
		/// Attempts to drop and create a database table.
		/// Add records to table.
		/// Report on number of rows inserted.
		/// </summary>
		/// <param name="dbName">Db name.</param>
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
					people.Add (new Person ("Peter", "Gabriel"));
					people.Add (new Person ("Thom", "Yorke"));
					people.Add (new Person ("J", "Spaceman"));
					people.Add (new Person ("Benjamin", "Gibbard"));

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


		/// <summary>
		/// Queries the database.
		/// </summary>
		/// <param name="dbName">Db name.</param>
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

		/// <summary>
		/// Deletes specified row in DB.
		/// </summary>
		/// <param name="dbName">Db name.</param>
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

