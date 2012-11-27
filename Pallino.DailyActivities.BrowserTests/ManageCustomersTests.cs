using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using Pallino.DailyActivities.BrowserTests.Pages;

namespace Pallino.DailyActivities.BrowserTests
{

    [TestFixture]
    public class ManageCustomersTests
    {
        IWebDriver driver;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            driver = Helper.StartBrowser();
        }
        
        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            driver.Quit();
        }

        [Test]
        public void CanAddACustomer()
        {
            var homePage = new HomePage(driver);
            var customersList = homePage.OpenCustomersPage();
            var createPage = customersList.OpenCreateCustomerPage();

            customersList = createPage.CreateCustomer("mario", "12345678902");

            var containsCustomer = customersList.ContainsCustomerRow("mario");
            Assert.IsTrue(containsCustomer);
        }
    }
}
