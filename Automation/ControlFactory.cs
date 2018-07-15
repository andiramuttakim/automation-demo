namespace Automation
{
    using OpenQA.Selenium;
    using System;
    using System.Collections.Generic;

    public class ControlFactory : IControlFactory
    {
        protected IDictionary<Type, Type> LookUpTable { get; }

        public IWebDriver WebDriver { get; }

        public ControlFactory(IWebDriver webDriver)
        {
            WebDriver = webDriver ?? throw new ArgumentNullException(nameof(webDriver));
            LookUpTable = new Dictionary<Type, Type>();
        }

        public T GetControl<T>() where T : IFindContext
        {
            return (T)Activator.CreateInstance(typeof(T), this);
        }

        public T GetControl<T>(ISearchContext context) where T : IFindContext
        {
            return (T)Activator.CreateInstance(typeof(T), this, context);
        }

        public T GetControl<T>(By by) where T : IFindBy
        {
            return (T)Activator.CreateInstance(typeof(T), this, by);
        }

        public T GetControl<T>(string text) where T : IFindText
        {
            return (T)Activator.CreateInstance(typeof(T), this, text);
        }

        public T GetControl<T>(ISearchContext context, By by) where T : IFindBy
        {
            return (T)Activator.CreateInstance(typeof(T), this, context, by);
        }

        public T GetControl<T>(ISearchContext context, string text) where T : IFindText
        {
            return (T)Activator.CreateInstance(typeof(T), this, context, text);
        }
    }
}