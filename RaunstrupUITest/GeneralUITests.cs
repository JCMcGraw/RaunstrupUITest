using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using Xunit;

namespace RaunstrupUITest
{
    public class GeneralUITests : IDisposable
    {
        private readonly IWebDriver _driver;
        public GeneralUITests()
        {
            _driver = new ChromeDriver();
        }
        [Fact]
        public void OpenHomepage()
        {
            _driver.Navigate()
        .GoToUrl("https://localhost:44347/");

            Assert.Equal("Hjem - Raunstrup", _driver.Title);
        }

        [Fact]
        public void TestLogin()
        { 
            bool loginSuccess = LoginAsAdmin();

            Assert.True(loginSuccess);
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

            if (result == "Bruger 8!")
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
