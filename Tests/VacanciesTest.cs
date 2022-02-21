using NUnit.Framework;
using OpenQA.Selenium;

namespace VeeamTest.Tests
{
    public class VacanciesTest
    {
        public class Tests
        {
            private IWebDriver driver;
            private readonly By _all_departments = By.XPath("//button[text()='Все отделы']");
            private readonly By _all_languages = By.XPath("//button[text()='Все языки']");
            private readonly By _card = By.XPath("//a[@class='card card-no-hover card-sm']");

            [SetUp]
            public void Setup()
            {
                driver = new OpenQA.Selenium.Chrome.ChromeDriver(@"C:\Users\omen0\Desktop\Домашка\two_app\veeam-test-automation-master\chromedriver_win32");
                driver.Navigate().GoToUrl("https://careers.veeam.ru/vacancies");
                driver.Manage().Window.Maximize();
            }

            [Test]
            [TestCase("Разработка продуктов", "Английский", 5)]
            [TestCase("Продажи", "Английский", 12)]
            [TestCase("Тех. поддержка", "Русский", 2)]
            public void Test1(string department, string language, int expected_result)
            {
                driver.FindElement(_all_departments).Click();

                driver.FindElement(By.XPath("//a[text()='" + department + "']")).Click();                

                driver.FindElement(_all_languages).Click();
                
                switch (language)
                {
                    case "Английский":
                        driver.FindElement(By.XPath("//label[@for='lang-option-0']")).Click();
                        break;
                    case "Русский":
                        driver.FindElement(By.XPath("//label[@for='lang-option-1']")).Click();
                        break;
                    case "Немецкий":
                        driver.FindElement(By.XPath("//label[@for='lang-option-2']")).Click();
                        break;
                    default:
                        Assert.False(false, "Язык не указан в параметризации");
                        break;
                }

                var cards = driver.FindElements(_card);

                Assert.AreEqual(expected_result, cards.Count);
            }
            [TearDown]
            public void TearDown()
            {
                driver.Quit();
                driver.Dispose();
            }
        }
    }
}
