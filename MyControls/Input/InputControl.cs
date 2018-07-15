namespace MyControls.Input
{
    using Automation;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Remote;
    using System;
    using System.Linq;

    public class InputControl : Control<InputControlElement>, IFindBy, IFindText
    {
        public InputControl(ControlFactory controlFactory, By by) : base(controlFactory, by)
        {
        }

        public InputControl(ControlFactory controlFactory, string text) : base(controlFactory, text)
        {
        }

        public InputControl(ControlFactory controlFactory, ISearchContext context, By by) : base(controlFactory, context, by)
        {
        }

        public InputControl(ControlFactory controlFactory, ISearchContext context, string text) : base(controlFactory, context, text)
        {
        }

        public void AssertValueIs(string expected)
        {
            expected = expected ?? string.Empty;

            var actual = GetValue();
            var match = actual.Equals(expected);

            if (!match) throw new Exception($"Expected is '{expected}', but found '{actual}'.");
        }

        public void Clear()
        {
            Wrapper(() => ThisElement.Clear());
        }

        public bool EnsureValueIs(string expected)
        {
            expected = expected ?? string.Empty;

            var actual = GetValue();
            var match = actual.Equals(expected);

            return match;
        }

        public void Focus()
        {
            Wrapper(() => ThisElement.Click());
        }

        public string GetValue()
        {
            var result = Wrapper(() =>
            {
                string value = ThisElement.GetAttribute("value");
                value = value ?? string.Empty;
                value = Trim(value);

                return value;
            });

            return result;
        }

        public void SendKeys(string value)
        {
            value = value ?? string.Empty;

            Wrapper(() => ThisElement.SendKeys(value));
        }

        protected override RemoteWebElement FindElement()
        {
            if (By != null)
            {
                return FindElementWithBy();
            }
            else if (!string.IsNullOrEmpty(Text))
            {
                return FindElementWithLabel();
            }
            else
            {
                throw new InvalidOperationException($"Please provide element locator with {nameof(By)} or element label with {nameof(Text)}.");
            }
        }

        private RemoteWebElement FindElementWithBy()
        {
            var element = Context.FindElements(By).SingleOrDefault();
            if (element == null) throw new NotFoundException($"Cannot find element with locator {By}.");

            if (!(element is RemoteWebElement webElement))
                throw new Exception("Retrieved element is not Remote Web Element.");

            return webElement;
        }

        private RemoteWebElement FindElementWithLabel()
        {
            var label = Context.FindElements(Elements.Label).SingleOrDefault(x => Trim(x.Text).Equals(Text, StringComparison.OrdinalIgnoreCase));
            var forAttr = label.GetAttribute("for");

            var element = Context.FindElements(By.Id(forAttr)).SingleOrDefault();
            if (element == null) throw new NotFoundException($"Cannot find element with locator {By}.");

            if (!(element is RemoteWebElement webElement))
                throw new Exception("Retrieved element is not Remote Web Element.");

            return webElement;
        }
    }
}