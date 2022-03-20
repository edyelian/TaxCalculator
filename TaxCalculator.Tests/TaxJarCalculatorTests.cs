using Moq;
using Moq.Protected;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using TaxCalculator.Services.Calculators.TaxJar;
using TaxCalculator.Services.Calculators.TaxJar.Models.Requests;
using TaxCalculator.Services.Models;
using Xunit;

namespace TaxCalculator.Tests
{
    public class TaxJarCalculatorTests
    {
        [Fact]
        public async Task GetRatesForLocationForZip()
        {
            var handlerMock = new Mock<HttpMessageHandler>();
            handlerMock
                    .Protected()
                    .Setup<Task<HttpResponseMessage>>(
                        "SendAsync",
                        ItExpr.IsAny<HttpRequestMessage>(),
                        ItExpr.IsAny<CancellationToken>()
                    )
                    .ReturnsAsync(new HttpResponseMessage
                    {
                        StatusCode = HttpStatusCode.OK,
                        Content = new StringContent("{\"rate\":{\"city\":\"SAVAGE\",\"city_rate\":\"0.0\",\"combined_district_rate\":\"0.0\",\"combined_rate\":\"0.07375\",\"country\":\"US\",\"country_rate\":\"0.0\",\"county\":\"SCOTT\",\"county_rate\":\"0.005\",\"freight_taxable\":true,\"state\":\"MN\",\"state_rate\":\"0.06875\",\"zip\":\"55378-1384\"}}")
                    })
                    .Verifiable();

            var client = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("https://api.mocktaxjar.com/v2/")
            };

            var taxJarClient = new TaxJarClient(client);
            var req = new TaxJarRateRequest(null, "55378", null, null, null);
            var resp = await taxJarClient.GetRatesForLocationAsync(req);

            Assert.NotNull(resp);
            Assert.Equal("0.07375", resp?.Rate?.CombinedRate);

        }
    }
}