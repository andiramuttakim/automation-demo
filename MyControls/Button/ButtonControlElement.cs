namespace MyControls.Button
{
    using Automation;
    using OpenQA.Selenium;

    public class ButtonControlElement : ControlElement
    {
        public By GithubButton
        {
            get
            {
                return By.CssSelector(".btn");
            }
        }
    }
}