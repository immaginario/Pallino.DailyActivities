using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using Pallino.DailyActivities.BrowserTests.Pages;

namespace Pallino.DailyActivities.BrowserTests
{
    [TestFixture]
    public class HomePageTests
    {
        IWebDriver driver;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            driver = StartBrowser();
        }

        private IWebDriver StartBrowser()
        {
            var driver = new FirefoxDriver();
            driver.Navigate().GoToUrl("http://localhost:50359/");
            return driver;
        }

        [TestFixtureTearDown]
        public void TestFixtureTearDown()
        {
            driver.Quit();
        }

        [Test]
        public void CanAccessHomePage()
        {
            var homePage = new HomePage(driver);

            Assert.AreEqual("Index", homePage.Title.Text);
        }
        [Test]
        public void CanAccessHomePageEnc()
        {
            var homePage = new HomePage(driver);

            homePage.HasTitleHeading();
        }

    }
}
