using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Fawatercom_Test.Data
{
	public class Admin
	{
		public Admin() {}
		public Admin(string username , string password)
		{
			this.username = username;
			this.password = password;
		}

		public Admin(string fullName, string phoneNumber, string email, string currentPassword, string address, string username)
		{
			this.fullName = fullName; ;
			this.phoneNumber = phoneNumber;
			this.email = email;
			this.currentPassword = currentPassword;
			this.address = address;
			this.username = username;
			
			

		}
		public string username { set; get; }
		public string password { set; get; }


		public string fullName { set; get; }
		public string phoneNumber { set; get; }
		public string email { set; get; }
		public string currentPassword { set; get; }
		public string address { set; get; }
		



	}
}
