using AventStack.ExtentReports;
using AventStack.ExtentReports.Model;
using AventStack.ExtentReports.Reporter;
using E_Fawatercom_Test.Assest_Methods;
using E_Fawatercom_Test.Data;
using E_Fawatercom_Test.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Fawatercom_Test.Test_Methods
{
	[TestClass]
	public class Login_TestMethod
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
		public void Login_Admin()
		{
			var test = extentReports.CreateTest("Valid_Login", "Verfiy admmin can login successfully");
			try
			{
				CommonMethods.NavigateToURL(GlobalConstants.loginPageLink);
				Admin admin = Login_AssistantMethods.ReadLoginDataFromExcle(2);
				test.Log(Status.Info, "Read Login Info From Excel Passed");

				Login_AssistantMethods.FillLoginForm(admin);
				test.Log(Status.Info, "Fill Login Form Passed");
				Thread.Sleep(5000);

				var expectedURL = GlobalConstants.homePageLink;
				var actualURL = ManageDriver.driver.Url;
				Assert.AreEqual(expectedURL, actualURL, $"Expected URL: {expectedURL}, but got: {actualURL}, Test Case: TC1");
				test.Pass("Admin Login Successfully");
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
