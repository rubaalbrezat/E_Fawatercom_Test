using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports;
using E_Fawatercom_Test.Data;
using E_Fawatercom_Test.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Fawatercom_Test.Assest_Methods;
using OpenQA.Selenium;
using E_Fawatercom_Test.POM;
using System.Globalization;
using AventStack.ExtentReports.Reporter.Configuration;
using AventStack.ExtentReports.Utils;

namespace E_Fawatercom_Test.Test_Methods
{
	[TestClass]
	public class Report_TestMethod
	{
		public static ExtentReports extentReports = new ExtentReports();
		public static ExtentHtmlReporter reporter = new ExtentHtmlReporter(GlobalConstants.htmlReportPaht);

		[ClassInitialize]

		public static void ClassInitialize(TestContext testContext)
		{
			extentReports.AttachReporter(reporter);
			ManageDriver.MaximizeDriver();
		}

		[ClassCleanup]

		public static void ClassCleanup()
		{

			extentReports.Flush();
			ManageDriver.CloseDriver();
		}

		[TestMethod]

		public void CheckCountPayIsRightByCategoryName()
		{
			var test = extentReports.CreateTest("check count pay", "Verfiy count pay is display correctly as database when search by category name");
			try
			{
				Login_TestMethod login_TestMethod = new Login_TestMethod();
				login_TestMethod.Login_Admin();
				test.Log(Status.Info, "Login successfully");

				Thread.Sleep(2000);

				CommonMethods.NavigateToURL(GlobalConstants.reportPageLink);
			

				Report_AssistentMethod.SelectRandomCategory();
				test.Log(Status.Info, "Fill Report Form Passed ");

				Thread.Sleep(2000);

				try
				{
					var expectedCountPay = Report_AssistentMethod.CheackifCountPayIsRightByCategoryName(ManageDriver.driver.FindElement(By.XPath("//tr//td[1]")).Text);
					var actualCountPay = ManageDriver.driver.FindElement(By.XPath("//tr//td[2]")).Text;
					Console.WriteLine(expectedCountPay);
					Console.WriteLine(actualCountPay);
					Assert.AreEqual(expectedCountPay, actualCountPay, "the count pay not equal");
					test.Log(Status.Info, "the Count Pay is Equal");
				}
				catch (NoSuchElementException) 
				{
					Assert.Fail("No Payment on This Category");
				}


			}
			catch (Exception ex)
			{
				test.Fail(ex.Message);
				string screenShotPath = CommonMethods.TakeScreenShot();
				test.AddScreenCaptureFromPath(screenShotPath);
			}
		}

		


		[TestMethod]
		public void CheckTotalProfitIsRightByCategoryName()
		{
			var test = extentReports.CreateTest("Check Total Profit", "Verify Summation of Total Profit is display correctly when search by category name");
			try
			{
				Login_TestMethod login_TestMethod = new Login_TestMethod();
				login_TestMethod.Login_Admin();
				test.Log(Status.Info, "Login successfully");

				Thread.Sleep(2000);

				CommonMethods.NavigateToURL(GlobalConstants.reportPageLink);
				

				Report_AssistentMethod.SelectRandomCategory();
				test.Log(Status.Info, "Fill Report Form Passed ");

				Thread.Sleep(2000);

				var expectedCountPay = Report_AssistentMethod.CalculateTotalProfit();
				if (expectedCountPay == 0)
				{
					test.Log(Status.Info, "No Payment On This Category");
				}
				else
				{
					var actualCountPay = Convert.ToInt32(ManageDriver.driver.FindElement(By.XPath("//table/tbody/tr[2]/th[2]")).Text.Replace("$", "").Trim());
					Assert.AreEqual(expectedCountPay, actualCountPay, "the Total not equal");
					test.Log(Status.Info, "the Total Pay is Equal");

				}

             }
			catch (Exception ex)
			{
				test.Fail(ex.Message);
				string screenShotPath = CommonMethods.TakeScreenShot();
				test.AddScreenCaptureFromPath(screenShotPath);
			}
		}

		

		[TestMethod]

