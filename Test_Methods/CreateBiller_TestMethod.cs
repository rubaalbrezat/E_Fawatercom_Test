using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports;
using E_Fawatercom_Test.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using E_Fawatercom_Test.Helpers;
using E_Fawatercom_Test.POM;
using E_Fawatercom_Test.Assest_Methods;
using System.Xml.Linq;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace E_Fawatercom_Test.Test_Methods
{
	[TestClass]
	public class CreateBiller_TestMethod
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
		public void CreateBiller_WithValidData()
		{
			var test = extentReports.CreateTest("Create Valid Bill ", "Verfiy admmin can Add Bill successfully With Valid Data");

			try
			{
				Login_TestMethod login_TestMethod = new Login_TestMethod();
				login_TestMethod.Login_Admin();
				test.Log(Status.Info, "Login successfully");

				Thread.Sleep(2000);


				Category_AssistantMethods.ChooseRandomCategoryAndClickCreatButton();
				test.Log(Status.Info, "Choose Category And Click Create Button successfully");

				Thread.Sleep(2000);

				Bill bill = Biller_AssistantMethods.ReadBillDataFromExcel(2);
				test.Log(Status.Info, "Read from Excel Passed ");

				Biller_AssistantMethods.FillBillForm(bill);
				test.Log(Status.Info, "Fill Biller Form Passed ");


				Assert.IsTrue(Biller_AssistantMethods.CheackBillInDataBase(bill.email), "Biller isn't Create successfully");
				test.Pass("Biller Is Create successfully");


			}
			catch (Exception ex)
			{
				test.Fail(ex.Message);
				string screenShotPath = CommonMethods.TakeScreenShot();
				test.AddScreenCaptureFromPath(screenShotPath);
			}
		}

		[TestMethod]
		public void CreateBillerWithMaximumAllowedCharactersInTheNameField()
		{
			var test = extentReports.CreateTest("Create Biller With Maximum Allowed Characters in name field ", "Verfiy admmin can Create biller successfully with maximum allowed characters in the name field(100) ");

			try
			{
				Login_TestMethod login_TestMethod = new Login_TestMethod();
				login_TestMethod.Login_Admin();
				test.Log(Status.Info, "Login successfully");

				Thread.Sleep(2000);

				Category_AssistantMethods.ChooseRandomCategoryAndClickCreatButton();
				test.Log(Status.Info, "Choose Category And Click Create Button successfully");

				Thread.Sleep(2000);

				Bill bill = Biller_AssistantMethods.ReadBillDataFromExcel(3);
				test.Log(Status.Info, "Read from Excel Passed ");

				Biller_AssistantMethods.FillBillForm(bill);
				test.Log(Status.Info, "Fill Biller Form Passed ");


				Assert.IsTrue(Biller_AssistantMethods.CheackBillInDataBase(bill.email), "Biller isn't Create successfully");
				test.Pass("Biller Is Create successfully");


			}
			catch (Exception ex)
			{
				test.Fail(ex.Message);
				string screenShotPath = CommonMethods.TakeScreenShot();
				test.AddScreenCaptureFromPath(screenShotPath);
			}
		}


		[TestMethod]
		public void CreateBillerWithExceedingMaximumCharacterLimitInNameField()
		{
			var test = extentReports.CreateTest("Create Biller With Maximum Allowed Characters in name field ", "Verify that the system prevents create biller if the billername field it exceeds the specified character limit more than 100 characters(101) ");

			try
			{
				Login_TestMethod login_TestMethod = new Login_TestMethod();
				login_TestMethod.Login_Admin();
				test.Log(Status.Info, "Login successfully");

				Thread.Sleep(2000);

				Category_AssistantMethods.ChooseRandomCategoryAndClickCreatButton();
				test.Log(Status.Info, "Choose Category And Click Create Button successfully");

				Thread.Sleep(2000);

				Bill bill = Biller_AssistantMethods.ReadBillDataFromExcel(4);
				test.Log(Status.Info, "Read from Excel Passed ");

				Biller_AssistantMethods.FillBillForm(bill);
				test.Log(Status.Info, "Fill Biller Form Passed ");

				Thread.Sleep(2000);
				Assert.IsFalse(Biller_AssistantMethods.CheackBillInDataBase(bill.email), "Biller is Create successfully");
				test.Pass("Biller isn't Create successfully");

				try
				{
					var errorMessage = ManageDriver.driver.FindElement(By.XPath("//*[text()='*required']"));
					Assert.IsTrue(errorMessage.Displayed, "The name required message is not displayed");
					test.Pass("Name is Required");
				}
				catch(NoSuchElementException)
				{
					test.Fail("Error: The system accepted create biller with exceeds the specified character limit more than 100 characters(101) in name field");
					Assert.Fail("The system accepted create biller with exceeds the specified character limit more than 100 characters(101) in name field");
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
		public void CreateBillerWithInValidEmailFormat()
		{
			var test = extentReports.CreateTest("Create Biller With Invalid Email Format ", "Verify that the system prevents create biller if email field have invalid format");

			try
			{
				Login_TestMethod login_TestMethod = new Login_TestMethod();
				login_TestMethod.Login_Admin();
				test.Log(Status.Info, "Login successfully");

				Thread.Sleep(2000);

				Category_AssistantMethods.ChooseRandomCategoryAndClickCreatButton();
				test.Log(Status.Info, "Choose Category And Click Create Button successfully");

				Thread.Sleep(2000);

				Bill bill = Biller_AssistantMethods.ReadBillDataFromExcel(5);
				test.Log(Status.Info, "Read from Excel Passed ");

				Biller_AssistantMethods.FillBillForm(bill);
				test.Log(Status.Info, "Fill Biller Form Passed ");

				Thread.Sleep(2000);
				Assert.IsFalse(Biller_AssistantMethods.CheackBillInDataBase(bill.email),"Biller is Create successfully");
				test.Pass("Biller isn't Create successfully");

				try
				{
					var errorMessage = ManageDriver.driver.FindElement(By.XPath("//*[text()='*emeil required']"));
					Assert.IsTrue(errorMessage.Displayed, "The email required message is not displayed");
					test.Pass("Email is Required");
				}
				catch (NoSuchElementException)
				{
					test.Fail("Error: The system accepted create biller with invalid email formate");
					Assert.Fail("The system accepted create biller with invalid email formate");
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
		public void CreateBillerWithMaximumAllowedCharactersInEmailField()
		{
			var test = extentReports.CreateTest("Create Biller with maximum allowed characters in Email field", "Verify that the system allows create biller with maximum allowed characters in email field(254) ");

			try
			{
				Login_TestMethod login_TestMethod = new Login_TestMethod();
				login_TestMethod.Login_Admin();
				test.Log(Status.Info, "Login successfully");

				Thread.Sleep(2000);

				Category_AssistantMethods.ChooseRandomCategoryAndClickCreatButton();
				test.Log(Status.Info, "Choose Category And Click Create Button successfully");

				Thread.Sleep(2000);

				Bill bill = Biller_AssistantMethods.ReadBillDataFromExcel(6);
				test.Log(Status.Info, "Read from Excel Passed ");

				Biller_AssistantMethods.FillBillForm(bill);
				test.Log(Status.Info, "Fill Biller Form Passed ");



				Assert.IsTrue(Biller_AssistantMethods.CheackBillInDataBase(bill.email), "Biller isn't Create successfully");
				test.Pass("Biller Create successfully");


			}
			catch (Exception ex)
			{
				test.Fail(ex.Message);
				string screenShotPath = CommonMethods.TakeScreenShot();
				test.AddScreenCaptureFromPath(screenShotPath);
			}
		}

		[TestMethod]
		public void CreateBillerExceedingMaximumCharacterLimitInEmailField()
		{
			var test = extentReports.CreateTest("Create Biller with Exceeding Maximum Character Limits in Email field", "Verify that the system prevents create biller with Exceed maximum  characters in email field(255) ");

			try
			{
				Login_TestMethod login_TestMethod = new Login_TestMethod();
				login_TestMethod.Login_Admin();
				test.Log(Status.Info, "Login successfully");

				Thread.Sleep(2000);

				Category_AssistantMethods.ChooseRandomCategoryAndClickCreatButton();
				test.Log(Status.Info, "Choose Category And Click Create Button successfully");

				Thread.Sleep(2000);

				Bill bill = Biller_AssistantMethods.ReadBillDataFromExcel(7);
				test.Log(Status.Info, "Read from Excel Passed ");

				Biller_AssistantMethods.FillBillForm(bill);
				test.Log(Status.Info, "Fill Biller Form Passed ");

				Thread.Sleep(2000);
				Assert.IsFalse(Biller_AssistantMethods.CheackBillInDataBase(bill.email), "Biller is Create successfully");
				test.Pass("Biller isn't Create successfully");

				try
				{
					var errorMessage = ManageDriver.driver.FindElement(By.XPath("//*[text()='*emeil required']"));
					Assert.IsTrue(errorMessage.Displayed, "The email required message is not displayed");
					test.Pass("Email is Required");
				}
				catch (NoSuchElementException)
				{
					test.Fail("Error: The system accepted create biller with Exceed maximum  characters in email field(255)");
					Assert.Fail("The system accepted create biller with Exceed maximum  characters in email field(255)");
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
		public void CreateBillerWithEmailContainSpaces()
		{
			var test = extentReports.CreateTest("Create Biller with email contain spaces", "Verify that the system prevents create biller if the Email  contain spaces ");

			try
			{
				Login_TestMethod login_TestMethod = new Login_TestMethod();
				login_TestMethod.Login_Admin();
				test.Log(Status.Info, "Login successfully");

				Thread.Sleep(2000);

				Category_AssistantMethods.ChooseRandomCategoryAndClickCreatButton();
				test.Log(Status.Info, "Choose Category And Click Create Button successfully");

				Thread.Sleep(2000);

				Bill bill = Biller_AssistantMethods.ReadBillDataFromExcel(8);
				test.Log(Status.Info, "Read from Excel Passed ");

				Biller_AssistantMethods.FillBillForm(bill);
				test.Log(Status.Info, "Fill Biller Form Passed ");

				Thread.Sleep(2000);
				Assert.IsFalse(Biller_AssistantMethods.CheackBillInDataBase(bill.email) ,"Biller is Create successfully");
				test.Pass("Biller isn't Create successfully");

				try
				{
					var errorMessage = ManageDriver.driver.FindElement(By.XPath("//*[text()='*emeil required']"));
					Assert.IsTrue(errorMessage.Displayed, "The email required message is not displayed");
					test.Pass("Email is Required");
				}
				catch(NoSuchElementException)
				{
					test.Fail("Error: The system accepted the Email  contain spaces ");
					Assert.Fail("The system accepted the Email  contain spaces ");

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
		public void CreateBillerWithMaximumAllowedCharactersInTheLocationField()
		{
			var test = extentReports.CreateTest("Create Biller With Maximum Allowed Characters in Location field ", "Verfiy admmin can Create biller successfully with maximum allowed characters in the location field(100) ");

			try
			{
				Login_TestMethod login_TestMethod = new Login_TestMethod();
				login_TestMethod.Login_Admin();
				test.Log(Status.Info, "Login successfully");

				Thread.Sleep(2000);

				Category_AssistantMethods.ChooseRandomCategoryAndClickCreatButton();
				test.Log(Status.Info, "Choose Category And Click Create Button successfully");

				Thread.Sleep(2000);

				Bill bill = Biller_AssistantMethods.ReadBillDataFromExcel(9);
				test.Log(Status.Info, "Read from Excel Passed ");

				Biller_AssistantMethods.FillBillForm(bill);
				test.Log(Status.Info, "Fill Biller Form Passed ");


				Assert.IsTrue(Biller_AssistantMethods.CheackBillInDataBase(bill.email), "Biller isn't Create successfully");
				test.Pass("Biller Is Create successfully");


			}
			catch (Exception ex)
			{
				test.Fail(ex.Message);
				string screenShotPath = CommonMethods.TakeScreenShot();
				test.AddScreenCaptureFromPath(screenShotPath);
			}
		}

		[TestMethod]
		public void CreateBillerWithExceedingMaximumCharacterLimitInLocationField()
		{
			var test = extentReports.CreateTest("Create Biller With Maximum Allowed Characters in Location field ", "Verify that the system prevents create biller if the location field it exceeds the specified character limit more than 100 characters(101) ");

			try
			{
				Login_TestMethod login_TestMethod = new Login_TestMethod();
				login_TestMethod.Login_Admin();
				test.Log(Status.Info, "Login successfully");

				Thread.Sleep(2000);

				Category_AssistantMethods.ChooseRandomCategoryAndClickCreatButton();
				test.Log(Status.Info, "Choose Category And Click Create Button successfully");

				Thread.Sleep(2000);

				Bill bill = Biller_AssistantMethods.ReadBillDataFromExcel(10);
				test.Log(Status.Info, "Read from Excel Passed ");

				Biller_AssistantMethods.FillBillForm(bill);
				test.Log(Status.Info, "Fill Biller Form Passed ");

				Thread.Sleep(2000);
				Assert.IsFalse(Biller_AssistantMethods.CheackBillInDataBase(bill.email), "Biller is Create successfully");
				test.Pass("Biller isn't Create successfully");

				try
				{
					var errorMessage = ManageDriver.driver.FindElement(By.XPath("//*[text()='*required']"));
					Assert.IsTrue(errorMessage.Displayed, "The location required message is not displayed");
					test.Pass("location is Required");
				}
				catch (NoSuchElementException)
				{
					test.Fail("Error: The system accepted a location field it exceeds the specified character limit more than 100 characters(101)");
					Assert.Fail("The system accepted a location field it exceeds the specified character limit more than 100 characters(101)");
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
		public void CheackCreateButtonIsDisableWhenLeaveNameFieldEmpty()
		{
			var test = extentReports.CreateTest("Cheack Create Button Is Disable and show error messeage when leave name field empty ", "Verify the system reject create biller and disable create button,show error messeage when leave name field empty ");

			try
			{
				Login_TestMethod login_TestMethod = new Login_TestMethod();
				login_TestMethod.Login_Admin();
				test.Log(Status.Info, "Login successfully");

				Thread.Sleep(2000);

				Category_AssistantMethods.ChooseRandomCategoryAndClickCreatButton();
				test.Log(Status.Info, "Choose Category And Click Create Button successfully");

				Thread.Sleep(2000);

				Bill bill = Biller_AssistantMethods.ReadBillDataFromExcel(11);
				test.Log(Status.Info, "Read from Excel Passed ");

				Biller_AssistantMethods.FillBillForm(bill);
				test.Log(Status.Info, "Fill Biller Form Passed ");

				Thread.Sleep(2000);

				try
				{
					var createButton = ManageDriver.driver.FindElement(By.XPath("//button[contains(text(), 'create')]"));
					Assert.IsFalse(createButton.Enabled, "The create button is should be disable when name field empty");
					test.Pass("button is disable");

					var errorMessage = ManageDriver.driver.FindElement(By.XPath("//*[text()='*required']"));
					Assert.IsTrue(errorMessage.Displayed, "The name required message is not displayed");
					test.Pass("Name is Required");
				}
				catch (NoSuchElementException)
				{
					test.Fail("Error: The create button isn't disable when leave name field empty and dont show error messeage");
					Assert.Fail("The system accepted create biller when leave name field empty and dont show error messeage");
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
		public void CreateBillerWithDublicateBillerName()
		{
			var test = extentReports.CreateTest("Create Biller With Dublicate in name field ", "Verify that the system prevents create biller if the billername field have dublicate name");

			try
			{
				

				Login_TestMethod login_TestMethod = new Login_TestMethod();
				login_TestMethod.Login_Admin();
				test.Log(Status.Info, "Login successfully");

				Thread.Sleep(2000);

				Category_AssistantMethods.ChooseRandomCategoryAndClickCreatButton();
				test.Log(Status.Info, "Choose Category And Click Create Button successfully");

				Thread.Sleep(2000);

				Bill bill = Biller_AssistantMethods.ReadBillDataFromExcel(12);
				test.Log(Status.Info, "Read from Excel Passed ");

				Biller_AssistantMethods.FillBillForm(bill);
				test.Log(Status.Info, "Fill Biller Form Passed ");

				Thread.Sleep(2000);
				Assert.IsFalse(Biller_AssistantMethods.CheackBillInDataBase(bill.email), "Biller is Create successfully");
				test.Pass("Biller isn't Create successfully");

				try
				{
					var errorMessage = ManageDriver.driver.FindElement(By.XPath("//*[text()='*required']"));
					Assert.IsTrue(errorMessage.Displayed, "The name required message is not displayed");
					test.Pass("Duplicate name error message displayed as expected");
				}
				catch (NoSuchElementException)
				{
					// If error message is not displayed, fail the test as the system incorrectly accepted the duplicate name
					test.Fail("Error: The system accepted a duplicate name when it should have rejected it.");
					Assert.Fail("The system accepted a duplicate name, which is not the expected behavior.");
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
		public void CheackCreateButtonIsDisableWhenLeaveEmailFieldEmpty()
		{
			var test = extentReports.CreateTest("Cheack Create Button Is Disable and show error messeage when leave email field empty ", "Verify the system reject create biller and disable create button,show error messeage when leave name field empty ");

			try
			{
				Login_TestMethod login_TestMethod = new Login_TestMethod();
				login_TestMethod.Login_Admin();
				test.Log(Status.Info, "Login successfully");

				Thread.Sleep(2000);

				Category_AssistantMethods.ChooseRandomCategoryAndClickCreatButton();
				test.Log(Status.Info, "Choose Category And Click Create Button successfully");

				Thread.Sleep(2000);

				Bill bill = Biller_AssistantMethods.ReadBillDataFromExcel(13);
				test.Log(Status.Info, "Read from Excel Passed ");

				Biller_AssistantMethods.FillBillForm(bill);
				test.Log(Status.Info, "Fill Biller Form Passed ");

				try
				{
					var createButton = ManageDriver.driver.FindElement(By.XPath("//button[contains(text(), 'create')]"));
					Assert.IsFalse(createButton.Enabled, "The create button is should be disable when email field empty");
					test.Pass("button is disable");

					var errorMessage = ManageDriver.driver.FindElement(By.XPath("//*[text()='*required']"));
					Assert.IsTrue(errorMessage.Displayed, "The name required message is not displayed");
					test.Pass("Email is Required");
				}
				catch (NoSuchElementException)
				{
					test.Fail("Error: The create button isn't disable when leave email field empty and dont show error messeage");
					Assert.Fail("The system accepted create biller when leave email field empty and dont show error messeage");
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
		public void CheackCreateButtonIsDisableWhenLeaveLocationFieldEmpty()
		{
			var test = extentReports.CreateTest("Cheack Create Button Is Disable and show error messeage when leave location field empty ", "Verify the system reject create biller and disable create button,show error messeage when leave location field empty ");

			try
			{
				Login_TestMethod login_TestMethod = new Login_TestMethod();
				login_TestMethod.Login_Admin();
				test.Log(Status.Info, "Login successfully");

				Thread.Sleep(2000);

				Category_AssistantMethods.ChooseRandomCategoryAndClickCreatButton();
				test.Log(Status.Info, "Choose Category And Click Create Button successfully");

				Thread.Sleep(2000);

				Bill bill = Biller_AssistantMethods.ReadBillDataFromExcel(14);
				test.Log(Status.Info, "Read from Excel Passed ");

				Biller_AssistantMethods.FillBillForm(bill);
				test.Log(Status.Info, "Fill Biller Form Passed ");

				try
				{
					var createButton = ManageDriver.driver.FindElement(By.XPath("//button[contains(text(), 'create')]"));
					Assert.IsFalse(createButton.Enabled, "The create button is should be disable when location field empty");
					test.Pass("button is disable");

					var errorMessage = ManageDriver.driver.FindElement(By.XPath("//*[text()='*required']"));
					Assert.IsTrue(errorMessage.Displayed, "The name required message is not displayed");
					test.Pass("Email is Required");
				}
				catch (NoSuchElementException)
				{
					test.Fail("Error: The create button isn't disable when leave location field empty and dont show error messeage");
					Assert.Fail("The system accepted create biller when leave location field empty and dont show error messeage");
				}


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
