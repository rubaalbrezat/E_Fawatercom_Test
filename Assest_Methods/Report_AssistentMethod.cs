using AventStack.ExtentReports.Gherkin.Model;
using Bytescout.Spreadsheet;
using E_Fawatercom_Test.Data;
using E_Fawatercom_Test.Helpers;
using E_Fawatercom_Test.POM;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel.Composition.Primitives;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Fawatercom_Test.Assest_Methods
{
	public class Report_AssistentMethod
	{

		public static Report ReadReportDataFromExcle(int row)
		{
			Report report = new Report();
			Worksheet worksheet = CommonMethods.ReadExcel("Report_Info");
			report.dateFrom = Convert.ToString(worksheet.Cell(row, 1).Value);
			report.dateTo = Convert.ToString(worksheet.Cell(row, 2).Value);

			return report;
		}

		public static void SelectRandomCategory()
		{
			ReportPage reportPage = new ReportPage(ManageDriver.driver);
			reportPage.GetRandomCategoryFromSelect();
		}

		public static void FillDatesFieldsFromExcel(Report report)
		{
			ReportPage reportPage = new ReportPage(ManageDriver.driver);
			
			reportPage.SetDateFrom(report.dateFrom);
			reportPage.SetDateTo(report.dateTo);
			
		}

		

		public static string CheackifCountPayIsRightByCategoryName(string categoryname)
		{
			try
			{
				
				string query = "SELECT COUNT(p.paymentid) FROM	paymenthistory p JOIN billername b ON p.billerid = b.billerid JOIN billercategory c ON b.billercategoryid = c.billercategoryid WHERE c.billercategoryname = :value";

				using (OracleConnection oracleConnection = new OracleConnection(GlobalConstants.connectionString))
				{
					oracleConnection.Open();

					OracleCommand oracleCommand = new OracleCommand(query, oracleConnection);
					oracleCommand.Parameters.Add(new OracleParameter(":value", categoryname));
					oracleCommand.CommandTimeout = 60;
					string result = Convert.ToString(oracleCommand.ExecuteScalar());

					

					Console.WriteLine(oracleCommand);
					Console.WriteLine(result);

					return result;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error from database:{ex.Message}");
				throw;
			}
		}

		

		public static string CheackifSumProfitIsRightByCategoryName(string categoryname)
		{
			try
			{


				string query = "SELECT SUM(p.profits)  FROM paymenthistory p JOIN billername b ON p.billerid = b.billerid  JOIN billercategory c ON b.billercategoryid = c.billercategoryid WHERE c.billercategoryname = :value";

				using (OracleConnection oracleConnection = new OracleConnection(GlobalConstants.connectionString))
				{
					oracleConnection.Open();

					OracleCommand oracleCommand = new OracleCommand(query, oracleConnection);
					oracleCommand.Parameters.Add(new OracleParameter(":value", categoryname));
					oracleCommand.CommandTimeout = 60;
					string result = Convert.ToString(oracleCommand.ExecuteScalar());



					Console.WriteLine(oracleCommand);
					Console.WriteLine(result);

					return result;
				}
			}
			catch (Exception ex)
			{
				Console.WriteLine($"Error from database:{ex.Message}");
				throw;
			}
		}


		

		public static int CalculateTotalProfit()
		{
			
				var profitElements = ManageDriver.driver.FindElements(By.XPath("//td[3]"));
			
				int totalProfit = 0;
				foreach (var element in profitElements)
				{

					string profitText = element.Text.Replace("$", "").Trim();
					if (int.TryParse(profitText, out int profit))
					{
						totalProfit += profit;
					}
				}

				return totalProfit;
		
		}


	


	}
}