			public void CheckSumProfitIsRightByCategoryName()
			{
				var test = extentReports.CreateTest("check sum profit ", "Verify sum profit is display correctly as database when search by category name");
				try
				{
					Login_TestMethod login_TestMethod = new Login_TestMethod();
					login_TestMethod.Login_Admin();
					test.Log(Status.Info, "Login successfully");

					Thread.Sleep(2000);

					CommonMethods.NavigateToURL(GlobalConstants.reportPageLink);
					/*Report report = Report_AssistentMethod.ReadReportDataFromExcle(2);
					test.Log(Status.Info, "Read from Excel Passed ");*/

					Report_AssistentMethod.SelectRandomCategory();
					test.Log(Status.Info, "Fill Report Form Passed ");

					Thread.Sleep(2000);

				try
				{
					var SumProfit = ManageDriver.driver.FindElement(By.XPath("//tr//td[3]")).Text;

					var expectedSumProfit = Report_AssistentMethod.CheackifSumProfitIsRightByCategoryName(ManageDriver.driver.FindElement(By.XPath("//tr//td[1]")).Text);
					var actualSumProfit = SumProfit.Replace("$", " ").Trim();
					Console.WriteLine(actualSumProfit);

					Assert.AreEqual(expectedSumProfit, actualSumProfit, "the count pay not equal");
					test.Log(Status.Info, "the Sum Profit is Equal");
				}
				catch (NoSuchElementException) 
				{
					test.Log(Status.Info,"No Payment On This Category");
				}


				}
				catch (Exception ex)
				{
					test.Fail(ex.Message);
					string screenShotPath = CommonMethods.TakeScreenShot();
					test.AddScreenCaptureFromPath(screenShotPath);
				}


			}

		[TestMethod]
		
		public void CheackIfBillsInRightDatesWithValidRange()
		{

			var test = extentReports.CreateTest("check bills in right time period ", "Verfiy bills in right time period after choose date ");
			try
			{
				Login_TestMethod login_TestMethod = new Login_TestMethod();
				login_TestMethod.Login_Admin();
				test.Log(Status.Info, "Login successfully");

				Thread.Sleep(2000);

				CommonMethods.NavigateToURL(GlobalConstants.reportPageLink);
				Report report = Report_AssistentMethod.ReadReportDataFromExcle(2);
				test.Log(Status.Info, "Read from Excel Passed ");
				Console.WriteLine(report.dateFrom);
				Console.WriteLine(report.dateTo);

			
				Report_AssistentMethod.FillDatesFieldsFromExcel(report);
				test.Log(Status.Info, "Fill Report Form Passed ");


				ReportPage reportPage = new ReportPage(ManageDriver.driver);

				
				int detailsButtonCount = reportPage.GetDetailsButtonCount();
				Console.WriteLine(detailsButtonCount);

				string startDate = report.dateFrom;
				string endDate = report.dateTo;


				DateTime startDate1 = DateTime.ParseExact(startDate, "MM-dd-yyyy", CultureInfo.InvariantCulture);
				DateTime endDate1 = DateTime.ParseExact(endDate, "MM-dd-yyyy", CultureInfo.InvariantCulture);
				Console.WriteLine(startDate1);
				Console.WriteLine(endDate1);

				for (int i = 0; i < detailsButtonCount; i++)
				{
					reportPage.ClickDetailsButton(i);

					Thread.Sleep(2000);

					// Get the list of bill dates on the bill page
					List<DateTime> billDates = reportPage.GetDisplayedBillDates();

					


					bool allDatesWithinRange = true;
					List<DateTime> outOfRangeDates = new List<DateTime>();
					Thread.Sleep(2000);
					foreach (var date in billDates)
					{
						if (date < startDate1 || date > endDate1)
						{
							allDatesWithinRange = false;
							outOfRangeDates.Add(date);
							Console.WriteLine(date);
						}
					}

					// Log result
					if (allDatesWithinRange)
					{
						test.Log(Status.Pass, "All bill dates are within the specified range.");
					}
					else
					{
						string outOfRangeDatesMessage = string.Join(", ", outOfRangeDates);
						test.Log(Status.Fail, $"Some bill dates are out of range: {outOfRangeDatesMessage}");
					}
					Thread.Sleep(2000);


					IJavaScriptExecutor js = (IJavaScriptExecutor)ManageDriver.driver;
					js.ExecuteScript($"sessionStorage.setItem('dateFrom', '{report.dateFrom}');");
					js.ExecuteScript($"sessionStorage.setItem('dateTo', '{report.dateTo}');");

					js.ExecuteScript("window.history.go(-1);");
					Thread.Sleep(3000);

					// Retrieve and reapply dates after navigating back
					string dateFrom = (string)js.ExecuteScript("return sessionStorage.getItem('dateFrom');");
					string dateTo = (string)js.ExecuteScript("return sessionStorage.getItem('dateTo');");
					reportPage.SetDateFrom(dateFrom);
					reportPage.SetDateTo(dateTo);
				}


				test.Log(Status.Info, "The bills in right time  period");

			}
			catch (Exception ex)
			{
				test.Fail(ex.Message);
				string screenShotPath = CommonMethods.TakeScreenShot();
				test.AddScreenCaptureFromPath(screenShotPath);
			}

		}



