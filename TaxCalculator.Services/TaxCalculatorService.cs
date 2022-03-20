using System.Threading.Tasks;
using TaxCalculator.Services.Interfaces;
using TaxCalculator.Services.Models;

namespace TaxCalculator.Services
{
    public class TaxCalculatorService
    {
        private readonly ICalculator _calculator;

        public TaxCalculatorService(ICalculator calculator)
        {
            _calculator = calculator;
        }

        public async Task<LocationRate?> GetRatesForLocationAsync(Address address)
        {
            return await _calculator.GetRatesForLocationAsync(address);
        }

        public async Task<float?> GetSalesTaxForOrderAsync(SalesOrder salesOrder)
        {
            return await _calculator.GetSalesTaxForOrderAsync(salesOrder);
        }
    }
}
