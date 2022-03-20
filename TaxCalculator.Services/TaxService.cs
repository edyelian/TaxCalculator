using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.Services.Interfaces;
using TaxCalculator.Services.Models;

namespace TaxCalculator.Services
{
    public class TaxService
    {
        private readonly ICalculator _calculator;

        public TaxService(ICalculator calculator)
        {
            _calculator = calculator;
        }

        public async Task<Rate?> GetRatesForLocationAsync(Address address)
        {
            return await _calculator.GetRatesForLocationAsync(address);
        }
    }
}
