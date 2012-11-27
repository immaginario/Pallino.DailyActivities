using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;

namespace Pallino.DailyActivities.BrowserTests
{
    public class Helper
    {
        public static IWebDriver StartBrowser()
        {
            var driver = new FirefoxDriver();
            driver.Navigate().GoToUrl("http://localhost:50359/");
            return driver;
        }
    }
}
