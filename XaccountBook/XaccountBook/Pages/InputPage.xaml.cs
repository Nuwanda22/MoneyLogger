using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace XaccountBook.Pages
{
	public partial class InputPage : ContentPage
	{
		bool isCash;
		public InputPage(bool isCash)
		{
			InitializeComponent();

			this.isCash = isCash;

			AmountButton.Clicked += OKButtonClicked;
			AmountEntry.TextChanged += InputEntryTextChanged;
		}

		private void InputEntryTextChanged(object sender, TextChangedEventArgs e)
		{
			//TODO: 숫자 외에 차단
			//System.Diagnostics.Debug.WriteLine(e.NewTextValue);
		}

		private async void OKButtonClicked(object sender, EventArgs e)
		{
			// 입력된 문자열 저장
			string amount = AmountEntry.Text;
			string place = PlaceEntry.Text;

			double money = double.Parse(amount);
			amount = new Money { Amount = money, Currency = Currency.Won }.ToString(false);

			// 올바르게 입력되었는지 물어본다. 
			string alert = "입력하신 금액이 " + amount + "원이 맞습니까?";
			if (await DisplayAlert("", "" + alert, "Yes", "No"))
			{
				// 예 이면 앞페이지에 넘긴다.	// TODO: Place 입력
			    var statement = new Statement { Amount = money, DateTime = DateTime.Now, Place = place, IsCash = isCash };
				await Navigation.PopAsync();
			}
		}
	}
}
