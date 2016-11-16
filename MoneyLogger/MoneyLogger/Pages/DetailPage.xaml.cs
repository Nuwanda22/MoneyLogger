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
			UserData user = await UserData.LoadFromJsonAsync("https://nuwanda22.github.io/userdata.json");
			
			if (user != null)
			{
				App.User = user;
				BindingContext = user;
			}
			else
			{
				await DisplayAlert("No Internet", "Internet was disconnected.\nPlease check connection.", "OK");
			}
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

		//async void OnSignUpButtonClicked(object sender, EventArgs e)
		//{
		//	await Navigation.PushAsync(new Page());
		//}

	 	async void ShowLogsClicked(object sender, EventArgs e)
		{
			StringBuilder logs = new StringBuilder();

			//syncIndicator = new ActivityIndicator { HorizontalOptions = LayoutOptions.Center, VerticalOptions = LayoutOptions.Center, IsVisible = false, IsEnabled = true };

			foreach (var statement in /*(App.Current as App).StatementList*/App.LocalDB.user)
			{
				//logs.AppendLine(statement.ToString());
				logs.AppendLine($"{statement.Name} : {statement.Cash }, {statement.Account}");
			}

			await DisplayAlert("", logs.ToString(), "OK");
		}

		//private async Task RefreshItems(bool showActivityIndicator, bool syncItems)
		//{
		//	using (var scope = new ActivityIndicatorScope(syncIndicator, showActivityIndicator))
		//	{
		//		//todoList.ItemsSource = await manager.GetTodoItemsAsync(syncItems);
		//	}
		//}

		//private class ActivityIndicatorScope : IDisposable
		//{
		//	private bool showIndicator;
		//	private ActivityIndicator indicator;
		//	private Task indicatorDelay;

		//	public ActivityIndicatorScope(ActivityIndicator indicator, bool showIndicator)
		//	{
		//		this.indicator = indicator;
		//		this.showIndicator = showIndicator;

		//		if (showIndicator)
		//		{
		//			indicatorDelay = Task.Delay(2000);
		//			SetIndicatorActivity(true);
		//		}
		//		else
		//		{
		//			indicatorDelay = Task.FromResult(0);
		//		}
		//	}

		//	private void SetIndicatorActivity(bool isActive)
		//	{
		//		this.indicator.IsVisible = isActive;
		//		this.indicator.IsRunning = isActive;
		//	}

		//	public void Dispose()
		//	{
		//		if (showIndicator)
		//		{
		//			indicatorDelay.ContinueWith(t => SetIndicatorActivity(false), TaskScheduler.FromCurrentSynchronizationContext());
		//		}
		//	}
		//}
	}
}
