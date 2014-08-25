using System;
using SQLite;

namespace XamarinSecurityTests
{
	public class Person
	{
		public Person () {}
		public Person (string first, string last) { FirstName = first; LastName = last; }

		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }

	}

}

