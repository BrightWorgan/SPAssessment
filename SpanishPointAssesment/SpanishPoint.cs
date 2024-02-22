
namespace SpanishPointAssesment
{

    using NUnit.Framework;

    using OpenQA.Selenium;

    using OpenQA.Selenium.Chrome;

    using OpenQA.Selenium.Support.UI;
    using System;

    namespace SeleniumCsharp

    {

        public class Tests

        {
            string url = "https://www.spanishpoint.ie/";

            string expectedDMSText = "Our Document Management System is a comprehensive suite of tools and technologies specifically designed to help organisations streamline and optimise their document management processes.";

            IWebDriver driver;
            WebDriverWait waiter;

            [OneTimeSetUp]
            public void init()
            {
                driver = new ChromeDriver();

                waiter = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
                driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(5);
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
            }


            // sets up pre-test criteria
            [SetUp]
            public void SetupTest()
            {
                driver.Navigate().GoToUrl(url);
                AcceptCookies();
            }

            // function to accept coookies if the pop-up displays
            public void AcceptCookies()

            {
                var cookieButton = driver.FindElement(By.Id("wt-cli-accept-btn"));
                cookieButton.Click();

            }

            public void VerifyTitle()
            {
                waiter.Until(driver =>
                {
                    var text = driver.FindElement(By.CssSelector("#document-management-system h2"));
                    return text.Text.Contains("Document Management System");
                });
            }

            [Test]

            public void VerifyDocumentManagementSystemText()

            {
                // used Find Element by LinkText, as the IDs seemed to be randomly generated, normally I would used IDs
                // Solutions & Services
                var solutionsAndServices = driver.FindElement(By.LinkText("Solutions & Services"));
                Assert.IsTrue(solutionsAndServices.Displayed);
                solutionsAndServices.Click();

                // Modern Work
                var modernWork = driver.FindElement(By.LinkText("Modern Work"));
                Assert.IsTrue(modernWork.Displayed);
                modernWork.Click();

                // Document Management System
                var headerLink = driver.FindElement(By.CssSelector("a[href*='document-management-system']"));
                headerLink.Click();

                // Wait until selected tab shows title text
                VerifyTitle();

                var documentManagementSystem = driver.FindElement(By.Id("document-management-system"));
                var activeHeader = driver.FindElement(By.ClassName("vc_active"));
                Assert.AreEqual(activeHeader.Text, "Document Management System");


                var documentManagementText = documentManagementSystem.FindElement(By.CssSelector("p"));
                Assert.AreEqual(documentManagementText.Text, expectedDMSText);
            }


            [OneTimeTearDown]
            public void TearDown()

            {
                driver.Quit();
            }

        }

    }


}