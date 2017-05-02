using System;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace XaccountBook
{
	public class LocalDatabase
	{
		static object locker = new object();

		SQLiteConnection database;

		public LocalDatabase()
		{
			database = DependencyService.Get<ISQLite>().GetConnection();
			database.CreateTable<LoginInfo>();
			database.CreateTable<Statement>();
			database.CreateTable<UserData>();
		}
		
		#region Login
		LoginInfo loginInfo
		{
			get
			{
				lock (locker)
				{
					return database.Get<LoginInfo>(0);
				}
			}
		}

		public bool IsUserLoggedIn
		{
			get
			{
				lock (locker)
				{
					// 테이블에 담긴 컬럼 수가 0이 아니면 유저는 로그인 기록이 있음.
					return database.Table<LoginInfo>().Count() != 0;
				}
			}
		}

		public string UserID
		{
			get
			{
				lock (locker)
				{
					return loginInfo.UserID;
				}
			}
		}

		public string Password
		{
			get
			{
				lock (locker)
				{
					return loginInfo.Password;
				}
			}
		}

		public void Login(string id, string password)
		{
			// 만약 사용자가 처음으로 로그인한 것이면
			if (!IsUserLoggedIn)
			{
				lock (locker)
				{
					// 새로운 데이터를 만들어 저장한다.
					database.Insert(new LoginInfo { UserID = id, Password = password });
				}
				var user = User;
			}
			// 그게 아니라면
			else
			{
				lock (locker)
				{
					// 현재 데이터에 아이디와 비밀번호를 바꾸어 저장한다.
					database.Update(new LoginInfo { ID = loginInfo.ID, UserID = id, Password = password });
				}
			}
		}

		public void Logout()
		{
			lock (locker)
			{
				database.DeleteAll<LoginInfo>();
			}
		}
		#endregion
		
		#region Statement
		public List<Statement> StatementList
		{
			get
			{
				lock (locker)
				{
					return database.Table<Statement>().ToList();
				}
			}
		}

		public void AddStatement(Statement statement)
		{
			lock (locker)
			{
				database.Insert(statement);
			}
		}
		#endregion

		#region UserData

		public UserData User
		{
			get
			{
				var user = new UserData { Name = "전성우", Account = new Money { Amount = 0, Currency = Currency.Won }, Cash = new Money { Amount = 0, Currency = Currency.Won } };

				lock (locker)
				{
					database.Insert(user);
				}
				return null;
			}
		}

		public List<UserData> user
		{
			get
			{
				var table = database.Table<UserData>();

				foreach(var item in table)
				{
					item.Deserialize();
				}

				return table.ToList();
			}
		}
		#endregion
	}
}
