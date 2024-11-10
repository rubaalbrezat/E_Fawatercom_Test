using Bytescout.Spreadsheet;
using Bytescout.Spreadsheet.COM;
using E_Fawatercom_Test.Helpers;
using OpenQA.Selenium;
using RazorEngine.Compilation.ImpromptuInterface.Optimization;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace E_Fawatercom_Test.POM
{
	public class ReportPage
	{
		IWebDriver _driver;

		public ReportPage(IWebDriver driver)
		{
			_driver = driver;
		}

		
		By dateFrom = By.XPath("//input[@formcontrolname='DateFrom']");
		By dateTo = By.XPath("//input[@formcontrolname='DateTo']");
		By totalProfit = By.XPath("//table/tbody/tr[2]/th[2]");
		IList<IWebElement> detailsButtons => _driver.FindElements(By.XPath("//td//button"));
		


		public IList<IWebElement> GetCategoriesSelected()
		{

			IList<IWebElement> categories = _driver.FindElements(By.XPath("//select//option"));

			// Skip the first row if it's the header
			if (categories.Count > 0)
			{
				// Assuming the first row is always the header
				return categories.Skip(1).ToList();
			}

			return categories;


		}
		public void GetRandomCategoryFromSelect()
		{
			try
			{
				IList<IWebElement> categories = GetCategoriesSelected();

				Random rand = new Random();
				int randomIndex = rand.Next(categories.Count);

				IWebElement selecteCategory = categories[randomIndex];

				CommonMethods.HighlightElement(selecteCategory);
				selecteCategory.Click();
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}


		public void SetDateFrom(string value)
		{


			IWebElement element = CommonMethods.WaitAndFindElement(dateFrom);
			CommonMethods.HighlightElement(element);
			element.SendKeys(value);

		}



		public void SetDateTo(string value)
		{
		

			try
			{
				IWebElement element2 = CommonMethods.WaitAndFindElement(dateTo);

				if (string.IsNullOrEmpty(value))
				{
					return;
				}

				IJavaScriptExecutor jsExecutor = (IJavaScriptExecutor)ManageDriver.driver;
				DateTime parsedDate = DateTime.ParseExact(value, "MM-dd-yyyy", CultureInfo.InvariantCulture);

				string formattedDate = parsedDate.ToString("yyyy-MM-dd");
				CommonMethods.HighlightElement(element2);
				jsExecutor.ExecuteScript("arguments[0].value = arguments[1];", element2, formattedDate); Thread.Sleep(3000);
				

				element2.SendKeys(Keys.ArrowUp);
				element2.SendKeys(Keys.ArrowDown);
				

			}
			catch (Exception ex)
			{

				Console.WriteLine(ex.Message);
			}

		}


		public void ClickDetailsButton(int index)
		{
			
			detailsButtons[index].Click();
		}

		public int GetDetailsButtonCount()
		{
			return detailsButtons.Count;
		}


		public List<DateTime> GetDisplayedBillDates()
		{
			

				List<DateTime> dates = new List<DateTime>();
				var dateElements = _driver.FindElements(By.XPath("//tr//td[4]")); // Adjust selector as needed

				foreach (var element in dateElements)
				{
					// Assuming each element's text is a date string like "Mar 4, 2022"
					string dateString = element.Text;

				// Convert string to DateTime
				DateTime paymentDate = DateTime.ParseExact(dateString, "MMM d, yyyy", CultureInfo.InvariantCulture);
				string formattedDate = paymentDate.ToString("MM-dd-yyyy");

				dates.Add(DateTime.ParseExact(formattedDate, "MM-dd-yyyy", CultureInfo.InvariantCulture)); // Format as needed, e.g., "MM/dd/yyyy"
				
				}
			
		
			return dates;
			
			
		}


		
	}
}
