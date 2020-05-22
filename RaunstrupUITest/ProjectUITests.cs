using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using Xunit;

namespace RaunstrupUITest
{
    public class ProjectUITests : IDisposable
    {
        private readonly IWebDriver _driver;
        public ProjectUITests()
        {
            _driver = new ChromeDriver();
        }
        
        

        [Fact]
        public void CreateProject()
        {
            LoginAsAdmin();

            _driver.Navigate()
        .GoToUrl("https://localhost:44347/Project/Create");

            _driver.FindElement(By.Id("StartDate"))
        .SendKeys("8/5/2020");

            _driver.FindElement(By.Id("EndDate"))
        .SendKeys("10/5/2020");

            _driver.FindElement(By.Id("Price"))
        .SendKeys("5000");

            _driver.FindElement(By.Id("ESTdriving"))
        .SendKeys("500");

            _driver.FindElement(By.Id("Description"))
        .SendKeys("TestProjekt 1");

            _driver.FindElement(By.Id("project_submit"))
                .Click();

            bool isCreated = _driver.PageSource.Contains("TestProjekt 1");

            string headline = _driver.FindElement(By.Id("project_index_headline")).Text;

            Assert.True(isCreated);
            Assert.Equal("Projektoversigt", headline);

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
