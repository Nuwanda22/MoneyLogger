using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace MoneyLogger
{
	public partial class InputPage : ContentPage
	{
		bool isCash;
		public InputPage(bool isCash)
		{
			InitializeComponent();

			this.isCash = isCash;

			OKButton.Clicked += OKButtonClicked;
			InputEntry.TextChanged += InputEntryTextChanged;
		}

		private void InputEntryTextChanged(object sender, TextChangedEventArgs e)
		{
			//TODO: 숫자 외에 차단
			//System.Diagnostics.Debug.WriteLine(e.NewTextValue);
		}

		private async void OKButtonClicked(object sender, EventArgs e)
		{
			// 입력된 문자열 저장
			string input = InputEntry.Text;

			// 올바르게 입력되었는지 물어본다.
			string alert = "입력하신 금액이 " + input + "이 맞습니까?";
			if (await DisplayAlert("", "" + alert, "Yes", "No"))
			{
				// 예 이면 앞페이지에 넘긴다.
				await Navigation.PopAsync();
			}
		}
	}
}
