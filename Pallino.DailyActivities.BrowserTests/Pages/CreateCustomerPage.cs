using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Pallino.DailyActivities.BrowserTests.Pages
{
    public class CreateCustomerPage: Page
    {
        [FindsBy(How = How.Id, Using = "Name")]
        public IWebElement Name { get; set; }
        [FindsBy(How = How.Id, Using = "VATNumber")]
        public IWebElement VATNumber { get; set; }
        [FindsBy(How = How.Id, Using = "SubmitBtn")]
        public IWebElement SubmitBtn { get; set; }

        public CreateCustomerPage(IWebDriver driver) : base(driver)
        {
            if (!driver.PageSource.Contains("Server Error") && driver.Title != "Nuovo cliente")
            {
                throw new ArgumentException("This is not the create customer page: " + driver.Title);
            }
        }

        public CustomerListPage CreateCustomer(string name, string vat)
        {
            Name.SendKeys(name);
            VATNumber.SendKeys(vat);
            SubmitBtn.Click();
            var page = new CustomerListPage(driver);
            return page;
        }
    }
}
