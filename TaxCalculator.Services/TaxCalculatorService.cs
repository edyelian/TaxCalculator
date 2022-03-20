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

        public async Task<Rate?> GetRatesForLocationAsync(Address address)
        {
            return await _calculator.GetRatesForLocationAsync(address);
        }
    }
}
