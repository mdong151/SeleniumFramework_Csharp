﻿using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;
using System;
using System.Threading;
using TravelAgencyApp.Configurations;


namespace TravelAgencyApp.Ultilities
{
    public class Browser
    {

        private static IWebDriver _webDriver;
        private static BrowserTypes _browser = (BrowserTypes)AppConfigReader.GetBrowser();
        private static TestEnvironmentTypes _testEnvironment = (TestEnvironmentTypes)AppConfigReader.GetTestEnvironment();
        private static int TIME_OUT = AppConfigReader.GetTimeout();
                
        public static IWebDriver Driver { get { return _webDriver; } }
        public static string Title { get { return _webDriver.Title; } }
        private static string BaseUrl
        {
            get
            {
                if (_testEnvironment == TestEnvironmentTypes.INTE)
                {
                    return "inte.amaris.com/TravelAgency";
                }
                else if (_testEnvironment == TestEnvironmentTypes.QA)
                {
                    return "qaarp.amaris.com/TravelAgency";
                }
                else if (_testEnvironment == TestEnvironmentTypes.PRD)
                {
                    return "arp.amaris.com/TravelAgency";
                }
                return null;
            }
        }

        public static void Initialize()
        {
            if(_browser == BrowserTypes.InternetExplorer)
            {
                _webDriver = new InternetExplorerDriver(@"C:\Users\MNG06\Documents\Visual Studio Code\Amaris\SeleniumFramework\Drivers");
            }
            else if (_browser == BrowserTypes.Chrome)
            {
                _webDriver = new ChromeDriver(@"C:\Users\MNG06\Documents\Visual Studio Code\Amaris\SeleniumFramework\Drivers");
            }
            else if (_browser == BrowserTypes.Firefox)
            {
                _webDriver = new FirefoxDriver();
            }
        }

        public static void GoToPage(string url,bool useBaseUrl = true)
        {
            if (useBaseUrl == true)
            {
                _webDriver.Navigate().GoToUrl("https://"+ BaseUrl + url);
            }
            else
            {
                _webDriver.Navigate().GoToUrl(url);
            }
            
        }
        public static void GoToPageWithCredentials(string url, bool useBase = true)
        {
            string username = AppConfigReader.GetUsername();
            string password = AppConfigReader.GetPassword();
            if(useBase == true)
            {
                _webDriver.Navigate().GoToUrl("https://" + username + ":" + password + "@" + BaseUrl + url);
            }
            else
            {
                _webDriver.Navigate().GoToUrl("https://" + username + ":" + password + "@" + url.Substring(1,url.Length-1));
            }
        }

        public static void Close()
        {
            _webDriver.Close();
        }

        public static By GetElementBy(string how,string locator)
        {
            By by = null;
            if (how.ToLower().Contains("xpath"))
            {
                by = By.XPath(locator);
            }
            else if (how.ToLower().Contains("id"))
            {
                by = By.Id(locator);
            }
            else if (how.ToLower().Contains("name"))
            {
                by = By.Name(locator);
            }
            else if (how.ToLower().Contains("css"))
            {
                by = By.CssSelector(locator);
            }
            else if (how.ToLower().Contains("linktext"))
            {
                by = By.LinkText(locator);
            }
            else if (how.ToLower().Contains("class"))
            {
                by = By.ClassName(locator);
            }
            return by;
        }

        public static IWebElement GetElement(string how,string locator)
        {
            IWebElement ele = null;
            ele = _webDriver.FindElement(GetElementBy(how, locator));
            return ele;
        }
        public static IWebElement GetElement(By byElement)
        {
            return _webDriver.FindElement(byElement);
        }

        public static string GetText(string how, string locator,int timeoutInSeconds)
        {
            WaitUntilElementIsDisplayed(how, locator, timeoutInSeconds);
            string text;
            text = GetElement(how, locator).Text;
            return text;
        }

        public static string GetText(string how, string locator)
        {
            return GetText(how, locator, TIME_OUT);
        }

        public static string GetText(By element,int timeoutInSeconds)
        {
            WaitUntilElementIsDisplayed(element,timeoutInSeconds);
            string text;
            text = GetElement(element).Text;
            return text;
        }

        public static string GetText(By byElement)
        {
            return GetText(byElement,TIME_OUT);
        }

        public static void ClearAndEnterText(string how, string locator,string textToType,int timeoutInSeconds)
        {
            WaitUntilElementIsDisplayed(how, locator, timeoutInSeconds);
            GetElement(how, locator).Clear();
            GetElement(how, locator).SendKeys(textToType);
        }

        public static void ClearAndEnterText(string how, string locator, string textToType)
        {
            ClearAndEnterText(how, locator, textToType, TIME_OUT);
        }

        public static void ClearAndEnterText(By byElement, string textToType, int timeoutInSeconds)
        {
            WaitUntilElementIsDisplayed(byElement, timeoutInSeconds);
            GetElement(byElement).Clear();
            GetElement(byElement).SendKeys(textToType);
        }

        public static void ClearAndEnterText(By byElement, string textToType)
        {
            ClearAndEnterText(byElement, textToType, TIME_OUT);
        }

        public static void EnterText(string how, string locator, string textToType, int timeoutInSeconds )
        {
            WaitUntilElementIsDisplayed(how, locator, timeoutInSeconds);
            GetElement(how, locator).SendKeys(textToType);
        }

        public static void EnterText(string how, string locator, string textToType)
        {
            EnterText(how, locator, textToType, TIME_OUT);
        }

