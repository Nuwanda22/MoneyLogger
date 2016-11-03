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
			//masterPageItems.Add(new MasterPageItem
			//{
			//	Title = "Contacts",
			//	IconSource = "contacts.png",
			//	TargetType = typeof(Pages.SubMainPage)
			//});

			listView.ItemsSource = masterPageItems;
		}
	}
}
