using System;
using System.Collections.Generic;
using System.Linq;
using SQLite;

namespace SecurityTestsSql
{
	public class PeopleDatabase : SQLiteConnection
	{

		public PeopleDatabase (string path) : base (path)
		{
			CreateTable<Person> ();
		}

		public IEnumerable<Person> GetPeople ()
		{
			return (from i in this.Table<Person> () select i);
		}

		public Person GetPerson (int id)
		{
			return (from i in Table<Person> ()
				where i.ID == id
				select i).FirstOrDefault ();
		}

		public int AddPerson (Person item)
		{
			return Insert (item);
		}
	}
}

