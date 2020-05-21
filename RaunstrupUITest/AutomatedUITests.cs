using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using Xunit;

namespace RaunstrupUITest
{
    public class AutomatedUITests : IDisposable
    {
        private readonly IWebDriver _driver;
        public AutomatedUITests()
        {
            _driver = new ChromeDriver();
        }
        [Fact]
        public void OpenHomepage()
        {
            _driver.Navigate()
        .GoToUrl("https://localhost:44347/");

            Assert.Equal("Home Page - Raunstrup", _driver.Title);
        }

        [Fact]
        public void TestLogin()
        { 
            bool loginSuccess = LoginAsAdmin();

            Assert.True(loginSuccess);
        }

        [Fact]
        public void CreateCustomer()
        {
            LoginAsAdmin();

                _driver.Navigate()
            .GoToUrl("https://localhost:44347/Customer/Create");

                _driver.FindElement(By.Id("input_name"))
            .SendKeys("TestName1");

                _driver.FindElement(By.Id("input_address"))
            .SendKeys("TestVej 1");

                _driver.FindElement(By.Id("input_phone"))
            .SendKeys("12345699");

                _driver.FindElement(By.Id("input_email"))
            .SendKeys("email@email.com");

            _driver.FindElement(By.Id("customer_submit"))
                .Click();

            bool isCreated = _driver.PageSource.Contains("TestName1");

            string headline = _driver.FindElement(By.Id("customer_index_headline")).Text;

            Assert.True(isCreated);
            Assert.Equal("Kundeoversigt", headline);

        }

        private bool LoginAsAdmin()
        {
            _driver.Navigate()
        .GoToUrl("https://localhost:44347/Identity/Account/Login");

            _driver.FindElement(By.Id("Input_UserName"))
        .SendKeys("8");

            _driver.FindElement(By.Id("Input_Password"))
                .SendKeys("bruger8");

            _driver.FindElement(By.Id("loginbutton"))
                .Click();

            var result = _driver.FindElement(By.Id("loginhello")).Text;

            if (result == "Hello 8!")
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public void Dispose()
        {
            _driver.Quit();
            _driver.Dispose();
        }
    }
}
