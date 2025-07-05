using FluentAssertions;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using PlaywrightDemo.Pages;
using System.Web;

namespace PlaywrightDemo;

public class UnitTest1 : PageTest
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

    //[Test]
    //[Obsolete]
    //public async Task TestNetwork()
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

    //    //var waitResponse = page.WaitForResponseAsync("**/Employee"); 
    //    //await loginPage.ClickEmployeeList();                 
    //    //var getResponse = await waitResponse;

    //    var response = await page.RunAndWaitForResponseAsync(async () =>
    //    {
    //        await loginPage.ClickEmployeeList();
    //    }, x => x.Url.Contains("/Employee") && x.Status == 200);

    //    var isExist = await loginPage.IsEmployeeDetailsExist();
    //    Assert.That(isExist);
    //}

    //[Test]
    //public async Task Flipkart()
    //{
    //    await using var browser = await Playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
    //    {
    //        Headless = false
    //    });

    //    var context = await browser.NewContextAsync();
    //    var page = await context.NewPageAsync();

    //    await page.GotoAsync("https://www.flipkart.com/", new PageGotoOptions
    //    {
    //        WaitUntil = WaitUntilState.NetworkIdle
    //    });

    //    await page.Locator("text=✕").ClickAsync();
    //    await page.Locator("a", new PageLocatorOptions
    //    {
    //        HasTextString = "Login"
    //    }).ClickAsync();

    //    var request = await page.RunAndWaitForRequestAsync(async () =>
    //    {
    //        await page.Locator("text=✕").ClickAsync();
    //    }, x => x.Url.Contains("flipkart.d1.sc.omtrdc.net") && x.Method == "GET");

    //    var returnData = HttpUtility.UrlDecode(request.Url);

    //    returnData.Should().Contain("Account Login: Displayed Exit");
    //}

    [Test]
    public async Task FlipkartNetworkInterception()
    {
        await using var browser = await Playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = false
        });

        var context = await browser.NewContextAsync();
        var page = await context.NewPageAsync();

        await page.RouteAsync("**/*", async route =>
        {
            if (route.Request.ResourceType == "image")
                await route.AbortAsync();
            else
                await route.ContinueAsync();
        });


        await page.GotoAsync("https://www.flipkart.com/", new PageGotoOptions
        {
            WaitUntil = WaitUntilState.NetworkIdle
        });
    }
}
