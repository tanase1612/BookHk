using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTests
{
    [TestFixture]
    public class CreateBookingTest
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;
        private bool acceptNextAlert = true;
        
        [SetUp]
        public void SetupTest()
        {
            driver = new FirefoxDriver();
            baseURL = "http://localhost:1247/";
            verificationErrors = new StringBuilder();
        }
        
        [TearDown]
        public void TeardownTest()
        {
            try
            {
                driver.Quit();
            }
            catch (Exception)
            {
                // Ignore errors if unable to close the browser
            }
            Assert.AreEqual("", verificationErrors.ToString());
        }
        
        [Test]
        public void TheCreateBookingTest()
        {
            driver.Navigate().GoToUrl(baseURL + "/");
            driver.FindElement(By.LinkText("Create New")).Click();
            driver.FindElement(By.XPath("//div[@id='sizzle-1460297264240']/div/table/tbody/tr/td[6]")).Click();
            driver.FindElement(By.XPath("//div[@id='sizzle-1460297264240']/div/table/tbody/tr[2]/td[2]")).Click();
            driver.FindElement(By.CssSelector("input.btn.btn-default")).Click();
            driver.FindElement(By.XPath("//div[@id='sizzle-1460297280451']/div/table/tbody/tr[3]/td[2]")).Click();
            driver.FindElement(By.XPath("//div[@id='sizzle-1460297280451']/div/table/tbody/tr[4]/td[3]")).Click();
            driver.FindElement(By.XPath("//tr[3]/td")).Click();
            driver.FindElement(By.XPath("//tr[3]/td")).Click();
            driver.FindElement(By.XPath("//div[@id='sizzle-1460297280451']/div/table/tbody/tr[3]/td[2]")).Click();
            driver.FindElement(By.CssSelector("input.btn.btn-default")).Click();
            driver.FindElement(By.LinkText("Create New")).Click();
            driver.FindElement(By.XPath("//div[@id='sizzle-1460297297760']/div/table/tbody/tr[5]/td[2]")).Click();
            driver.FindElement(By.XPath("//div[@id='sizzle-1460297297760']/div/table/tbody/tr[5]/td[7]")).Click();
            driver.FindElement(By.CssSelector("input.btn.btn-default")).Click();
            driver.FindElement(By.LinkText("Create New")).Click();
            driver.FindElement(By.CssSelector("th.next")).Click();
            driver.FindElement(By.XPath("//div[@id='sizzle-1460297310904']/div/table/tbody/tr[4]/td[4]")).Click();
            driver.FindElement(By.XPath("//div[@id='sizzle-1460297310904']/div/table/tbody/tr[5]/td[4]")).Click();
            new SelectElement(driver.FindElement(By.Id("CustomerId"))).SelectByText("Jane Doe");
            driver.FindElement(By.CssSelector("input.btn.btn-default")).Click();
        }
        private bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }
        
        private bool IsAlertPresent()
        {
            try
            {
                driver.SwitchTo().Alert();
                return true;
            }
            catch (NoAlertPresentException)
            {
                return false;
            }
        }
        
        private string CloseAlertAndGetItsText() {
            try {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert) {
                    alert.Accept();
                } else {
                    alert.Dismiss();
                }
                return alertText;
            } finally {
                acceptNextAlert = true;
            }
        }
    }
}
