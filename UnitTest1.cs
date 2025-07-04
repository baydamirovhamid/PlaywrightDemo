using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using PlaywrightDemo.Pages;

namespace PlaywrightDemo;

public class UnitTest1: PageTest
{
    [SetUp]
    public async Task Setup()
    {
        await Page.GotoAsync("http://eaapp.somee.com/");

    }

    //[Test]
    //[Obsolete]
    //public async Task TestWithPOM()
    //{
    //    await using var browser = await Playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
    //    {
    //        Headless = false
    //    });

    //    var page = await browser.NewPageAsync();
    //    await page.GotoAsync("http://eaapp.somee.com/");


    //    var loginPage = new LoginPageUpgraded(page);

    //    await loginPage.ClickLogin();
    //    await loginPage.Login("admin", "password");

    //    var isExist = await loginPage.IsEmployeeDetailsExist();
    //    Assert.That(isExist);

    //}

    [Test]
    [Obsolete]
    public async Task TestNetwork()
    {
        await using var browser = await Playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = false
        });

        var page = await browser.NewPageAsync();
        await page.GotoAsync("http://eaapp.somee.com/");

        var loginPage = new LoginPageUpgraded(page);

        await loginPage.ClickLogin();                       
        await loginPage.Login("admin", "password");          

        //var waitResponse = page.WaitForResponseAsync("**/Employee"); 
        //await loginPage.ClickEmployeeList();                 
        //var getResponse = await waitResponse;

        var response = await page.RunAndWaitForResponseAsync(async () =>
        {
            await loginPage.ClickEmployeeList();
        }, x => x.Url.Contains("/Employee") && x.Status == 200);

        var isExist = await loginPage.IsEmployeeDetailsExist();
        Assert.That(isExist);
    }

}
