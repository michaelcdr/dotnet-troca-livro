using OpenQA.Selenium.Chrome;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using Xunit;

namespace TrocaLivro.Testes.Automacao
{
    
    public class Class1
    {
        
        public Class1()
        {
        
        }

        [Fact]
        public void Teste()
        {
            new DriverManager().SetUpDriver(new ChromeConfig());

            var driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://www.selenium.dev/selenium/web/web-form.html");

            var title = driver.Title;
        }

    }
}