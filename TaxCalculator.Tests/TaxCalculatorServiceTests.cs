using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.Services;
using TaxCalculator.Services.Interfaces;
using TaxCalculator.Services.Models;
using Xunit;

namespace TaxCalculator.Tests
{
    public class TaxCalculatorServiceTests
    {
        [Fact]
        public async Task GetRatesByUSLocation_Zip()
        {
            Mock<ICalculator> mockCalculator = new();
            var address = new Address(null, "55378", null, null, null);
            LocationRate? rate = new(zip: "55378", country: "US", countryRate: 0.0F, state: "MN", stateRate: 0.06875F, county: "SCOTT", countyRate: 0.005F, 
                            city: "PRIOR LAKE", cityRate: 0.0F, combinedDistrictRate: 0.0F, combinedRate: 0.07375F, freightTaxable: true);
            mockCalculator.Setup(c => c.GetRatesForLocationAsync(address)).Returns(Task.FromResult<LocationRate?>(rate));

            var taxCalculatorService = new TaxCalculatorService(mockCalculator.Object);

            var expectedLocationRates = await taxCalculatorService.GetRatesForLocationAsync(address);

            Assert.NotNull(expectedLocationRates);
            Assert.Equal(0.07375F, expectedLocationRates?.CombinedRate);
            Assert.Equal(0.06875F, expectedLocationRates?.StateRate);
            Assert.Equal(0.005F, expectedLocationRates?.CountyRate);
        }

        [Fact]
        public async Task GetRatesByUSLocation_ZipPlusFour()
        {
            Mock<ICalculator> mockCalculator = new();
            var address = new Address(null, "55378", null, null, null);
            LocationRate? rate = new(zip: "55378", country: "US", countryRate: 0.0F, state: "MN", stateRate: 0.06875F, county: "SCOTT", countyRate: 0.005F,
                            city: "Savage", cityRate: 0.0F, combinedDistrictRate: 0.0F, combinedRate: 0.07375F, freightTaxable: true);
            mockCalculator.Setup(c => c.GetRatesForLocationAsync(address)).Returns(Task.FromResult<LocationRate?>(rate));

            var taxCalculatorService = new TaxCalculatorService(mockCalculator.Object);

            var expectedLocationRates = await taxCalculatorService.GetRatesForLocationAsync(address);

            Assert.NotNull(expectedLocationRates);
            Assert.Equal(0.07375F, expectedLocationRates?.CombinedRate);
            Assert.Equal(0.06875F, expectedLocationRates?.StateRate);
            Assert.Equal(0.005F, expectedLocationRates?.CountyRate);
        }

        [Fact]
        public async Task GetRatesByUSLocation_FullAddress()
        {
            Mock<ICalculator> mockCalculator = new();
            var address = new Address("US", "05495-2086", "VT", "Williston", "312 Hurricane Lane");
            LocationRate? rate = new(zip: "05495-2086", country: "US", countryRate: 0.0F, state: "VT", stateRate: 0.06F, county: "CHITTENDEN", countyRate: 0.0F,
                            city: "WILLISTON", cityRate: 0.01F, combinedDistrictRate: 0.0F, combinedRate: 0.07F, freightTaxable: true);
            mockCalculator.Setup(c => c.GetRatesForLocationAsync(address)).Returns(Task.FromResult<LocationRate?>(rate));

            var taxCalculatorService = new TaxCalculatorService(mockCalculator.Object);

            var expectedLocationRates = await taxCalculatorService.GetRatesForLocationAsync(address);

            Assert.NotNull(expectedLocationRates);
            Assert.Equal(0.07F, expectedLocationRates?.CombinedRate);
            Assert.Equal(0.06F, expectedLocationRates?.StateRate);
            Assert.Equal(0.0F, expectedLocationRates?.CountyRate);
            Assert.Equal(0.01F, expectedLocationRates?.CityRate);
        }


        [Fact]
        public async Task GetSalesTaxForOrder_US()
        {
            Mock<ICalculator> mockCalculator = new();
            var fromAddress = new Address(country: "US", zip: "92093", state: "CA", city: "La Jolla", street: "9500 Gilman Drive");
            var toAddress = new Address(country: "US", zip: "90002", state: "CA", city: "Los Angeles", street: "1335 E 103rd St");
            var nexusAdddress = new NexusAddress(id: "Main Location", country: "US", zip: "92093", state: "CA", city: "La Jolla", street: "9500 Gilman Drive");
            var item = new Item(1, 1, "20010", 15, 0);

            var salesOrder = new SalesOrder(fromAddress: fromAddress, toAddress: toAddress, 1.5F, items: new List<Item> { item }, nexusAddresses: new List<NexusAddress> { nexusAdddress });
            var salesTax = 1.43F;
            mockCalculator.Setup(c => c.GetSalesTaxForOrderAsync(salesOrder)).Returns(Task.FromResult<float?>(salesTax));

            var taxCalculatorService = new TaxCalculatorService(mockCalculator.Object);
            
            var expectedSalesTax = await taxCalculatorService.GetSalesTaxForOrderAsync(salesOrder);

            Assert.NotNull(expectedSalesTax);
            Assert.Equal(1.43F, expectedSalesTax);
        }

