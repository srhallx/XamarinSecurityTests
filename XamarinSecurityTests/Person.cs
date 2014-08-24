﻿using System;
using SQLite;

namespace XamarinSecurityTests
{
	public class Person
	{
		public Person () { }

		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
	}

}
