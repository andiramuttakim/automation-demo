namespace MyControls.Header
{
    using Automation;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Remote;
    using System;

    public class HeaderControl : Control<HeaderControlElement>, IFindContext
    {
        public HeaderControl(ControlFactory controlFactory) : base(controlFactory)
        {
        }

        public HeaderControl(ControlFactory controlFactory, ISearchContext context) : base(controlFactory, context)
        {
        }

        public void ClickLink(string linkText)
        {
            if (string.IsNullOrWhiteSpace(linkText)) throw new ArgumentNullException(nameof(linkText));

            Wrapper(() => ThisElement.FindElementByLinkText(linkText).Click());
        }

        protected override RemoteWebElement FindElement()
        {
            var element = Context.FindElement(Elements.Header);

            if (!(element is RemoteWebElement webElement))
                throw new Exception("Retrieved element is not Remote Web Element.");

            return webElement;
        }
    }
}