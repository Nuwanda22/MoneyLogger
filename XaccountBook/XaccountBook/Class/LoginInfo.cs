using System;
using SQLite;

namespace XaccountBook
{
	public class LoginInfo
	{
		[PrimaryKey, AutoIncrement]
		public int ID { get; set; }
		public string UserID { get; set; }
		public string Password { get; set; }
	}
}
