using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Fawatercom_Test.Data
{
	public class Report
	{
		public Report() {}

		public Report(string dateFrom, string dateTo)
		{
			this.dateFrom = dateFrom;	
			this.dateTo = dateTo;	
		}

		public string dateFrom { set; get; }
		public string dateTo { set; get; }
	}
}
