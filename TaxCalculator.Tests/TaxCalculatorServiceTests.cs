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
        public void GetRatesByUSLocation_Zip()
        {
            Mock<ICalculator> mockCalculator = new();
            var address = new Address(null, "55378", null, null, null);
            Rate? rate = new(zip: "55378", country: "US", countryRate: 0.0F, state: "MN", stateRate: 0.06875F, county: "SCOTT", countyRate: 0.005F, 
                            city: "PRIOR LAKE", cityRate: 0.0F, combinedDistrictRate: 0.0F, combinedRate: 0.07375F, freightTaxable: true, 
                            name: null, standardRate: null, reducedRate: null, superReduceRate: null, parkingRate: null, distanceSalesThreshold: null);
            mockCalculator.Setup(c => c.GetRatesForLocationAsync(address)).Returns(Task.FromResult<Rate?>(rate));

            var taxCalculatorService = new TaxCalculatorService(mockCalculator.Object);

            Assert.NotNull(rate);
            Assert.Equal(0.07375F, rate?.CombinedRate);
        }
    }
}
