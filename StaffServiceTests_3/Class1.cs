using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace StaffServiceTests_3;

public class StaffServiceTests
{
    private ChromeDriver driver;

    [Test]
    public void AuthorizationTest()
    {
        var options = new ChromeOptions();
        options.AddArguments("--no-sandbox", "--start-maximized", "--disable-extensions");

        driver = new ChromeDriver(options);
        driver.Navigate().GoToUrl("https://staff-testing.testkontur.ru/");
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        var login = driver.FindElement(By.Id("Username"));
        login.SendKeys("user");
        var password = driver.FindElement(By.Id("Password"));
        password.SendKeys("1q2w3e4r%T");
        var enter = driver.FindElement(By.Name("button"));
        enter.Click();
        var titlePageElement = driver.FindElement(By.CssSelector("[data-tid='Title']"));

        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
        wait.Until(ExpectedConditions.UrlToBe("https://staff-testing.testkontur.ru/news"));

        var titleInBrowser = driver.Title;
        Assert.Multiple(() =>
        {
            Assert.AreEqual("Новости", titlePageElement.Text, "Новости не загрузились");
            Assert.AreEqual("Новости", titleInBrowser);
        });
    }


    [Test]
    public void NavigationMenuElementTest()
    {
        var options = new ChromeOptions();
        options.AddArguments("--no-sandbox", "--start-maximized", "--disable-extensions");

        driver = new ChromeDriver(options);
        driver.Navigate().GoToUrl("https://staff-testing.testkontur.ru/");
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        var login = driver.FindElement(By.Id("Username"));
        login.SendKeys("user");
        var password = driver.FindElement(By.Id("Password"));
        password.SendKeys("1q2w3e4r%T");
        var enter = driver.FindElement(By.Name("button"));
        enter.Click();
        var titlePageElement = driver.FindElement(By.CssSelector("[data-tid='Title']"));

        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
        wait.Until(ExpectedConditions.UrlToBe("https://staff-testing.testkontur.ru/news"));

        var SidebarMenuButton = driver.FindElement(By.CssSelector("[data-tid='SidebarMenuButton']"));
        SidebarMenuButton.Click();

        wait.Until(ExpectedConditions.ElementIsVisible(By.CssSelector("[data-tid='SidePage__root']")));
        
        var community = driver.FindElements(By.CssSelector("[data-tid='Community']")).First(element => element.Displayed);
        community.Click();
        
        wait.Until(ExpectedConditions.UrlToBe("https://staff-testing.testkontur.ru/communities"));
        titlePageElement = driver.FindElement(By.CssSelector("[data-tid='Title']"));

        Assert.AreEqual("Сообщества", titlePageElement.Text, "На странице 'сообщества' нет заголовка 'Сообщества'");
    }

    [Test]
    public void SearchTest()
    {
        var options = new ChromeOptions();
        options.AddArguments("--no-sandbox", "--start-maximized", "--disable-extensions");

        driver = new ChromeDriver(options);
        driver.Navigate().GoToUrl("https://staff-testing.testkontur.ru/");
        driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        var login = driver.FindElement(By.Id("Username"));
        login.SendKeys("user");
        var password = driver.FindElement(By.Id("Password"));
        password.SendKeys("1q2w3e4r%T");
        var enter = driver.FindElement(By.Name("button"));
        enter.Click();

        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(3));
        wait.Until(ExpectedConditions.UrlToBe("https://staff-testing.testkontur.ru/news"));
        
        var search = driver.FindElement(By.CssSelector("[data-tid='SearchBar']"));
        search.Click();
        var searchInput = driver.FindElement(By.CssSelector("[placeholder='Поиск сотрудника, подразделения, сообщества, мероприятия']"));
        searchInput.SendKeys("агапова алиса алексеевна ");
        
    }


    [TearDown]
    public void TearDown()
    {
        driver.Quit();
    }
}