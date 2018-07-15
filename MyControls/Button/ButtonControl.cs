namespace MyControls.Button
{
    using Automation;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Remote;
    using System;
    using System.Linq;

    public class ButtonControl : Control<ButtonControlElement>, IFindText, IFindBy
    {
        public ButtonControl(ControlFactory controlFactory, By by) : base(controlFactory, by)
        {
        }

        public ButtonControl(ControlFactory controlFactory, string text) : base(controlFactory, text)
        {
        }

        public ButtonControl(ControlFactory controlFactory, ISearchContext context, By by) : base(controlFactory, context, by)
        {
        }

        public ButtonControl(ControlFactory controlFactory, ISearchContext context, string text) : base(controlFactory, context, text)
        {
        }

        public void Click()
        {
            Wrapper(ThisElement.Click);
        }

        protected override RemoteWebElement FindElement()
        {
            if (By != null)
            {
                return FindElement(By);
            }
            else if (!string.IsNullOrEmpty(Text))
            {
                return FindElementWithLabel(Text);
            }
            else
            {
                throw new InvalidOperationException($"Please provide element locator with {nameof(By)} or element label with {nameof(Text)}.");
            }
        }

        private RemoteWebElement FindElement(By by)
        {
            var element = Context.FindElements(by).Where(ControlElement.VisibleElement).SingleOrDefault();
            if (element == null) throw new NotFoundException($"Cannot find element with locator {by}.");

            if (!(element is RemoteWebElement webElement))
                throw new Exception("Retrieved element is not Remote Web Element.");

            return webElement;
        }

        private RemoteWebElement FindElementWithLabel(string text)
        {
            var element = Context.FindElements(Elements.GithubButton).Where(ControlElement.VisibleElement).SingleOrDefault(x => Trim(x.Text).Equals(text) || Trim(x.GetAttribute("value")).Equals(text));
            if (element == null) throw new NotFoundException($"Cannot find element with label {text}.");

            if (!(element is RemoteWebElement webElement))
                throw new Exception("Retrieved element is not Remote Web Element.");

            return webElement;
        }
    }
}