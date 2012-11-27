using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.PageObjects;

namespace Pallino.DailyActivities.BrowserTests.Pages
{
    public class HomePage: Page
    {
        [FindsBy(How=How.Id, Using="title")]
        public IWebElement Title { get; set; }

        [FindsBy(How=How.LinkText, Using="Customers")]
        public IWebElement CustomersLink { get; set; }

        public HomePage(IWebDriver driver): base(driver) { }

        public void HasTitleHeading()
        {
            Assert.AreEqual("Index", this.Title.Text);
        }

        public CustomerListPage OpenCustomersPage()
        { 
            this.CustomersLink.Click();
            return new CustomerListPage(this.driver);
        }
    }
}