		[TestMethod]
		public void CheackNoBillsInDateRangewhereNoBillsExistIn()
		{

			var test = extentReports.CreateTest("Verify Search with Date Range where No Bills Exist ", "Verify the system not give any invoices with date range where no bills exist ");
			try
			{
				Login_TestMethod login_TestMethod = new Login_TestMethod();
				login_TestMethod.Login_Admin();
				test.Log(Status.Info, "Login successfully");

				Thread.Sleep(2000);

				CommonMethods.NavigateToURL(GlobalConstants.reportPageLink);
				Report report = Report_AssistentMethod.ReadReportDataFromExcle(3);
				test.Log(Status.Info, "Read from Excel Passed ");
				Console.WriteLine(report.dateFrom);
				Console.WriteLine(report.dateTo);


				Report_AssistentMethod.FillDatesFieldsFromExcel(report);
				test.Log(Status.Info, "Fill Report Form Passed ");


				ReportPage reportPage = new ReportPage(ManageDriver.driver);


				int detailsButtonCount = reportPage.GetDetailsButtonCount();
				Console.WriteLine(detailsButtonCount);

				if (detailsButtonCount == 0)
				{
					test.Log(Status.Pass, "No Bills in This Time Period as Expected");
				}
				else
				{
					test.Log(Status.Fail,$"Should No Bill in This Time Period but Foun {detailsButtonCount} Bills");
				}

			}
			catch (Exception ex)
			{
				test.Fail(ex.Message);
				string screenShotPath = CommonMethods.TakeScreenShot();
				test.AddScreenCaptureFromPath(screenShotPath);
			}

		}

		[TestMethod]
		public void CheackNoBillsWithInvalidDateRange()
		{

			var test = extentReports.CreateTest("Verify Search with Invalid Date Range ", "Verify the system not give any invoices with invalid date range (Start Date after End Date)");
			try
			{
				Login_TestMethod login_TestMethod = new Login_TestMethod();
				login_TestMethod.Login_Admin();
				test.Log(Status.Info, "Login successfully");

				Thread.Sleep(2000);

				CommonMethods.NavigateToURL(GlobalConstants.reportPageLink);
				Report report = Report_AssistentMethod.ReadReportDataFromExcle(4);
				test.Log(Status.Info, "Read from Excel Passed ");
				Console.WriteLine(report.dateFrom);
				Console.WriteLine(report.dateTo);


				Report_AssistentMethod.FillDatesFieldsFromExcel(report);
				test.Log(Status.Info, "Fill Report Form Passed ");


				ReportPage reportPage = new ReportPage(ManageDriver.driver);


				int detailsButtonCount = reportPage.GetDetailsButtonCount();
				Console.WriteLine(detailsButtonCount);

				if (detailsButtonCount == 0)
				{
					test.Log(Status.Pass, "No Bills in This Time Period as Expected");
				}
				else
				{
					test.Log(Status.Fail, $"Should No Bill in This Time Period but Foun {detailsButtonCount} Bills");
				}

			}
			catch (Exception ex)
			{
				test.Fail(ex.Message);
				string screenShotPath = CommonMethods.TakeScreenShot();
				test.AddScreenCaptureFromPath(screenShotPath);
			}

		}


