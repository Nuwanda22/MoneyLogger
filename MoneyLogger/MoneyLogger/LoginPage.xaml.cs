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

		async void OnSignUpButtonClicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new SignUpPage());
		}

		async void OnLoginButtonClicked(object sender, EventArgs e)
		{
			var Username = usernameEntry.Text;
			var Password = passwordEntry.Text;

			if (Username == "admin" && Password == "1234")
			{
				App.IsUserLoggedIn = true;
				App.Current.MainPage = new MainPage();
			}
			else
			{
				await DisplayAlert("", "Login failed", "OK");
				passwordEntry.Text = string.Empty;
			}
		}
	}
}