        public static void EnterText(By byElement, string textToType, int timeoutInSeconds)
        {
            WaitUntilElementIsDisplayed(byElement,timeoutInSeconds);
            GetElement(byElement).SendKeys(textToType);
        }

        public static void EnterText(By byElement, string textToType)
        {
            EnterText(byElement, textToType, TIME_OUT);
        }

        public static void PressEnter(string how,string locator)
        {
            GetElement(how, locator).SendKeys(Keys.Enter);
        }

        public static void PressEnter(By byElement)
        {
            GetElement(byElement).SendKeys(Keys.Enter);
        }

        public static void Select(string how, string locator,int timeoutInSeconds)
        {
            WaitUntilElementIsDisplayed(how, locator, timeoutInSeconds);
            GetElement(how, locator).Click();
        }

        public static void Select(string how, string locator)
        {
            Select(how, locator, TIME_OUT);
        }

        public static void Select(By byElement,int timeoutInSeconds)
        {
            WaitUntilElementIsDisplayed(byElement,timeoutInSeconds);
            GetElement(byElement).Click();
        }

        public static void Select(By byElement)
        {
            Select(byElement, TIME_OUT);
        }

        public static bool WaitUntilElementIsDisplayed(string how,string locator,int timeoutInSeconds)
        {
            for(int i = 0; i < timeoutInSeconds; i++)
            {
                if (ElementIsDisplayed(how, locator))
                {
                    return true;
                }
                Thread.Sleep(1000);
            }
            return false;
        }

        public static bool WaitUntilElementIsDisplayed(string how, string locator)
        {
            return WaitUntilElementIsDisplayed(how, locator, TIME_OUT);
        }

        public static bool WaitUntilElementIsDisplayed(By byElement, int timeoutInSeconds)
        {
            WebDriverWait wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(timeoutInSeconds));
            wait.Until(ExpectedConditions.ElementIsVisible(byElement));
            return false;
        }

        public static bool WaitUntilElementIsDisplayed(By byElement)
        {
            return WaitUntilElementIsDisplayed(byElement, TIME_OUT);
        }

        public static bool ElementIsDisplayed(string how,string locator)
        {
            var isDisplayed = false;
            try
            {
                isDisplayed = GetElement(how, locator).Displayed;
            }
            catch (NoSuchElementException) { }
            return isDisplayed;
        }

        public static bool IsAt(string pageTitle)
        {
            return _webDriver.Title.Contains(pageTitle);
        }

        public static void SwitchToTab(int tabIndex)
        {
            var windows = _webDriver.WindowHandles;
            _webDriver.SwitchTo().Window(windows[tabIndex]);
        }

        public static void Maximize()
        {
            _webDriver.Manage().Window.Maximize();
        }

        public static void WaitFor(int seconds)
        {
            var miliseconds = seconds * 1000;
            Thread.Sleep(miliseconds);
        }

        //public static void Authenticate(string username,string password)
        //{
        //    WaitFor(5);
        //    _webDriver.SwitchTo().Alert().SetAuthenticationCredentials(username,password);
        //}

        public static bool WaitUntilElementIsInvisibled(string how, string locator, int timeoutInSeconds)
        {
            WaitUntilElementIsDisplayed(how, locator, timeoutInSeconds);
            var wait = new WebDriverWait(_webDriver,TimeSpan.FromSeconds(timeoutInSeconds));
            return wait.Until(ExpectedConditions.InvisibilityOfElementLocated(GetElementBy(how,locator)));
        }

        public static bool WaitUntilElementIsInvisibled(string how, string locator)
        {
            return WaitUntilElementIsInvisibled(how, locator, TIME_OUT);
        }

        public static bool WaitUntilElementIsInvisibled(By byElement, int timeoutInSeconds)
        {
            WaitUntilElementIsDisplayed(byElement, timeoutInSeconds);
            var wait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(timeoutInSeconds));
            return wait.Until(ExpectedConditions.InvisibilityOfElementLocated(byElement));
        }

        public static bool WaitUntilElementIsInvisibled(By byElement)
        {
            return WaitUntilElementIsInvisibled(byElement, TIME_OUT);
        }

        //to handle Travel Agency search field 
        public static void SearchAndSelect(string how, string locator, string textToSearch,int timeoutInSeconds)
        {
            WaitUntilElementIsDisplayed(how, locator, timeoutInSeconds);
            EnterText(how, locator, textToSearch);
            WaitUntilElementIsInvisibled("xpath", "//li[contains(text(),'Searching…')]", timeoutInSeconds);
            PressEnter(how, locator);
        }

        public static void SearchAndSelect(string how, string locator, string textToSearch)
        {
            SearchAndSelect(how, locator, textToSearch, TIME_OUT);
        }

        public static void SearchAndSelect (By byElement, string textToSearch,int timeoutInSeconds)
        {
            WaitUntilElementIsDisplayed(byElement,timeoutInSeconds);
            EnterText(byElement,textToSearch);
            WaitUntilElementIsInvisibled("xpath", "//li[contains(text(),'Searching…')]", timeoutInSeconds);
            PressEnter(byElement);
        }

        public static void SearchAndSelect(By byElement, string textToSearch)
        {
            SearchAndSelect(byElement, textToSearch, TIME_OUT);
        }

        public static void SelectDropdown(string how, string locator, string value)
        {
            new SelectElement(GetElement(how, locator)).SelectByText(value);
        }
    }
}