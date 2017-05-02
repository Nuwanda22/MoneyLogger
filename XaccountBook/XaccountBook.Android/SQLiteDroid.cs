using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;

using Xamarin.Forms;
using XaccountBook.Droid;
using SQLite;
using System.IO;

[assembly: Dependency(typeof(SQLiteDroid))]
namespace XaccountBook.Droid
{
	public class SQLiteDroid : ISQLite
	{
		public SQLiteDroid() { }
		public SQLiteConnection GetConnection()
		{
			string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
			string path = Path.Combine(documentsPath, "login.db3");
			
			return new SQLiteConnection(path);
		}
	}
}