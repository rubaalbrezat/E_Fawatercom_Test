using E_Fawatercom_Test.Helpers;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Fawatercom_Test.POM
{
	public class CategoryPage
	{
		IWebDriver _driver;

		public CategoryPage(IWebDriver driver)
		{
			_driver = driver;
		}

		public IList<IWebElement> GetCategoriesTableRows()
		{
			
			IList<IWebElement> rows = _driver.FindElements(By.XPath("//table//tr"));

			// Skip the first row if it's the header
			if (rows.Count > 0)
			{
		   // Assuming the first row is always the header
				return rows.Skip(1).ToList();
			}

			return rows;
			


			
			
		}

		public  void SelectRandomCategoryAndCreateBill()
		{
			try
			{

				IList<IWebElement> rows = GetCategoriesTableRows();

				Random rand = new Random();
				int randomIndex = rand.Next(rows.Count);

				IWebElement selectedRows = rows[randomIndex];

				IWebElement createButton = selectedRows.FindElement(By.XPath(".//button[contains(text(), 'Create')]"));
				CommonMethods.HighlightElement(createButton);
				createButton.Click();
			}
			catch (Exception ex) 
			{ 
				Console.WriteLine(ex.Message);
			}

		}
	}
}
