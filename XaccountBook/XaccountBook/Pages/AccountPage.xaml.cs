using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace XaccountBook
{
	public partial class AccountPage : ContentPage
	{
		public AccountPage()
		{
			InitializeComponent();
		}

		async void OnLogoutButtonClicked(object sender, EventArgs e)
		{
			if (await DisplayAlert("", "정말 로그아웃 하시겠습니까?", "확인", "취소"))
			{
				App.LocalDB.Logout();
				App.Current.MainPage = new LoginPage();
			}
		}
	}
}
