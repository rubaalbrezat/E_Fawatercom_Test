using E_Fawatercom_Test.Helpers;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Fawatercom_Test.POM
{
	public class LoginPage
	{
		IWebDriver _driver;

		public LoginPage(IWebDriver driver)
		{
			_driver = driver;
		}

		By username = By.XPath("//input[@type='email']");
		By password = By.XPath("//input[@type='password']");
		By signinButton = By.XPath("/html/body/app-root/app-login/div/div/div[1]/form/button");


		public void EntertUserName(string value)
		{
			IWebElement element = CommonMethods.WaitAndFindElement(username);
			CommonMethods.HighlightElement(element);
			element.SendKeys(value);
		}

		public void EnterPassword(string value)
		{
			IWebElement element = CommonMethods.WaitAndFindElement(password);
			CommonMethods.HighlightElement(element);
			element.SendKeys(value);
		}

		public void ClickSigninButton()
		{
			IWebElement element = CommonMethods.WaitAndFindElement(signinButton);
			CommonMethods.HighlightElement(element);
			element.Click();
		}
	}
}
