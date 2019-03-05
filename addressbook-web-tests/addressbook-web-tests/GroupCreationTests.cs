using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Chrome;
using WebAddressBookTests;

namespace WebAddressbookTests
{
    [TestFixture]
    public class GroupCreationTests
    {
        private IWebDriver driver;
        private StringBuilder verificationErrors;
        private string baseURL;
        private bool acceptNextAlert = true;

        [SetUp]
        public void SetupTest()
        {
            //FirefoxOptions options = new FirefoxOptions();
            //options.UseLegacyImplementation = true;
            //options.BrowserExecutableLocation = @"C:\Program Files\Mozilla FirefoxESR\firefox.exe";
            //driver = new FirefoxDriver();
            //ChromeOptions options = new ChromeOptions();
            //options.AddWindowType = IWindo
            driver = new ChromeDriver();
            baseURL = "http://localhost/";
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
        public void GroupCreationTest()
        {
            OpenHomePage();
            Login(new AccountData("admin", "secret"));
            GoToGroupPage();
            InitGroupCreation();
            GroupData group = new GroupData("thisName");
            group.Header = "thisHeader";
            group.Footer = "thisFooter";
            FillGroupForm(group);
            SubmitGroupCreation();
            ReturnGroupPage();
            ReturnHomePage();
            LogOut();
        }

        private void LogOut()
        {
            // logout
            driver.FindElement(By.LinkText("Logout")).Click();
        }

        private void ReturnHomePage()
        {
            // returm home page
            driver.FindElement(By.LinkText("home")).Click();
        }

        private void ReturnGroupPage()
        {
            // return group page
            driver.FindElement(By.LinkText("groups")).Click();
        }

        private void SubmitGroupCreation()
        {
            // submit group creation
            driver.FindElement(By.Name("submit")).Click();
        }

        private void FillGroupForm(GroupData groupData)
        {
            // fill group form
            driver.FindElement(By.Name("group_name")).Clear();
            driver.FindElement(By.Name("group_name")).SendKeys(groupData.Name);
            driver.FindElement(By.Name("group_header")).Clear();
            driver.FindElement(By.Name("group_header")).SendKeys(groupData.Header);
            driver.FindElement(By.Name("group_footer")).Clear();
            driver.FindElement(By.Name("group_footer")).SendKeys(groupData.Footer);
        }

        private void InitGroupCreation()
        {
            // init group creation
            driver.FindElement(By.Name("new")).Click();
        }

        private void GoToGroupPage()
        {
            // got to group page
            driver.FindElement(By.LinkText("groups")).Click();
        }

        private void Login(AccountData account)
        {
            driver.FindElement(By.Name("user")).Clear();
            driver.FindElement(By.Name("user")).SendKeys(account.Username);
            driver.FindElement(By.Name("pass")).Clear();
            driver.FindElement(By.Name("pass")).SendKeys(account.Password);
            driver.FindElement(By.CssSelector("input[type=\"submit\"]")).Click();
        }

        private void OpenHomePage()
        {
            driver.Navigate().GoToUrl(baseURL + "addressbook/");
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

        private string CloseAlertAndGetItsText()
        {
            try
            {
                IAlert alert = driver.SwitchTo().Alert();
                string alertText = alert.Text;
                if (acceptNextAlert)
                {
                    alert.Accept();
                }
                else
                {
                    alert.Dismiss();
                }
                return alertText;
            }
            finally
            {
                acceptNextAlert = true;
            }
        }
    }
}

