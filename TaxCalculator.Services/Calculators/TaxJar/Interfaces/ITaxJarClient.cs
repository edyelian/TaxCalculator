using System.Threading.Tasks;
using TaxCalculator.Services.Calculators.TaxJar.Models.Responses;
using TaxCalculator.Services.Calculators.TaxJar.Models.Requests;

namespace TaxCalculator.Services.Calculators.TaxJar.Interfaces
{
    public interface ITaxJarClient
    {
        Task<TaxJarRateRootResponse?> GetRatesForLocationAsync(TaxJarRateRequest req);
        Task<TaxJarSalesTaxForOrderResponse?> GetSalesTaxForOrderAsync(TaxJarOrderRequest req);
    }
}
