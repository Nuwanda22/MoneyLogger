using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.WindowsAzure.MobileServices;
using Xamarin.Forms;
using System.IO;

namespace MoneyLogger
{
	public partial class App : Application
	{
		public static UserData User { get; set; }           // 유저 데이터
		public List<Statement> StatementList { get { return LocalDB.StatementList; } }  // 돈 사용내역
		public event EventHandler<SMSReceivedEventArgs> SMSReceived;    // 문자 수신 이벤트

		static LocalDatabase localDB;
		public static LocalDatabase LocalDB
		{
			get
			{
				if (localDB == null)
				{
					localDB = new LocalDatabase();
				}
				return localDB;
			}
		}
		public static bool IsUserLoggedIn
		{
			get
			{
				return LocalDB.IsUserLoggedIn;
			}
		}

		/// <summary>
		/// Default Constuctor
		/// </summary>
		public App()
		{
			SMSReceived += (sender, e) =>
			{
				LocalDB.AddStatement(Statement.ExtractFromSMS(e.Address, e.Message));
			};

			// 사용자가 로그인한 기록이 있으면
			if (IsUserLoggedIn)
			{
				// 서버로부터 확인하여
				// TODO: 서버 데이터베이스 연결
				if (true)
				{
					// 메인 화면을 표시한다.
					MainPage = new MainPage();
				}
				else
				{
					MainPage = new LoginPage();
				}
			}
			else
			{
				MainPage = new LoginPage();
			}
		}

		public void CallSMSReceived(object sender, SMSReceivedEventArgs e)
		{
			SMSReceived?.Invoke(sender, e);
		}

		protected override void OnStart()
		{
			// Handle when your app starts
		}

		protected override void OnSleep()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume()
		{
			// Handle when your app resumes
		}
	}
}
