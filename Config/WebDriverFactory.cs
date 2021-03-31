using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;

namespace WebStore.BDD.Tests.Config
{
    public static class WebDriverFactory
    {
        public static IWebDriver CreateWebDriver(Browser browser, string driverPath, bool headless)
        {
            IWebDriver webDriver = null;

            switch (browser)
            {
                case Browser.Firefox:
                    var optionsFirefox = new FirefoxOptions();
                    if (headless)
                        optionsFirefox.AddArgument("--headless");
                    webDriver = new FirefoxDriver(driverPath, optionsFirefox);
                    break;

                case Browser.Chrome:
                    var optionsChrome = new ChromeOptions();
                    if (headless)
                        optionsChrome.AddArgument("--headless");
                    webDriver = new ChromeDriver(driverPath, optionsChrome);
                    break;
            }
            return webDriver
        }
    }
}
