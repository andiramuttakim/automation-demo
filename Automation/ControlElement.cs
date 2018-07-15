namespace Automation
{
    using OpenQA.Selenium;
    using System;

    public abstract class ControlElement
    {
        public virtual By AllElements
        {
            get
            {
                return By.CssSelector("*");
            }
        }

        public By Body
        {
            get
            {
                return By.TagName("body");
            }
        }

        public By Input
        {
            get
            {
                return By.TagName("input");
            }
        }

        public By A
        {
            get
            {
                return By.TagName("a");
            }
        }

        public By Span
        {
            get
            {
                return By.TagName("span");
            }
        }

        public By H1
        {
            get
            {
                return By.TagName("h1");
            }
        }

        public By H2
        {
            get
            {
                return By.TagName("h2");
            }
        }

        public By H3
        {
            get
            {
                return By.TagName("h3");
            }
        }

        public By Strong
        {
            get
            {
                return By.TagName("strong");
            }
        }

        public By Label
        {
            get
            {
                return By.TagName("label");
            }
        }

        public By Div
        {
            get
            {
                return By.TagName("div");
            }
        }

        public By Button
        {
            get
            {
                return By.TagName("Button");
            }
        }

        public static bool VisibleElement(IWebElement element)
        {
            if (element == null) throw new ArgumentNullException("element");

            try
            {
                return element.Displayed && element.Size.Height > 0 && element.Size.Width > 0 && (element.Location.X > -1 || element.Location.Y > -1);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
