using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace MoneyLogger
{
	public partial class DetailPage : ContentPage
	{
		private const int BUTTON_BORDER_WIDTH = 1;
		private const int BUTTON_HEIGHT = 88;
		private const int BUTTON_HEIGHT_WP = 144;
		private const int BUTTON_HALF_HEIGHT = 44;
		private const int BUTTON_HALF_HEIGHT_WP = 72;
		private const int BUTTON_WIDTH = 88;
		private const int BUTTON_WIDTH_WP = 144;

		public DetailPage()
		{
			InitializeComponent();
			InitializeComponentAsync();

			CashButton.Clicked += OnClicked;
			AccountButton.Clicked += OnClicked;
		}

		private async void InitializeComponentAsync()
		{
			// load user data on web
			var user = await UserData.LoadFromJsonAsync("https://nuwanda22.github.io/userdata.json");
			App.User = user;
			BindingContext = user;
		}

		private async void OnClicked(object sender, EventArgs e)
		{
			// 버튼을 가져와
			Button button = sender as Button;

			// 현금을 선택한건지 통장 잔액을 선택한지 알아내고
			bool isCash = button.Image.File == "cash.png"; // true면 현금, false면 통장 잔액
														   // 입력창을 만든 후 그 값을 넘긴다.
			await Navigation.PushAsync(new InputPage(isCash));
		}

		async void OnSignUpButtonClicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new Page());
		}
	}
}
