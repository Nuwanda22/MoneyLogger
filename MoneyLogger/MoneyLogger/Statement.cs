using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MoneyLogger
{
	/// <summary>
	/// 돈의 사용 내역을 자세히 담고 있는 클래스
	/// </summary>
	public class Statement
	{
		public double Amount { get; set; }
		public string Place { get; set; }
		public DateTime DateTime { get; set; }
		public bool IsCash { get; set; }

		public override string ToString()
		{
			string time = DateTime.ToString();
			string money = new Money { Amount = Amount, Currency = Currency.Won }.ToString(false);
			string isCash = IsCash ? "현금" : "카드";
			return $"{time}에 {Place}에서 {isCash}로 {money}원을 사용하셨습니다.";
		}

		private static Dictionary<string, Action> actions;
		/// <summary>
		/// Extract state data from sms
		/// </summary>
		/// <param name="address">Address(phone number) of payment sms</param>
		/// <param name="message">message of payment sms</param>
		/// <returns>return extracted state. If someting is wrong, return null</returns>
		public static Statement ExtractFromSMS(string address, string message)
		{
			Statement state = null;
			if(actions == null)
			{
				actions = new Dictionary<string, Action>();
				actions.Add("15881600", () =>
				{
					string[] part = message.Split('\n');
					// 0 [체크승인]
					// 1 사용량   0,000원
					// 2 사용카드  00카드(0 * 0 *)
					// 3 사용자 홍*동님
					// 4 사용시간 MM/ DD HH: MM
					// 5 사용장소 어딘가
					if (part[0] == "[체크승인]")
					{
						double amount = double.Parse(part[1].Substring(0, part[1].Length - 1), System.Globalization.NumberStyles.Currency);
						DateTime dateTime = DateTime.Parse(DateTime.Now.Year + "/" + part[4]);
						string place = part[5];

						state = new Statement { Amount = amount, DateTime = dateTime, Place = place, IsCash = false };
					}
				});
				Action act = () =>
				{
					string[] temp = message.Split(' ');
					string place = temp[0];
					double money = double.Parse(temp[1]);

					state = new Statement { DateTime = DateTime.Now, Place = place, Amount = money, IsCash = false };
				};
				actions.Add("01027062567", act);
				actions.Add("01066744778", act);
			}

			actions[address].Invoke();

			return state;
		}
	}
}
