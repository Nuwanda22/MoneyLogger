using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MoneyLogger
{
	public partial class MainPage : MasterDetailPage
	{
		public MainPage()
		{
			InitializeComponent();

			//(App.Current as App).SMSReceived += (sender, e) => { DisplayAlert(e.Address, e.Message, "OK"); }; 
		}
	}
}
