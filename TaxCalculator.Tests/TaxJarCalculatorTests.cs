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
        public async Task GetRatesByUSLocation_Zip()
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

            var mockClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("https://api.mocktaxjar.com/v2/")
            };

            var taxJarClient = new TaxJarClient(mockClient);
            var taxJarCalculator = new TaxJarCalculator(taxJarClient);

            var address = new Address(null, "55378", null, null, null);
            var rate = await taxJarCalculator.GetRatesForLocationAsync(address);

            Assert.NotNull(rate);
            Assert.Equal(0.07375F, rate?.CombinedRate);
        }

        [Fact]
        public async Task GetRatesByUSLocation_ZipPlusFour()
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

            var mockClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("https://api.mocktaxjar.com/v2/")
            };

            var taxJarClient = new TaxJarClient(mockClient);
            var taxJarCalculator = new TaxJarCalculator(taxJarClient);

            var address = new Address(null, "55378-1384", null, null, null);
            var rate = await taxJarCalculator.GetRatesForLocationAsync(address);

            Assert.NotNull(rate);
            Assert.Equal(0.07375F, rate?.CombinedRate);
        }

        [Fact]
        public async Task GetRatesByUSLocation_FullAddress()
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
                        Content = new StringContent("{\"rate\":{\"city\":\"WILLISTON\",\"city_rate\":\"0.01\",\"combined_district_rate\":\"0.0\",\"combined_rate\":\"0.07\",\"country\":\"US\",\"country_rate\":\"0.0\",\"county\":\"CHITTENDEN\",\"county_rate\":\"0.0\",\"freight_taxable\":true,\"state\":\"VT\",\"state_rate\":\"0.06\",\"zip\":\"05495-2086\"}}")
                    })
                    .Verifiable();

            var mockClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("https://api.mocktaxjar.com/v2/")
            };

            var taxJarClient = new TaxJarClient(mockClient);
            var taxJarCalculator = new TaxJarCalculator(taxJarClient);

            var address = new Address("US", "05495-2086", "VT", "Williston", "312 Hurricane Lane");
            var rate = await taxJarCalculator.GetRatesForLocationAsync(address);

            Assert.NotNull(rate);
            Assert.Equal(0.07F, rate?.CombinedRate);
        }

        [Fact]
        public async Task GetRatesByLocation_NullZip()
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
                        Content = new StringContent("{\"status\":400,\"error\":\"Bad Request\",\"detail\":\"No zip, required when country is US\"}")
                    })
                    .Verifiable();

            var mockClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("https://api.mocktaxjar.com/v2/")
            };

            var taxJarClient = new TaxJarClient(mockClient);
            var taxJarCalculator = new TaxJarCalculator(taxJarClient);

            var address = new Address(null, null, null, null, null);
            var rate = await taxJarCalculator.GetRatesForLocationAsync(address);

            Assert.Null(rate);
        }

        [Fact]
        public async Task GetRatesByCALocation_ZipWithOptionalParameters()
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
                        Content = new StringContent("{\"rate\":{\"city\":\"Vancouver\",\"combined_rate\":\"0.12\",\"country\":\"CA\",\"freight_taxable\":true,\"state\":\"BC\",\"zip\":\"V5K0A1\"}}")
                    })
                    .Verifiable();

            var mockClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("https://api.mocktaxjar.com/v2/")
            };

            var taxJarClient = new TaxJarClient(mockClient);
            var taxJarCalculator = new TaxJarCalculator(taxJarClient);

            var address = new Address("CA", "V5K0A1", "BC", "Vancouver", null);
            var rate = await taxJarCalculator.GetRatesForLocationAsync(address);

            Assert.NotNull(rate);
            Assert.Equal(0.12F, rate?.CombinedRate);
        }

        [Fact]
        public async Task GetRatesByAULocation_ZipWithOptionalParameters()
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
                        Content = new StringContent("{\"rate\":{\"combined_rate\":\"0.1\",\"country\":\"AU\",\"country_rate\":\"0.1\",\"freight_taxable\":true,\"zip\":\"2060\"}}")
                    })
                    .Verifiable();

            var mockClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("https://api.mocktaxjar.com/v2/")
            };

            var taxJarClient = new TaxJarClient(mockClient);
            var taxJarCalculator = new TaxJarCalculator(taxJarClient);

            var address = new Address("AU", "2060 ", null, "Sydney", null);
            var rate = await taxJarCalculator.GetRatesForLocationAsync(address);

            Assert.NotNull(rate);
            Assert.Equal(0.1F, rate?.CombinedRate);
        }

        [Fact]
        public async Task GetRatesByEULocation_ZipWithOptionalParameters()
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
                        Content = new StringContent("{\"rate\":{\"country\":\"FI\",\"distance_sale_threshold\":\"0.0\",\"freight_taxable\":true,\"name\":\"Finland\",\"parking_rate\":\"0.0\",\"reduced_rate\":\"0.14\",\"standard_rate\":\"0.24\",\"super_reduced_rate\":\"0.1\"}}")
                    })
                    .Verifiable();

            var mockClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("https://api.mocktaxjar.com/v2/")
            };

            var taxJarClient = new TaxJarClient(mockClient);
            var taxJarCalculator = new TaxJarCalculator(taxJarClient);

            var address = new Address("FI", "00150  ", null, "Helsinki", null);
            var rate = await taxJarCalculator.GetRatesForLocationAsync(address);

            Assert.NotNull(rate);
            Assert.Equal(0.24F, rate?.StandardRate);
        }
    }
}