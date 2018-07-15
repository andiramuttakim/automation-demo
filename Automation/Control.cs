namespace Automation
{
    using OpenQA.Selenium;
    using OpenQA.Selenium.Remote;
    using OpenQA.Selenium.Support.UI;
    using System;
    using System.Text.RegularExpressions;

    public abstract class Control : IControlFactory
    {
        #region Constant

        private const int MAX_TRY = 3;
        private const string WHITE_SPACE_REGEX = @"\s+";
        private const string WHITE_SPACE = " ";

        protected const int DEFAULT_WAIT_IN_MINUTE = 1;

        #endregion Constant

        #region Field

        private RemoteWebElement _Element;

        #endregion Field

        #region Property

        protected ControlFactory ControlFactory { get; }

        protected RemoteWebElement ThisElement
        {
            get
            {
                _Element = _Element ?? FindElement();

                for (int i = 1; i <= MAX_TRY; i++)
                {
                    try
                    {
                        var scrollIntoView = _Element.LocationOnScreenOnceScrolledIntoView;
                        break;
                    }
                    catch (StaleElementReferenceException)
                    {
                        _Element = FindElement();
                        if (i == MAX_TRY) throw;
                    }
                }

                return _Element;
            }
        }

        protected IWebDriver WebDriver => ControlFactory.WebDriver;

        public By By { get; }

        public ISearchContext Context { get; }

        public string Text { get; }

        #endregion Property

        #region Constructor

        protected Control(ControlFactory controlFactory) : this(controlFactory, controlFactory.WebDriver)
        {
        }

        protected Control(ControlFactory controlFactory, ISearchContext context)
        {
            ControlFactory = controlFactory ?? throw new ArgumentNullException(nameof(controlFactory));
            Context = context ?? throw new ArgumentNullException(nameof(context));
        }

        protected Control(ControlFactory controlFactory, By by) : this(controlFactory, controlFactory.WebDriver, by)
        {
        }

        protected Control(ControlFactory controlFactory, string text) : this(controlFactory, controlFactory.WebDriver, text)
        {
        }

        protected Control(ControlFactory controlFactory, ISearchContext context, By by)
        {
            ControlFactory = controlFactory ?? throw new ArgumentNullException(nameof(controlFactory));
            Context = context ?? throw new ArgumentNullException(nameof(context));
            By = by ?? throw new ArgumentNullException(nameof(by));
        }

        protected Control(ControlFactory controlFactory, ISearchContext context, string text)
        {
            ControlFactory = controlFactory ?? throw new ArgumentNullException(nameof(controlFactory));
            Context = context ?? throw new ArgumentNullException(nameof(context));
            Text = text ?? throw new ArgumentNullException(nameof(text));
        }

        #endregion Constructor

        #region IControlFactory

        public T GetControl<T>() where T : IFindContext
        {
            return ControlFactory.GetControl<T>(ThisElement);
        }

        public T GetControl<T>(ISearchContext context) where T : IFindContext
        {
            return ControlFactory.GetControl<T>(context);
        }

        public T GetControl<T>(By by) where T : IFindBy
        {
            return ControlFactory.GetControl<T>(ThisElement, by);
        }

        public T GetControl<T>(string text) where T : IFindText
        {
            return ControlFactory.GetControl<T>(ThisElement, text);
        }

        public T GetControl<T>(ISearchContext context, By by) where T : IFindBy
        {
            return ControlFactory.GetControl<T>(context, by);
        }

        public T GetControl<T>(ISearchContext context, string text) where T : IFindText
        {
            return ControlFactory.GetControl<T>(context, text);
        }

        #endregion IControlFactory

        #region Wait

        public TResult Wait<TResult>(Func<IWebDriver, TResult> condition)
        {
            return Wait(condition, TimeSpan.FromMinutes(DEFAULT_WAIT_IN_MINUTE));
        }

        public TResult Wait<TResult>(Func<IWebDriver, TResult> condition, TimeSpan timeout)
        {
            if (condition == null) throw new ArgumentNullException(nameof(condition));
            if (timeout == null) throw new ArgumentNullException(nameof(timeout));

            var webDriverWait = new WebDriverWait(WebDriver, timeout);
            webDriverWait.IgnoreExceptionTypes(typeof(Exception));

            var result = webDriverWait.Until(condition);
            return result;
        }

        #endregion Wait

        #region Method

        protected abstract RemoteWebElement FindElement();

        protected void Wrapper(Action action)
        {
            if (action == null) throw new ArgumentNullException(nameof(action));

            Wrapper(() => { action(); return true; });
        }

        protected TResult Wrapper<TResult>(Func<TResult> func)
        {
            if (func == null) throw new ArgumentNullException(nameof(func));

            TResult result = default(TResult);

            for (int i = 1; i <= MAX_TRY; i++)
            {
                try
                {
                    result = func();
                    break;
                }
                catch (StaleElementReferenceException)
                {
                    if (i == MAX_TRY) throw;
                }
            }

            return result;
        }

        #endregion Method

        #region Static

        protected static string Trim(string text)
        {
            return Regex.Replace(text, WHITE_SPACE_REGEX, WHITE_SPACE).Trim();
        }

        #endregion Static
    }

    public abstract class Control<TElement> : Control
        where TElement : ControlElement
    {
        protected TElement Elements { get; } = Activator.CreateInstance<TElement>();

        protected Control(ControlFactory controlFactory) : base(controlFactory)
        {
        }

        protected Control(ControlFactory controlFactory, ISearchContext context) : base(controlFactory, context)
        {
        }

        protected Control(ControlFactory controlFactory, By by) : base(controlFactory, by)
        {
        }

        protected Control(ControlFactory controlFactory, string text) : base(controlFactory, text)
        {
        }

        protected Control(ControlFactory controlFactory, ISearchContext context, By by) : base(controlFactory, context, by)
        {
        }

        protected Control(ControlFactory controlFactory, ISearchContext context, string text) : base(controlFactory, context, text)
        {
        }
    }
}