using Bytescout.Spreadsheet;
using E_Fawatercom_Test.Data;
using E_Fawatercom_Test.Helpers;
using E_Fawatercom_Test.POM;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Fawatercom_Test.Assest_Methods
{
	public class Biller_AssistantMethods
	{
		public static Bill ReadBillDataFromExcel(int row)
		{
			Bill bill = new Bill();

			Worksheet worksheet = CommonMethods.ReadExcel("Biller_Info");
			bill.billerName = Convert.ToString(worksheet.Cell(row,1).Value);
			bill.email = Convert.ToString(worksheet.Cell(row, 2).Value);
			bill.location = Convert.ToString(worksheet.Cell(row, 3).Value);


			return bill;
		}

		public static void FillBillForm(Bill bill)
		{
			BillerPage billPage = new BillerPage(ManageDriver.driver);
			billPage.EnterBillName(bill.billerName);
			billPage.EnterEmail(bill.email);
			billPage.EnterLocation(bill.location);
			billPage.ClickCreateButton();

		}

		public static bool CheackBillInDataBase(string email)
		{
			try
			{
				bool isData = false;
				string query = "select count(*) from billername where email = :value";

				using (OracleConnection oracleConnection = new OracleConnection(GlobalConstants.connectionString))
				{
					oracleConnection.Open();

					OracleCommand oracleCommand = new OracleCommand(query,oracleConnection);
					oracleCommand.Parameters.Add(new OracleParameter(":value" , email));
					oracleCommand.CommandTimeout= 60;
					int result = Convert.ToInt32(oracleCommand.ExecuteScalar());

					isData = result > 0;
					Console.WriteLine(oracleCommand);
					Console.WriteLine(result);

					return isData;
				}
			}
			catch(Exception ex)
			{
				Console.WriteLine($"Error from database:{ex.Message}");
				throw;
			}
		}
	}
}
