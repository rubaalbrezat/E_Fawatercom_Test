using E_Fawatercom_Test.Helpers;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Fawatercom_Test.POM
{
	public class ProfilePage
	{
		IWebDriver _driver;

		public ProfilePage(IWebDriver diver)
		{
			_driver = diver;
		}

		By fullName = By.XPath("//input[@formcontrolname=\"FullName\"]");
		By phoneNumber = By.XPath("//input[@formcontrolname=\"PhoneNumber\"]");
		By email = By.XPath("//input[@formcontrolname=\"Email\"]");
		By currentPassword = By.XPath("//input[@formcontrolname=\"password\"]");
		By address = By.XPath("//input[@formcontrolname=\"address\"]");
		By userName = By.XPath("//input[@formcontrolname=\"username\"]");
		By newPassword = By.XPath("//input[@formcontrolname=\"curenrpassword\"]");
		/*By profilePicture = By.XPath("//input[@type='file']");*/
		By updateButton = By.XPath("//button[contains(text(), 'Update')]");

		public void EntertFullName(string value)
		{
			IWebElement element = CommonMethods.WaitAndFindElement(fullName);
			CommonMethods.HighlightElement(element);
			element.SendKeys(value);
		}

		public void EnterPhoneNumber(string value)
		{
			IWebElement element = CommonMethods.WaitAndFindElement(phoneNumber);
			CommonMethods.HighlightElement(element);
			element.SendKeys(value);
		}

		public void Entertemail(string value)
		{
			IWebElement element = CommonMethods.WaitAndFindElement(email);
			CommonMethods.HighlightElement(element);
			element.SendKeys(value);
		}

		public void EntertCurrentPassword(string value)
		{
			IWebElement element = CommonMethods.WaitAndFindElement(currentPassword);
			CommonMethods.HighlightElement(element);
			element.SendKeys(value);
		}

		public void Enteraddress(string value)
		{
			IWebElement element = CommonMethods.WaitAndFindElement(address);
			CommonMethods.HighlightElement(element);
			element.SendKeys(value);
		}

		public void EntertUserName(string value)
		{
			IWebElement element = CommonMethods.WaitAndFindElement(userName);
			CommonMethods.HighlightElement(element);
			element.SendKeys(value);
		}



		public void ClickUpdateButton()
		{
			IWebElement element = CommonMethods.WaitAndFindElement(updateButton);
			CommonMethods.HighlightElement(element);
			element.Click();
		}
	}
}
