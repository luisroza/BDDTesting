using OpenQA.Selenium;
using OpenQA.Selenium.Support.Extensions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using ExpectedConditions = SeleniumExtras.WaitHelpers.ExpectedConditions;

namespace WebStore.BDD.Tests.Config
{
    public class SeleniumHelper : IDisposable
    {
        public IWebDriver WebDriver;
        public readonly ConfigurationHelper Configuration;
        public WebDriverWait Wait;

        public SeleniumHelper(Browser browser, ConfigurationHelper configuration, bool headless = true)
        {
            Configuration = configuration;
            WebDriver = WebDriverFactory.CreateWebDriver(browser, Configuration.WebDrivers, headless);
            WebDriver.Manage().Window.Maximize();
            Wait = new WebDriverWait(WebDriver, TimeSpan.FromSeconds(30));
        }

        public string GetUrl()
        {
            return WebDriver.Url;
        }

        public void GoToUrl(string url)
        {
            WebDriver.Navigate().GoToUrl(url);
        }

        public bool ValidateUrlContent(string content)
        {
            return Wait.Until(ExpectedConditions.UrlContains(content));
        }

        public void ClickLinkText(string linkText)
        {
            //find and click it
            //WebDriver.FindElement(By.LinkText(linkText)).Click();

            var link = Wait.Until(ExpectedConditions.ElementIsVisible(By.LinkText(linkText)));
            link.Click();
        }

        public void ClickButtonById(string buttonId)
        {
            var button = Wait.Until(ExpectedConditions.ElementIsVisible(By.Id(buttonId)));
            button.Click();
        }

        public void ClickByXPath(string xPath)
        {
            var button = Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(xPath)));
            button.Click();
        }

        public IWebElement GetElementByClass(string classCss)
        {
            return Wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName(classCss)));
        }

        public IWebElement GetElementByXPath(string xPath)
        {
            return Wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(xPath)));
        }

        public void FillTextBoxById(string fieldId, string fieldValue)
        {
            var field = Wait.Until(ExpectedConditions.ElementIsVisible(By.Id(fieldId)));
            field.SendKeys(fieldValue);
        }

        public void FillDropDownById(string fieldId, string fieldValue)
        {
            var field = Wait.Until(ExpectedConditions.ElementIsVisible(By.Id(fieldId)));
            var selectElement = new SelectElement(field);
            selectElement.SelectByValue(fieldValue);
        }

        public string GetTextElementByClassCss(string className)
        {
            return Wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName(className))).Text;
        }

        public string GetTextElementById(string id)
        {
            return Wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName(id))).Text;
        }

        public string GetTextBoxValueById(string id)
        {
            return Wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName(id))).GetAttribute("value");
        }

        public IEnumerable<IWebElement> GetListByClass(string className)
        {
            return Wait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(By.ClassName(className)));
        }

        public bool ValidateElementExistsById(string id)
        {
            return ExistingElement(By.Id(id));
        }

        public void BackNavigation(int times = 1)
        {
            for (int i = 0; i < times; i++)
            {
                WebDriver.Navigate().Back();
            }
        }

        public void GetScreenShot(string name)
        {
            SaveScreenShot(WebDriver.TakeScreenshot(), string.Format("{0}_" + name + ".png", DateTime.Now.ToFileTime()));
        }

        private void SaveScreenShot(Screenshot screenshot, string fileName)
        {
            screenshot.SaveAsFile($"{Configuration.FolderPicture}{fileName}", ScreenshotImageFormat.Png);
        }

        private bool ExistingElement(By by)
        {
            try
            {
                WebDriver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        public void Dispose()
        {
            WebDriver?.Quit();
            WebDriver?.Dispose();
        }
    }
}
