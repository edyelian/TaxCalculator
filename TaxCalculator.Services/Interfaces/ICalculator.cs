using System.Threading.Tasks;
using TaxCalculator.Services.Models;

namespace TaxCalculator.Services.Interfaces
{
    public interface ICalculator
    {
        Task<LocationRate?> GetRatesForLocationAsync(Address address);
        Task<float?> GetSalesTaxForOrderAsync(SalesOrder salesOrder);
    }
}
