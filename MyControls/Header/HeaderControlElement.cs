namespace MyControls.Header
{
    using Automation;
    using OpenQA.Selenium;

    public class HeaderControlElement : ControlElement
    {
        public By Header
        {
            get
            {
                return By.CssSelector("header.Header");
            }
        }
    }
}