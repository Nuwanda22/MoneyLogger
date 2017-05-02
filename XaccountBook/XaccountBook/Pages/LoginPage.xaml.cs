using Plugin.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace XaccountBook.Pages
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

		private async void OnLoginButtonClicked(object sender, EventArgs e)
        {
            var userName = usernameEntry.Text;
            var password = passwordEntry.Text;

            if (string.IsNullOrWhiteSpace(userName)|| string.IsNullOrWhiteSpace(password))
			{
				await DisplayAlert("", "Please input ID or password", "OK");
			}
            else
            {
                // TODO: 서버를 통해 로그인 시도
                if (userName == "admin" && password == "1234")
                {
                    CrossSettings.Current.AddOrUpdateValue("id", userName);
                    CrossSettings.Current.AddOrUpdateValue("password", password);

                    Application.Current.MainPage = new MainPage();
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
}
