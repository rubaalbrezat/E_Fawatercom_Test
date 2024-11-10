using E_Fawatercom_Test.Helpers;
using E_Fawatercom_Test.POM;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Fawatercom_Test.Assest_Methods
{
	public class Category_AssistantMethods
	{
		public static void ChooseRandomCategoryAndClickCreatButton()
		{
			CategoryPage categoryPage = new CategoryPage(ManageDriver.driver);
			categoryPage.SelectRandomCategoryAndCreateBill();

		
			
		}
	}
}
