using System;
using SQLite;

namespace XamarinSecurityTests
{
	public class Settings
	{

			public Settings () {}
			public Settings (string property, string value) { Property = property; Value = value; }

			[PrimaryKey, AutoIncrement]
			public int ID { get; set; }
			public string Property { get; set; }
			public string Value { get; set; }

	}
}

