namespace Automation
{
    using OpenQA.Selenium;

    public interface IControlFactory
    {
        T GetControl<T>() where T : IFindContext;

        T GetControl<T>(ISearchContext context) where T : IFindContext;

        T GetControl<T>(By by) where T : IFindBy;

        T GetControl<T>(ISearchContext context, By by) where T : IFindBy;

        T GetControl<T>(string text) where T : IFindText;

        T GetControl<T>(ISearchContext context, string text) where T : IFindText;
    }
}