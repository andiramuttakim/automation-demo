namespace Automation
{
    using OpenQA.Selenium;

    public interface IFindContext
    {
        ISearchContext Context { get; }
    }
}