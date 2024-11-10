using AventStack.ExtentReports.Reporter;
using AventStack.ExtentReports;
using E_Fawatercom_Test.Assest_Methods;
using E_Fawatercom_Test.Data;
using E_Fawatercom_Test.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace E_Fawatercom_Test.Test_Methods
{
	[TestClass]
	public class UpdateProfile_TestMethod
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
		public void UpdateFullNameWithValidData()
		{
			var test = extentReports.CreateTest("Valid Update FullName", "Verfiy admmin Can Update Full Name successfully");
			try
			{
				Login_TestMethod login_TestMethod = new Login_TestMethod();
				login_TestMethod.Login_Admin();
				test.Log(Status.Info, "Login successfully");

				Thread.Sleep(2000);

				CommonMethods.NavigateToURL(GlobalConstants.updateProfileLink);
				Admin admin = UpdateProfile_AssistantMethods.ReadProfileDataFromExcle(2);
				test.Log(Status.Info, "Read Profile Info From Excel Passed");

				UpdateProfile_AssistantMethods.FillUpdateProfileForm(admin);
				test.Log(Status.Info, "Fill Update Profile Form Passed");

				Thread.Sleep(2000);

				Assert.IsTrue(UpdateProfile_AssistantMethods.CheackUpdateifPassedInDataBase(admin.fullName));
				test.Pass("Full Name Updated Successfully");
			}
			catch (Exception ex)
			{
				test.Fail(ex.Message);
				string screenShotPath = CommonMethods.TakeScreenShot();
				test.AddScreenCaptureFromPath(screenShotPath);

			}


		}
		[TestMethod]
		public void UpdateFullNameWithMaximumAllowedCharacters()
		{
			var test = extentReports.CreateTest("Update FullName With Maximum Allowed Characters", "Verfiy admmin Can Update Full Name successfully With Maximum Allowed Characters(100)");
			try
			{
				Login_TestMethod login_TestMethod = new Login_TestMethod();
				login_TestMethod.Login_Admin();
				test.Log(Status.Info, "Login successfully");

				Thread.Sleep(2000);

				CommonMethods.NavigateToURL(GlobalConstants.updateProfileLink);
				Admin admin = UpdateProfile_AssistantMethods.ReadProfileDataFromExcle(3);
				test.Log(Status.Info, "Read Profile Info From Excel Passed");

				UpdateProfile_AssistantMethods.FillUpdateProfileForm(admin);
				test.Log(Status.Info, "Fill Update Profile Form Passed");

				Thread.Sleep(2000);

				Assert.IsTrue(UpdateProfile_AssistantMethods.CheackUpdateifPassedInDataBase(admin.fullName));
				test.Pass("Full Name Updated Successfully");
			}
			catch (Exception ex)
			{
				test.Fail(ex.Message);
				string screenShotPath = CommonMethods.TakeScreenShot();
				test.AddScreenCaptureFromPath(screenShotPath);

			}


		}

		[TestMethod]
		public void UpdateFullNameExceedingMaximumCharacterLimit()
		{
			var test = extentReports.CreateTest("Update FullName With Exceeding Maximum Character Limit", "Verfiy admmin Can't Update Full Name successfully if it exceeds the specified character limit (more than 100 characters)");
			try
			{
				Login_TestMethod login_TestMethod = new Login_TestMethod();
				login_TestMethod.Login_Admin();
				test.Log(Status.Info, "Login successfully");

				Thread.Sleep(2000);

				CommonMethods.NavigateToURL(GlobalConstants.updateProfileLink);
				Admin admin = UpdateProfile_AssistantMethods.ReadProfileDataFromExcle(4);
				test.Log(Status.Info, "Read Profile Info From Excel Passed");

				UpdateProfile_AssistantMethods.FillUpdateProfileForm(admin);
				test.Log(Status.Info, "Fill Update Profile Form Passed");

				Thread.Sleep(2000);

				Assert.IsFalse(UpdateProfile_AssistantMethods.CheackUpdateifPassedInDataBase(admin.fullName));
				test.Pass("Full Name not updated successfully");

				try
				{
					var errorMessage = ManageDriver.driver.FindElement(By.XPath("//*[text()='*required']"));
					Assert.IsTrue(errorMessage.Displayed, "The Full Name required message is not displayed");
					test.Pass("Full Name is Required");
				}
				catch (NoSuchElementException)
				{
					test.Fail("Error: The system accepted changed name with Exceeding Maximum Character Limit in full name field");
					Assert.Fail("The system accepted changed name with Exceeding Maximum Character Limit full name field");
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
		public void UpdateFullNameWithEmptyField()
		{
			var test = extentReports.CreateTest("Update FullName With Empty Field", "Verfiy admmin Can't Update Full Name successfully when the Full Name field is left empty");
			try
			{
				Login_TestMethod login_TestMethod = new Login_TestMethod();
				login_TestMethod.Login_Admin();
				test.Log(Status.Info, "Login successfully");

				Thread.Sleep(2000);

				CommonMethods.NavigateToURL(GlobalConstants.updateProfileLink);
				Admin admin = UpdateProfile_AssistantMethods.ReadProfileDataFromExcle(5);
				test.Log(Status.Info, "Read Profile Info From Excel Passed");

				UpdateProfile_AssistantMethods.FillUpdateProfileForm(admin);
				test.Log(Status.Info, "Fill Update Profile Form Passed");

				Thread.Sleep(2000);
				Assert.IsFalse(UpdateProfile_AssistantMethods.CheackUpdateifPassedInDataBase(admin.fullName));
				test.Pass("Full Name not updated successfully");

				try
				{
					var errorMessage = ManageDriver.driver.FindElement(By.XPath("//*[text()='*required']"));
					Assert.IsTrue(errorMessage.Displayed, "The Full Name required message is not displayed");
					test.Pass("Full Name is Required");
				}
				catch (NoSuchElementException)
				{
					test.Fail("Error: The system accepted changed name without entering name in full name field and not show error messeage");
					Assert.Fail("The system accepted changed name without name in full name field");
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
		public void UpdateFullNameWithoutEnteringCurrentPassword()
		{
			var test = extentReports.CreateTest("Update FullName Without Entering Current Password", "Verfiy admmin Can't Update Full Name successfully when the Current Password field is left empty");
			try
			{
				Login_TestMethod login_TestMethod = new Login_TestMethod();
				login_TestMethod.Login_Admin();
				test.Log(Status.Info, "Login successfully");

				Thread.Sleep(2000);

				CommonMethods.NavigateToURL(GlobalConstants.updateProfileLink);
				Admin admin = UpdateProfile_AssistantMethods.ReadProfileDataFromExcle(6);
				test.Log(Status.Info, "Read Profile Info From Excel Passed");

				UpdateProfile_AssistantMethods.FillUpdateProfileForm(admin);
				test.Log(Status.Info, "Fill Update Profile Form Passed");

				Thread.Sleep(2000);
				Assert.IsFalse(UpdateProfile_AssistantMethods.CheackUpdateifPassedInDataBase(admin.fullName));
				test.Pass("Full Name not updated successfully");

				
				try
				{
					var errorMessage = ManageDriver.driver.FindElement(By.XPath("//*[text()='*required']"));
					Assert.IsTrue(errorMessage.Displayed, "The Full Name required message is not displayed");
					test.Pass("Current Password is Required");
				}
				catch (NoSuchElementException)
				{
					test.Fail("Error: The system accepted changed name without entering current password and not show the error messeage");
					Assert.Fail("The system accepted changed name without entering current password");
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
		public void UpdateFullNameWithIncorrectCurrentPassword()
		{
			var test = extentReports.CreateTest("Update FullName With Incorrect Current Password", "Verfiy admmin Can't Update Full Name successfully when the Current Password is Incorrect");
			try
			{
				Login_TestMethod login_TestMethod = new Login_TestMethod();
				login_TestMethod.Login_Admin();
				test.Log(Status.Info, "Login successfully");

				Thread.Sleep(2000);

				CommonMethods.NavigateToURL(GlobalConstants.updateProfileLink);
				Admin admin = UpdateProfile_AssistantMethods.ReadProfileDataFromExcle(7);
				test.Log(Status.Info, "Read Profile Info From Excel Passed");

				UpdateProfile_AssistantMethods.FillUpdateProfileForm(admin);
				test.Log(Status.Info, "Fill Update Profile Form Passed");

				Thread.Sleep(2000);


				Assert.IsFalse(UpdateProfile_AssistantMethods.CheackUpdateifPassedInDataBase(admin.fullName));
				test.Pass("Full Name not updated successfully");
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
