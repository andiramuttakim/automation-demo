namespace MyControls.Page
{
    using Automation;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Remote;
    using SeleniumExtras.WaitHelpers;
    using System;

    public class PageControl : Control<PageControlElement>, IFindText
    {
        public string ActualTitle => WebDriver.Title;

        public PageControl(ControlFactory controlFactory, string text) : base(controlFactory, text)
        {
        }

        public PageControl(ControlFactory controlFactory, ISearchContext context, string text) : base(controlFactory, context, text)
        {
        }

        public void Open()
        {
            WebDriver.Navigate().GoToUrl(Text);
        }

        public void Refresh()
        {
            WebDriver.Navigate().Refresh();
        }

        public void WaitUntilLoadComplete(string expectedTitle)
        {
            expectedTitle = expectedTitle ?? string.Empty;
            WaitUntilLoadComplete(expectedTitle, TimeSpan.FromMinutes(DEFAULT_WAIT_IN_MINUTE));
        }

        public void WaitUntilLoadComplete(string expectedTitle, TimeSpan timeOut)
        {
            expectedTitle = expectedTitle ?? string.Empty;
            Wait(ExpectedConditions.TitleIs(expectedTitle), timeOut);
        }

        protected override RemoteWebElement FindElement()
        {
            var element = Context.FindElement(Elements.Body);

            if (!(element is RemoteWebElement webElement))
                throw new Exception("Retrieved element is not Remote Web Element.");

            return webElement;
        }
    }
}