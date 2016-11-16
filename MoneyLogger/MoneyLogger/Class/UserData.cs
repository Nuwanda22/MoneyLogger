using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using SQLite;

namespace MoneyLogger
{
	/// <summary>
	/// 통화
	/// </summary>
	public enum Currency
	{
		Dollar, Won, Etc
	}

	/// <summary>
	/// 확장 메소드를 담는 클래스
	/// </summary>
	public static class Extention
	{
		/// <summary>
		/// 돈의 기호를 리턴한다.
		/// </summary>
		/// <param name="currency">통화</param>
		/// <returns>돈 기호 ($, ￦)</returns>
		public static string ToCurrency(this Currency currency)
		{
			switch (currency)
			{
				case Currency.Dollar:
					return "$";
				case Currency.Won:
					return "￦";
				case Currency.Etc:
					return "";
				default:
					throw new ArgumentException("Wrong Argument", "currency");
			}
		}

		public static string CurrencyFormat(this Currency currency)
		{
			switch (currency)
			{
				case Currency.Dollar:
					return "C2";
				case Currency.Won:
					return "C";
				case Currency.Etc:
					return "C";
				default:
					throw new ArgumentException("Wrong Argument", "currency");
			}
		}

		public static string ToCurrency(this double money, Currency currency)
		{
			string currencyFormat = currency.CurrencyFormat();
			return money.ToString(currencyFormat).Substring(1);
		}
	}

	/// <summary>
	/// 돈에 관한 클래스
	/// </summary>
	public class Money
	{
		/// <summary>
		/// 통화 (달러, 원)
		/// </summary>
		public Currency Currency { get; set; }
		/// <summary>
		/// 돈의 양
		/// </summary>
		public double Amount { get; set; }

		/// <summary>
		/// 돈의 통화와 양을 나타낸다.
		/// </summary>
		/// <returns>예) $ 1,000</returns>
		public override string ToString()
		{
			return Currency.ToCurrency() + " " + Amount.ToCurrency(Currency);
		}

		public string ToString(bool currency)
		{
			string money;

			if (currency)
			{
				money = Currency.ToCurrency() + " " + Amount.ToCurrency(Currency);
			}
			else
			{
				money = Amount.ToCurrency(Currency);
			}

			return money;
		}

		[JsonIgnore]
		public string Temp { get { return this.ToString(); } }
	}

	/// <summary>
	/// 사용자 데이터
	/// </summary>
	[SQLite.Table("UserData")]
	public class UserData
	{
		public string Name { get; set; }
		[Ignore]
		public Money Cash { get; set; }
		[Ignore]
		public Money Account { get; set; }

		#region SQLite
		[PrimaryKey, NotNull, AutoIncrement]
		private int ID { get; set; }
		private string cash { get { return JsonConvert.SerializeObject(Cash); } }
		private string account { get { return JsonConvert.SerializeObject(Account); } }
		public void Deserialize() { Cash = JsonConvert.DeserializeObject<Money>(cash); Account = JsonConvert.DeserializeObject<Money>(cash); }
		#endregion

		public string SavaAsJson()
		{
			return JsonConvert.SerializeObject(this);
		}

		public static async Task<UserData> LoadFromJsonAsync(string url)
		{
			try
			{
				string jsonString;
				using (HttpClient client = new HttpClient())
				{
					jsonString = await client.GetStringAsync(url);
				}

				return JsonConvert.DeserializeObject<UserData>(jsonString);
			}
			catch (Exception ex)
			{
				System.Diagnostics.Debug.WriteLine(ex.Message);
				return null;
			}
		}
	}
}

