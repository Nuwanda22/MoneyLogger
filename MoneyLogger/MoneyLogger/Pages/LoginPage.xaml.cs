using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace MoneyLogger
{
	public partial class LoginPage : ContentPage
	{
		public LoginPage()
		{
			InitializeComponent();
		}

		//async void OnSignUpButtonClicked(object sender, EventArgs e)
		//{
		//	await Navigation.PushAsync(new SignUpPage());
		//}

		async void OnLoginButtonClicked(object sender, EventArgs e)
		{
			// UI로부터 데이터를 가져옴
			var Username = usernameEntry.Text;
			var Password = passwordEntry.Text;

			if (string.IsNullOrWhiteSpace(Username)|| string.IsNullOrWhiteSpace(Password))
			{
				await DisplayAlert("", "Please input ID or password", "OK");
			}

			// 서버를 통해 로그인 시도함
			// TODO 서버 데이터베이스 연결
			if (Username == "admin" && Password == "1234")
			{
				// 로컬 DB에 저장하고
				App.LocalDB.Login(Username, Password);
				// 로그인되면 메인 화면을 띄운다.
				App.Current.MainPage = new MainPage();
			}
			else
			{
				// 로그인이 안 되면 알림창을 띄운다.
				await DisplayAlert("", "Login failed", "OK");
				passwordEntry.Text = string.Empty;
			}
		}
	}
}
