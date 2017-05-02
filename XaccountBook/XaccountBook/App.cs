using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

using XaccountBook.Pages;
using Plugin.Settings.Abstractions;
using Plugin.Settings;

namespace XaccountBook
{
	public partial class App : Application
	{
        ISettings settings => CrossSettings.Current;
        
        public App()
		{
            // 사용자가 로그인한 기록이 있고 로그인 정보가 맞으면
            // TODO: 서버로부터 로그인 확인
            if (settings.Contains("id") && settings.Contains("password") && true)
            {
                // 메인 화면을 표시한다.
                MainPage = new MainPage();
            }
			else
			{
				MainPage = new LoginPage();
			}
		}

		public void CallSMSReceived(object sender, SMSReceivedEventArgs e)
		{
            // TODO: DB 저장
            var statement = Statement.ExtractFromSMS(e.Address, e.Message);
        }
	}
}
