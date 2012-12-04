using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.SqlServer.Management.Smo;
using Microsoft.SqlServer.Management.Common;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using Pallino.DailyActivities.BrowserTests.Pages;
using SharpTestsEx;

namespace Pallino.DailyActivities.BrowserTests
{

    [TestFixture]
    public class ManageCustomersTests
    {
        IWebDriver driver;

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            var sqlScriptPath = AppDomain.CurrentDomain.BaseDirectory + "..\\..\\..\\script.sql";
            var mdfDirPath = AppDomain.CurrentDomain.BaseDirectory + "..\\..\\..\\..\\Pallino.DailyActivities.WebApp\\App_Data\\";
            var solDir = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent;
            var mdfDir = Path.Combine(solDir.FullName, "Pallino.DailyActivities.WebApp", "App_Data");
            var mdfName = "PallinoDailyActivities";
            string sqlConnectionString = string.Format("Data Source=(LocalDb)\\v11.0;Initial Catalog={0};Integrated Security=SSPI;AttachDBFilename={1}\\{0}.mdf", mdfName, mdfDir);

            FileInfo file = new FileInfo(sqlScriptPath);

            string script = file.OpenText().ReadToEnd();

            SqlConnection conn = new SqlConnection(sqlConnectionString);

            Server server = new Server(new ServerConnection(conn));

            server.ConnectionContext.ExecuteNonQuery(script);
            file.OpenText().Close();

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

            customersList = createPage.CreateCustomer("IBM corp.", "12345678902") as CustomerListPage;

            customersList.Should().Not.Be.Null();
            var containsCustomer = customersList.ContainsCustomerRow("IBM corp.");
            Assert.IsTrue(containsCustomer);
        }
        [Test]
        [Ignore]
        public void CantAddACustomerWithInvalidVatNumber()
        {
            var homePage = new HomePage(driver);
            var customersListPage = homePage.OpenCustomersPage();
            var createPage = customersListPage.OpenCreateCustomerPage();

            createPage = createPage.CreateCustomer("IBM corp.", "12345678902hhhh") as CreateCustomerPage;
            createPage.Should().Not.Be.Null();
            createPage.ContainsVatRegExValidation().Should().Be.True();

        }
    }
}
