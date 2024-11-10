using Bytescout.Spreadsheet;
using E_Fawatercom_Test.Data;
using E_Fawatercom_Test.Helpers;
using E_Fawatercom_Test.POM;
using OpenQA.Selenium;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Fawatercom_Test.Assest_Methods
{
	public class UpdateProfile_AssistantMethods
	{
		public static Admin ReadProfileDataFromExcle(int row)
		{
			Admin admin = new Admin();
			Worksheet worksheet = CommonMethods.ReadExcel("Update_Profile_Info");
			admin.fullName = Convert.ToString(worksheet.Cell(row, 1).Value);
			admin.phoneNumber = Convert.ToString(worksheet.Cell(row, 2).Value);
			admin.email = Convert.ToString(worksheet.Cell(row, 3).Value);
			admin.currentPassword = Convert.ToString(worksheet.Cell(row, 4).Value);
			admin.address = Convert.ToString(worksheet.Cell(row, 5).Value);
			admin.username = Convert.ToString(worksheet.Cell(row, 6).Value);

			return admin;
		}

		public static void FillUpdateProfileForm(Admin admin)
		{
			ProfilePage profilePage = new ProfilePage(ManageDriver.driver);
			profilePage.EntertFullName(admin.fullName);
			profilePage.EnterPhoneNumber(admin.phoneNumber);
			profilePage.Entertemail(admin.email);
			profilePage.EntertCurrentPassword(admin.currentPassword);
			profilePage.Enteraddress(admin.address);
			profilePage.EntertUserName(admin.username);
			profilePage.ClickUpdateButton();

		}


		public static bool CheackUpdateifPassedInDataBase(string fullname)
		{
			try
			{
				bool isData = false;
				string query = "select count(*) from userf where fullname = :value";

				using (OracleConnection oracleConnection = new OracleConnection(GlobalConstants.connectionString))
				{
					oracleConnection.Open();

					OracleCommand oracleCommand = new OracleCommand(query, oracleConnection);
					oracleCommand.Parameters.Add(new OracleParameter(":value", fullname));
					oracleCommand.CommandTimeout = 60;
					int result = Convert.ToInt32(oracleCommand.ExecuteScalar());

					isData = result > 0;
					Console.WriteLine(oracleCommand);
					Console.WriteLine(result);

					return isData;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error from database:{ex.Message}");
				throw;
			}
		}

	}
}
