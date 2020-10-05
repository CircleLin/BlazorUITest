using NUnit.Framework;
using Bunit;
using BlazorUITest.Pages;
using NSubstitute;
using static BlazorUITest.Pages.FetchData;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using System;
using BlazorUITest.Service;

namespace NUnitTestProject1
{
    public class Tests
    {
        [Test]
        public void IndexShouldRender()
        {
            //
            var ctx = new Bunit.TestContext();

            //cut = component under test
            var cut = ctx.RenderComponent<BlazorUITest.Pages.Index>();
            cut.MarkupMatches("<h1>Hello, world!</h1>");
        }

        [Test]
        public void CounterShouldIncrementWhenSelected()
        {
            var ctx = new Bunit.TestContext();
            //Arrange
            var cut = ctx.RenderComponent<Counter>();
            var element = cut.Find("p");

            //Act
            cut.Find("button").Click();
            string elementText = element.TextContent;

            //Assert
            elementText.MarkupMatches("Current count: 1");
        }

        [Test]
        public void FetchDataShouldRenderLoadingWhenDataIsNull()
        {
            var ctx = new Bunit.TestContext();

            var mockService = Substitute.For<IWeatherService>();

            mockService.GetWeatherDataAsync().Returns(Task.FromResult<FetchData.WeatherForecast[]>(null));

            ctx.Services.AddSingleton<IWeatherService>(mockService);

            var cut = ctx.RenderComponent<FetchData>();

            var expectedHtml = @"<h1>Weather forecast</h1>
                                <p>This component demonstrates fetching data from the server.</p>
                                <p><em>Loading...</em></p>";

            cut.MarkupMatches(expectedHtml);
        }

        [Test]
        public void FetchDataShouldRenderLoadingWhenDataIsNotNull()
        {
            var ctx = new Bunit.TestContext();
            var mockService = Substitute.For<IWeatherService>();

            mockService.GetWeatherDataAsync().Returns(Task.FromResult(new WeatherForecast[] { new WeatherForecast() { TemperatureC = 30, Summary = "test", Date = new DateTime(2020, 10, 6) } }));

            ctx.Services.AddSingleton<IWeatherService>(mockService);

            var cut = ctx.RenderComponent<FetchData>();

            var expectedHtml = @"<h1>Weather forecast</h1>
                                <p>This component demonstrates fetching data from the server.</p>
                                <table class='table'>
                                 <thead>
                                   <tr>
                                     <th>Date</th>
                                     <th>Temp. (C)</th>
                                     <th>Temp. (F)</th>
                                     <th>Summary</th>
                                   </tr>
                                 </thead>
                                 <tbody>
                                   <tr>
                                     <td>2020/10/6</td>
                                     <td>30</td>
                                     <td>85</td>
                                     <td>test</td>
                                   </tr>
                                 </tbody>
                                </table>";

            cut.MarkupMatches(expectedHtml);
        }
    }
}