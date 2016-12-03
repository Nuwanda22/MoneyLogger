using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace MoneyLogger
{
	public partial class MasterPage : ContentPage
	{
		public ListView ListView { get { return listView; } }

		public MasterPage()
		{
			InitializeComponent();
			
			var masterPageItems = new List<MasterPageItem>();
			masterPageItems.Add(new MasterPageItem
			{
				Title = "Account",
				IconSource = "ic_account_box_black_48dp.png",
				TargetType = typeof(AccountPage)
			});
			masterPageItems.Add(new MasterPageItem
			{
				Title = "Settings",
				IconSource = "ic_settings_black_48dp.png",
				TargetType = typeof(AccountPage)
			});

			listView.ItemsSource = masterPageItems;
		}
	}
}
