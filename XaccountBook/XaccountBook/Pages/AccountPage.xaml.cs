using Plugin.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace XaccountBook.Pages
{
	public partial class AccountPage : ContentPage
	{
		public AccountPage()
		{
			InitializeComponent();
		}
        
        private async void Button_Clicked(object sender, EventArgs e)
        {
            if (await DisplayAlert("", "정말 로그아웃 하시겠습니까?", "확인", "취소"))
            {
                CrossSettings.Current.Remove("id");
                CrossSettings.Current.Remove("password");

                App.Current.MainPage = new LoginPage();
            }
        }
    }
}
