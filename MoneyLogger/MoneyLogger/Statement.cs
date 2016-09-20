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
		public double Money { get; set; }
		public string Place { get; set; }
		public DateTime Time { get; set; }
	}
}
