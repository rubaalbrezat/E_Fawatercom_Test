using E_Fawatercom_Test.Helpers;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Fawatercom_Test.POM
{
	public class BillerPage
	{

		IWebDriver _driver;

		public BillerPage(IWebDriver driver)
		{
			_driver = driver;
		}

		By billName = By.XPath("//input[@formcontrolname=\"Billname\"]");
		By email = By.XPath("//input[@formcontrolname=\"Email\"]");
		By location = By.XPath("//input[@formcontrolname=\"Location\"]");
		By createButton = By.XPath("//button[contains(text(), 'create')]");

		public void EnterBillName(string value)
		{
			IWebElement element = CommonMethods.WaitAndFindElement(billName);
			CommonMethods.HighlightElement(element);
			element.SendKeys(value);
		}

		public void EnterEmail(string value)
		{
			IWebElement element = CommonMethods.WaitAndFindElement(email);
			CommonMethods.HighlightElement(element);
			element.SendKeys(value);
		}

		public void EnterLocation(string value)
		{
			IWebElement element = CommonMethods.WaitAndFindElement(location);
			CommonMethods.HighlightElement(element);
			element.SendKeys(value);
		}

		public void ClickCreateButton()
		{
			IWebElement element = CommonMethods.WaitAndFindElement(createButton);
			CommonMethods.HighlightElement(element);
			element.Click();
		}
	}
}
