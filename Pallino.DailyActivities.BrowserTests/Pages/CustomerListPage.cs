using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Pallino.DailyActivities.BrowserTests.Pages
{
    public class CustomerListPage: Page
    {
        [FindsBy(How = How.LinkText, Using = "Nuovo cliente")]
        public IWebElement CreateCustomerLink { get; set; }

        public CustomerListPage(IWebDriver driver) : base(driver)
        {
            if (!driver.PageSource.Contains("Server Error") && driver.Title != "Clienti")
            {
                throw new ArgumentException("This is not the customer list page: " + driver.Title);
            }
        }

        public CreateCustomerPage OpenCreateCustomerPage()
        {
            this.CreateCustomerLink.Click();
            var page = new CreateCustomerPage(this.driver);
            return page;
        }

        public bool ContainsCustomerRow(string surname)
        {
            var result = false;
            var customers = driver.FindElements(By.XPath(@"//*[@id=""body""]/section/table/tbody/tr/td[1]"));

            result = customers.Any(x =>x.Text == surname);
            return result;
        }

    }
}