		[TestMethod]
		public void CheackIfBillsInRightDatesWithSameDateinBothFromandToFields()
		{

			var test = extentReports.CreateTest("Verify Search with Same Date in Both From and To Fields", "Verify that bills from a single specified date are displayed when the From and To dates are identical");
			try
			{
				Login_TestMethod login_TestMethod = new Login_TestMethod();
				login_TestMethod.Login_Admin();
				test.Log(Status.Info, "Login successfully");

				Thread.Sleep(2000);

				CommonMethods.NavigateToURL(GlobalConstants.reportPageLink);
				Report report = Report_AssistentMethod.ReadReportDataFromExcle(5);
				test.Log(Status.Info, "Read from Excel Passed ");
				Console.WriteLine(report.dateFrom);
				Console.WriteLine(report.dateTo);


				Report_AssistentMethod.FillDatesFieldsFromExcel(report);
				test.Log(Status.Info, "Fill Report Form Passed ");


				ReportPage reportPage = new ReportPage(ManageDriver.driver);


				int detailsButtonCount = reportPage.GetDetailsButtonCount();
				Console.WriteLine(detailsButtonCount);

				string startDate = report.dateFrom;
				string endDate = report.dateTo;


				DateTime startDate1 = DateTime.ParseExact(startDate, "MM-dd-yyyy", CultureInfo.InvariantCulture);
				DateTime endDate1 = DateTime.ParseExact(endDate, "MM-dd-yyyy", CultureInfo.InvariantCulture);
				Console.WriteLine(startDate1);
				Console.WriteLine(endDate1);

				for (int i = 0; i < detailsButtonCount; i++)
				{
					reportPage.ClickDetailsButton(i);

					Thread.Sleep(2000);

					// Get the list of bill dates on the bill page
					List<DateTime> billDates = reportPage.GetDisplayedBillDates();




					bool allDatesWithinRange = true;
					List<DateTime> outOfRangeDates = new List<DateTime>();
					Thread.Sleep(2000);
					foreach (var date in billDates)
					{
						if (date < startDate1 || date > endDate1)
						{
							allDatesWithinRange = false;
							outOfRangeDates.Add(date);
							Console.WriteLine(date);
						}
					}

					// Log result
					if (allDatesWithinRange)
					{
						test.Log(Status.Pass, "All bill dates are within the specified range.");
					}
					else
					{
						string outOfRangeDatesMessage = string.Join(", ", outOfRangeDates);
						test.Log(Status.Fail, $"Some bill dates are out of range: {outOfRangeDatesMessage}");
					}
					Thread.Sleep(2000);


					IJavaScriptExecutor js = (IJavaScriptExecutor)ManageDriver.driver;
					js.ExecuteScript($"sessionStorage.setItem('dateFrom', '{report.dateFrom}');");
					js.ExecuteScript($"sessionStorage.setItem('dateTo', '{report.dateTo}');");

					js.ExecuteScript("window.history.go(-1);");
					Thread.Sleep(3000);

					// Retrieve and reapply dates after navigating back
					string dateFrom = (string)js.ExecuteScript("return sessionStorage.getItem('dateFrom');");
					string dateTo = (string)js.ExecuteScript("return sessionStorage.getItem('dateTo');");
					reportPage.SetDateFrom(dateFrom);
					reportPage.SetDateTo(dateTo);
				}


				test.Log(Status.Info, "The bill in right time  period");

			}
			catch (Exception ex)
			{
				test.Fail(ex.Message);
				string screenShotPath = CommonMethods.TakeScreenShot();
				test.AddScreenCaptureFromPath(screenShotPath);
			}

		}

		[TestMethod]
		public void CheackNoBillswithDateRangeExceedingCurrentDate()
		{

			var test = extentReports.CreateTest("Verify Search with Date Range Exceeding Current Date", "Verify that no results are shown if the date range goes beyond the current date");
			try
			{
				Login_TestMethod login_TestMethod = new Login_TestMethod();
				login_TestMethod.Login_Admin();
				test.Log(Status.Info, "Login successfully");

				Thread.Sleep(2000);

				CommonMethods.NavigateToURL(GlobalConstants.reportPageLink);
				Report report = Report_AssistentMethod.ReadReportDataFromExcle(6);
				test.Log(Status.Info, "Read from Excel Passed ");
				Console.WriteLine(report.dateFrom);
				Console.WriteLine(report.dateTo);


				Report_AssistentMethod.FillDatesFieldsFromExcel(report);
				test.Log(Status.Info, "Fill Report Form Passed ");


				ReportPage reportPage = new ReportPage(ManageDriver.driver);


				int detailsButtonCount = reportPage.GetDetailsButtonCount();
				Console.WriteLine(detailsButtonCount);

				if (detailsButtonCount == 0)
				{
					test.Log(Status.Pass, "No Bills in This Time Period as Expected");
				}
				else
				{
					test.Log(Status.Fail, $"Should No Bill in This Time Period but Foun {detailsButtonCount} Bills");
				}

			}
			catch (Exception ex)
			{
				test.Fail(ex.Message);
				string screenShotPath = CommonMethods.TakeScreenShot();
				test.AddScreenCaptureFromPath(screenShotPath);
			}

		}

