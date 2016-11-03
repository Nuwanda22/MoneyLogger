using System;
using SQLite;
using System.Collections.Generic;
using System.Linq;
using Xamarin.Forms;

namespace MoneyLogger
{
	public class LoginInfoDatabase
	{
		static object locker = new object();

		SQLiteConnection database;

		public LoginInfoDatabase()
		{
			database = DependencyService.Get<ISQLite>().GetConnection();
			database.CreateTable<LoginInfo>();
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

		private LoginInfo loginInfo
		{
			get
			{
				lock (locker)
				{
					return database.Get<LoginInfo>(0);
				}
			}
		}

		public void UpdateData(string id, string password)
		{
			// 만약 사용자가 처음으로 로그인한 것이면
			if (!IsUserLoggedIn)
			{
				lock (locker)
				{
					// 새로운 데이터를 만들어 저장한다.
					database.Insert(new LoginInfo { UserID = id, Password = password });
				}
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
	}
}

