using Bytescout.Spreadsheet;
using E_Fawatercom_Test.Data;
using E_Fawatercom_Test.Helpers;
using E_Fawatercom_Test.POM;
using OpenQA.Selenium.DevTools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Fawatercom_Test.Assest_Methods
{
	public class Login_AssistantMethods
	{
		public static Admin ReadLoginDataFromExcle(int row)
		{
			Admin admin = new Admin();
			Worksheet worksheet = CommonMethods.ReadExcel("Admin_Login_Info");
			admin.username = Convert.ToString(worksheet.Cell(row, 1).Value);
			admin.password = Convert.ToString(worksheet.Cell(row, 2).Value);

			return admin;
		}

		public static void FillLoginForm(Admin admin)
		{
			LoginPage loginPage = new LoginPage(ManageDriver.driver);
			loginPage.EntertUserName(admin.username);
			loginPage.EnterPassword(admin.password);
			loginPage.ClickSigninButton();

		}
	}
}