		[TestMethod]

		public void CheackIfBillsInRightDatesWithLargeDate()
		{

			var test = extentReports.CreateTest("check bills with large dates range ", "Verfiy bills in right time period after choose date ");
			try
			{
				Login_TestMethod login_TestMethod = new Login_TestMethod();
				login_TestMethod.Login_Admin();
				test.Log(Status.Info, "Login successfully");

				Thread.Sleep(2000);

				CommonMethods.NavigateToURL(GlobalConstants.reportPageLink);
				Report report = Report_AssistentMethod.ReadReportDataFromExcle(7);
				test.Log(Status.Info, "Read from Excel Passed ");
				Console.WriteLine(report.dateFrom);
				Console.WriteLine(report.dateTo);


				Report_AssistentMethod.FillDatesFieldsFromExcel(report);
				test.Log(Status.Info, "Fill Report Form Passed ");


				ReportPage reportPage = new ReportPage(ManageDriver.driver);


				int detailsButtonCount = reportPage.GetDetailsButtonCount();
				Console.WriteLine(detailsButtonCount);

				string startDate = report.dateFrom;
				string endDate = report.dateTo;


				DateTime startDate1 = DateTime.ParseExact(startDate, "MM-dd-yyyy", CultureInfo.InvariantCulture);
				DateTime endDate1 = DateTime.ParseExact(endDate, "MM-dd-yyyy", CultureInfo.InvariantCulture);
				Console.WriteLine(startDate1);
				Console.WriteLine(endDate1);

				for (int i = 0; i < detailsButtonCount; i++)
				{
					reportPage.ClickDetailsButton(i);

					Thread.Sleep(2000);

					// Get the list of bill dates on the bill page
					List<DateTime> billDates = reportPage.GetDisplayedBillDates();




					bool allDatesWithinRange = true;
					List<DateTime> outOfRangeDates = new List<DateTime>();
					Thread.Sleep(2000);
					foreach (var date in billDates)
					{
						if (date < startDate1 || date > endDate1)
						{
							allDatesWithinRange = false;
							outOfRangeDates.Add(date);
							Console.WriteLine(date);
						}
					}

					// Log result
					if (allDatesWithinRange)
					{
						test.Log(Status.Pass, "All bill dates are within the specified range.");
					}
					else
					{
						string outOfRangeDatesMessage = string.Join(", ", outOfRangeDates);
						test.Log(Status.Fail, $"Some bill dates are out of range: {outOfRangeDatesMessage}");
					}
					Thread.Sleep(2000);


					IJavaScriptExecutor js = (IJavaScriptExecutor)ManageDriver.driver;
					js.ExecuteScript($"sessionStorage.setItem('dateFrom', '{report.dateFrom}');");
					js.ExecuteScript($"sessionStorage.setItem('dateTo', '{report.dateTo}');");

					js.ExecuteScript("window.history.go(-1);");
					Thread.Sleep(3000);

					// Retrieve and reapply dates after navigating back
					string dateFrom = (string)js.ExecuteScript("return sessionStorage.getItem('dateFrom');");
					string dateTo = (string)js.ExecuteScript("return sessionStorage.getItem('dateTo');");
					reportPage.SetDateFrom(dateFrom);
					reportPage.SetDateTo(dateTo);
				}


				test.Log(Status.Info, "The bills in right time  period");

			}
			catch (Exception ex)
			{
				test.Fail(ex.Message);
				string screenShotPath = CommonMethods.TakeScreenShot();
				test.AddScreenCaptureFromPath(screenShotPath);
			}

		}


	}
}
