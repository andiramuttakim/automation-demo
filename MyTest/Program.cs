namespace MyTest
{
    using Automation;
    using MyControls.Button;
    using MyControls.Header;
    using MyControls.Input;
    using MyControls.Page;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using System.IO;

    public class Program
    {
        public static void Main(string[] args)
        {
            string username = "yourusername";
            string password = "yourpassword";

            using (var webDriver = new ChromeDriver(Directory.GetCurrentDirectory()))
            {
                webDriver.Manage().Window.Maximize();
                var factory = new ControlFactory(webDriver);

                var homePage = factory.GetControl<PageControl>("https://github.com/");
                homePage.Open();
                homePage.WaitUntilLoadComplete("The world’s leading software development platform · GitHub");
                homePage.GetControl<HeaderControl>().ClickLink("Sign in");

                var loginPage = factory.GetControl<PageControl>("https://github.com/login");
                loginPage.WaitUntilLoadComplete("Sign in to GitHub · GitHub");
                loginPage.GetControl<InputControl>("Username or email address").Write(username);
                loginPage.GetControl<InputControl>(By.Id("password")).Write(password);
                loginPage.GetControl<ButtonControl>("Sign in").Click();

                homePage.WaitUntilLoadComplete("GitHub");
            }
        }
    }
}