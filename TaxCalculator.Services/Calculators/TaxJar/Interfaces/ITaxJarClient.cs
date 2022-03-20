using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaxCalculator.Services.Models;
using TaxCalculator.Services.Calculators.TaxJar.Models.Responses;
using TaxCalculator.Services.Calculators.TaxJar.Models.Requests;

namespace TaxCalculator.Services.Calculators.TaxJar.Interfaces
{
    public interface ITaxJarClient
    {
        Task<TaxJarRateRootResponse?> GetRatesForLocationAsync(TaxJarRateRequest req);
    }
}
