using Moq;
using Moq.Protected;
using System;
using System.Collections.Generic;
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
            Assert.Equal(0.06875F, rate?.StateRate);
            Assert.Equal(0.005F, rate?.CountyRate);
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
            Assert.Equal(0.06875F, rate?.StateRate);
            Assert.Equal(0.005F, rate?.CountyRate);
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
                        StatusCode = HttpStatusCode.BadRequest,
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

        [Fact]
        public async Task GetSalesTaxForOrder_US()
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
                        Content = new StringContent("{\"tax\":{\"amount_to_collect\":1.43,\"breakdown\":{\"city_tax_collectable\":0.0,\"city_tax_rate\":0.0,\"city_taxable_amount\":0.0,\"combined_tax_rate\":0.095,\"county_tax_collectable\":0.15,\"county_tax_rate\":0.01,\"county_taxable_amount\":15.0,\"line_items\":[{\"city_amount\":0.0,\"city_tax_rate\":0.0,\"city_taxable_amount\":0.0,\"combined_tax_rate\":0.095,\"county_amount\":0.15,\"county_tax_rate\":0.01,\"county_taxable_amount\":15.0,\"id\":\"1\",\"special_district_amount\":0.34,\"special_district_taxable_amount\":15.0,\"special_tax_rate\":0.0225,\"state_amount\":0.94,\"state_sales_tax_rate\":0.0625,\"state_taxable_amount\":15.0,\"tax_collectable\":1.43,\"taxable_amount\":15.0}],\"special_district_tax_collectable\":0.34,\"special_district_taxable_amount\":15.0,\"special_tax_rate\":0.0225,\"state_tax_collectable\":0.94,\"state_tax_rate\":0.0625,\"state_taxable_amount\":15.0,\"tax_collectable\":1.43,\"taxable_amount\":15.0},\"freight_taxable\":false,\"has_nexus\":true,\"jurisdictions\":{\"city\":\"LOS ANGELES\",\"country\":\"US\",\"county\":\"LOS ANGELES COUNTY\",\"state\":\"CA\"},\"order_total_amount\":16.5,\"rate\":0.095,\"shipping\":1.5,\"tax_source\":\"destination\",\"taxable_amount\":15.0}}")
                    })
                    .Verifiable();

            var mockClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("https://api.mocktaxjar.com/v2/")
            };

            var taxJarClient = new TaxJarClient(mockClient);
            var taxJarCalculator = new TaxJarCalculator(taxJarClient);

            var fromAddress = new Address(country: "US", zip: "92093", state: "CA", city: "La Jolla", street: "9500 Gilman Drive");
            var toAddress = new Address(country: "US", zip: "90002", state: "CA", city: "Los Angeles", street: "1335 E 103rd St");
            var nexusAdddress = new NexusAddress(id: "Main Location", country: "US", zip: "92093", state: "CA", city: "La Jolla", street: "9500 Gilman Drive");
            var item = new Item(1, 1, "20010", 15, 0);

            var salesOrder = new SalesOrder(fromAddress: fromAddress, toAddress: toAddress, 1.5F, items: new List<Item> { item }, nexusAddresses: new List<NexusAddress> { nexusAdddress });
            var salesTax = await taxJarCalculator.GetSalesTaxForOrderAsync(salesOrder);

            Assert.NotNull(salesTax);
            Assert.Equal(1.43F, salesTax);
        }

        [Fact]
        public async Task GetSalesTaxForOrder_OriginBasedStates()
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
                        Content = new StringContent("{\"tax\":{\"amount_to_collect\":1.36,\"breakdown\":{\"city_tax_collectable\":0.17,\"city_tax_rate\":0.01,\"city_taxable_amount\":16.5,\"combined_tax_rate\":0.0825,\"county_tax_collectable\":0.0,\"county_tax_rate\":0.0,\"county_taxable_amount\":0.0,\"line_items\":[{\"city_amount\":0.15,\"city_tax_rate\":0.01,\"city_taxable_amount\":15.0,\"combined_tax_rate\":0.0825,\"county_amount\":0.0,\"county_tax_rate\":0.0,\"county_taxable_amount\":0.0,\"id\":\"1\",\"special_district_amount\":0.15,\"special_district_taxable_amount\":15.0,\"special_tax_rate\":0.01,\"state_amount\":0.94,\"state_sales_tax_rate\":0.0625,\"state_taxable_amount\":15.0,\"tax_collectable\":1.24,\"taxable_amount\":15.0}],\"shipping\":{\"city_amount\":0.02,\"city_tax_rate\":0.01,\"city_taxable_amount\":1.5,\"combined_tax_rate\":0.0825,\"county_amount\":0.0,\"county_tax_rate\":0.0,\"county_taxable_amount\":0.0,\"special_district_amount\":0.02,\"special_tax_rate\":0.01,\"special_taxable_amount\":1.5,\"state_amount\":0.09,\"state_sales_tax_rate\":0.0625,\"state_taxable_amount\":1.5,\"tax_collectable\":0.12,\"taxable_amount\":1.5},\"special_district_tax_collectable\":0.17,\"special_district_taxable_amount\":16.5,\"special_tax_rate\":0.01,\"state_tax_collectable\":1.03,\"state_tax_rate\":0.0625,\"state_taxable_amount\":16.5,\"tax_collectable\":1.36,\"taxable_amount\":16.5},\"freight_taxable\":true,\"has_nexus\":true,\"jurisdictions\":{\"city\":\"AUSTIN\",\"country\":\"US\",\"county\":\"TRAVIS\",\"state\":\"TX\"},\"order_total_amount\":16.5,\"rate\":0.0825,\"shipping\":1.5,\"tax_source\":\"origin\",\"taxable_amount\":16.5}}")
                    })
                    .Verifiable();

            var mockClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("https://api.mocktaxjar.com/v2/")
            };

            var taxJarClient = new TaxJarClient(mockClient);
            var taxJarCalculator = new TaxJarCalculator(taxJarClient);

            var fromAddress = new Address(country: "US", zip: "78701", state: "TX", city: "Austin", street: "1100 Congress Ave");
            var toAddress = new Address(country: "US", zip: "77058", state: "TX", city: "Houston", street: "1601 E NASA Pkwy");
            var nexusAdddress = new NexusAddress(id: "Main Location", country: "US", zip: "78701", state: "TX", city: "Austin", street: "1100 Congress Ave");
            var item = new Item(1, 1, null, 15, 0);

            var salesOrder = new SalesOrder(fromAddress: fromAddress, toAddress: toAddress, 1.5F, items: new List<Item> { item }, nexusAddresses: new List<NexusAddress> { nexusAdddress });
            var salesTax = await taxJarCalculator.GetSalesTaxForOrderAsync(salesOrder);

            Assert.NotNull(salesTax);
            Assert.Equal(1.36F, salesTax);
        }

        [Fact]
        public async Task GetSalesTaxForOrder_ShippingExemptions()
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
                        Content = new StringContent("{\"tax\":{\"amount_to_collect\":0.94,\"breakdown\":{\"city_tax_collectable\":0.0,\"city_tax_rate\":0.0,\"city_taxable_amount\":0.0,\"combined_tax_rate\":0.0625,\"county_tax_collectable\":0.0,\"county_tax_rate\":0.0,\"county_taxable_amount\":0.0,\"line_items\":[{\"city_amount\":0.0,\"city_tax_rate\":0.0,\"city_taxable_amount\":0.0,\"combined_tax_rate\":0.0625,\"county_amount\":0.0,\"county_tax_rate\":0.0,\"county_taxable_amount\":0.0,\"id\":\"1\",\"special_district_amount\":0.0,\"special_district_taxable_amount\":0.0,\"special_tax_rate\":0.0,\"state_amount\":0.94,\"state_sales_tax_rate\":0.0625,\"state_taxable_amount\":15.0,\"tax_collectable\":0.94,\"taxable_amount\":15.0}],\"special_district_tax_collectable\":0.0,\"special_district_taxable_amount\":0.0,\"special_tax_rate\":0.0,\"state_tax_collectable\":0.94,\"state_tax_rate\":0.0625,\"state_taxable_amount\":15.0,\"tax_collectable\":0.94,\"taxable_amount\":15.0},\"freight_taxable\":false,\"has_nexus\":true,\"jurisdictions\":{\"country\":\"US\",\"state\":\"MA\"},\"order_total_amount\":16.5,\"rate\":0.0625,\"shipping\":1.5,\"tax_source\":\"destination\",\"taxable_amount\":15.0}}")
                    })
                    .Verifiable();

            var mockClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("https://api.mocktaxjar.com/v2/")
            };

            var taxJarClient = new TaxJarClient(mockClient);
            var taxJarCalculator = new TaxJarCalculator(taxJarClient);

            var fromAddress = new Address(country: "US", zip: "02110", state: "MA", city: "Boston", street: "1 Central Wharf");
            var toAddress = new Address(country: "US", zip: "01608", state: "MA", city: "Worcester", street: "455 Main St");
            var nexusAdddress = new NexusAddress(id: "Main Location", country: "US", zip: "02110", state: "MA", city: "Boston", street: "1 Central Wharf");
            var item = new Item(1, 1, null, 15, 0);

            var salesOrder = new SalesOrder(fromAddress: fromAddress, toAddress: toAddress, 1.5F, items: new List<Item> { item }, nexusAddresses: new List<NexusAddress> { nexusAdddress });
            var salesTax = await taxJarCalculator.GetSalesTaxForOrderAsync(salesOrder);

            Assert.NotNull(salesTax);
            Assert.Equal(0.94F, salesTax);
        }

        [Fact]
        public async Task GetSalesTaxForOrder_ClothingExemptions_NoNexus_TwoItems()
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
                        Content = new StringContent("{\"tax\":{\"amount_to_collect\":1.98,\"breakdown\":{\"city_tax_collectable\":0.0,\"city_tax_rate\":0.0,\"city_taxable_amount\":0.0,\"combined_tax_rate\":0.05218,\"county_tax_collectable\":1.66,\"county_tax_rate\":0.04375,\"county_taxable_amount\":37.93,\"line_items\":[{\"city_amount\":0.0,\"city_tax_rate\":0.0,\"city_taxable_amount\":0.0,\"combined_tax_rate\":0.04375,\"county_amount\":0.87,\"county_tax_rate\":0.04375,\"county_taxable_amount\":19.99,\"id\":\"1\",\"special_district_amount\":0.0,\"special_district_taxable_amount\":0.0,\"special_tax_rate\":0.0,\"state_amount\":0.0,\"state_sales_tax_rate\":0.0,\"state_taxable_amount\":0.0,\"tax_collectable\":0.87,\"taxable_amount\":19.99},{\"city_amount\":0.0,\"city_tax_rate\":0.0,\"city_taxable_amount\":0.0,\"combined_tax_rate\":0.04375,\"county_amount\":0.44,\"county_tax_rate\":0.04375,\"county_taxable_amount\":9.95,\"id\":\"2\",\"special_district_amount\":0.0,\"special_district_taxable_amount\":0.0,\"special_tax_rate\":0.0,\"state_amount\":0.0,\"state_sales_tax_rate\":0.0,\"state_taxable_amount\":0.0,\"tax_collectable\":0.44,\"taxable_amount\":9.95}],\"shipping\":{\"city_amount\":0.0,\"city_tax_rate\":0.0,\"city_taxable_amount\":0.0,\"combined_tax_rate\":0.08375,\"county_amount\":0.35,\"county_tax_rate\":0.04375,\"county_taxable_amount\":7.99,\"special_district_amount\":0.0,\"special_tax_rate\":0.0,\"special_taxable_amount\":0.0,\"state_amount\":0.32,\"state_sales_tax_rate\":0.04,\"state_taxable_amount\":7.99,\"tax_collectable\":0.67,\"taxable_amount\":7.99},\"special_district_tax_collectable\":0.0,\"special_district_taxable_amount\":0.0,\"special_tax_rate\":0.0,\"state_tax_collectable\":0.32,\"state_tax_rate\":0.04,\"state_taxable_amount\":7.99,\"tax_collectable\":1.98,\"taxable_amount\":37.93},\"freight_taxable\":true,\"has_nexus\":true,\"jurisdictions\":{\"city\":\"MAHOPAC\",\"country\":\"US\",\"county\":\"PUTNAM\",\"state\":\"NY\"},\"order_total_amount\":37.93,\"rate\":0.05218,\"shipping\":7.99,\"tax_source\":\"destination\",\"taxable_amount\":37.93}}")
                    })
                    .Verifiable();

            var mockClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("https://api.mocktaxjar.com/v2/")
            };

            var taxJarClient = new TaxJarClient(mockClient);
            var taxJarCalculator = new TaxJarCalculator(taxJarClient);

            var fromAddress = new Address(country: "US", zip: "12054", state: "NY", city: "Delmar", street: null);
            var toAddress = new Address(country: "US", zip: "10541", state: "NY", city: "Mahopac", street: null);
            var item1 = new Item(quantity: 1, unitPrice: 19.99F, taxCode: "20010");
            var item2 = new Item(quantity: 1, unitPrice: 9.95F, taxCode: "20010");

            var salesOrder = new SalesOrder(fromAddress: fromAddress, toAddress: toAddress, 1.5F, items: new List<Item> { item1, item2 }, nexusAddresses: null);
            var salesTax = await taxJarCalculator.GetSalesTaxForOrderAsync(salesOrder);

            Assert.NotNull(salesTax);
            Assert.Equal(1.98F, salesTax);
        }
    }
}