        [Fact]
        public async Task GetSalesTaxForOrder_OriginBasedStates()
        {
            Mock<ICalculator> mockCalculator = new();
            var fromAddress = new Address(country: "US", zip: "78701", state: "TX", city: "Austin", street: "1100 Congress Ave");
            var toAddress = new Address(country: "US", zip: "77058", state: "TX", city: "Houston", street: "1601 E NASA Pkwy");
            var nexusAdddress = new NexusAddress(id: "Main Location", country: "US", zip: "78701", state: "TX", city: "Austin", street: "1100 Congress Ave");
            var item = new Item(1, 1, null, 15, 0);

            var salesOrder = new SalesOrder(fromAddress: fromAddress, toAddress: toAddress, 1.5F, items: new List<Item> { item }, nexusAddresses: new List<NexusAddress> { nexusAdddress });
            var salesTax = 1.36F;
            mockCalculator.Setup(c => c.GetSalesTaxForOrderAsync(salesOrder)).Returns(Task.FromResult<float?>(salesTax));

            var taxCalculatorService = new TaxCalculatorService(mockCalculator.Object);

            var expectedSalesTax = await taxCalculatorService.GetSalesTaxForOrderAsync(salesOrder);

            Assert.NotNull(expectedSalesTax);
            Assert.Equal(1.36F, expectedSalesTax);
        }

        [Fact]
        public async Task GetSalesTaxForOrder_ShippingExemptions()
        {
            Mock<ICalculator> mockCalculator = new();
            var fromAddress = new Address(country: "US", zip: "02110", state: "MA", city: "Boston", street: "1 Central Wharf");
            var toAddress = new Address(country: "US", zip: "01608", state: "MA", city: "Worcester", street: "455 Main St");
            var nexusAdddress = new NexusAddress(id: "Main Location", country: "US", zip: "02110", state: "MA", city: "Boston", street: "1 Central Wharf");
            var item = new Item(1, 1, null, 15, 0);

            var salesOrder = new SalesOrder(fromAddress: fromAddress, toAddress: toAddress, 1.5F, items: new List<Item> { item }, nexusAddresses: new List<NexusAddress> { nexusAdddress });
            var salesTax = 0.94F;
            mockCalculator.Setup(c => c.GetSalesTaxForOrderAsync(salesOrder)).Returns(Task.FromResult<float?>(salesTax));

            var taxCalculatorService = new TaxCalculatorService(mockCalculator.Object);

            var expectedSalesTax = await taxCalculatorService.GetSalesTaxForOrderAsync(salesOrder);

            Assert.NotNull(expectedSalesTax);
            Assert.Equal(0.94F, expectedSalesTax);
        }

        [Fact]
        public async Task GetSalesTaxForOrder_ClothingExemptions_NoNexus_TwoItems()
        {
            Mock<ICalculator> mockCalculator = new();
            var fromAddress = new Address(country: "US", zip: "12054", state: "NY", city: "Delmar", street: null);
            var toAddress = new Address(country: "US", zip: "10541", state: "NY", city: "Mahopac", street: null);
            var item1 = new Item(quantity: 1, unitPrice: 19.99F, taxCode: "20010");
            var item2 = new Item(quantity: 1, unitPrice: 9.95F, taxCode: "20010");

            var salesOrder = new SalesOrder(fromAddress: fromAddress, toAddress: toAddress, 1.5F, items: new List<Item> { item1, item2 }, nexusAddresses: null);
            var salesTax = 1.98F;
            mockCalculator.Setup(c => c.GetSalesTaxForOrderAsync(salesOrder)).Returns(Task.FromResult<float?>(salesTax));

            var taxCalculatorService = new TaxCalculatorService(mockCalculator.Object);

            var expectedSalesTax = await taxCalculatorService.GetSalesTaxForOrderAsync(salesOrder);

            Assert.NotNull(expectedSalesTax);
            Assert.Equal(1.98F, expectedSalesTax);
        }
    }
}
