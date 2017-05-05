using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace XaccountBook.Pages
{
	public partial class DetailPage : ContentPage
	{
		public DetailPage()
		{
			InitializeComponent();
		}

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var user = new UserData { Name = "Kyle", Account = new Money { Amount = 100000, Currency = Currency.Won }, Cash = new Money { Amount = 100000, Currency = Currency.Won } };
            BindingContext = user;
        }

		private async void Button_Clicked(object sender, EventArgs e)
		{
			Button button = sender as Button;

			// 현금을 선택한건지 통장 잔액을 선택한지 알아내고
			bool isCash = button.Image.File == "cash.png"; // true면 현금, false면 통장 잔액
			// 입력창을 만든 후 그 값을 넘긴다.
			await Navigation.PushAsync(new InputPage(isCash));
		}
    }
}
