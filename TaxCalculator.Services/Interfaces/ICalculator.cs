using System.Threading.Tasks;
using TaxCalculator.Services.Models;

namespace TaxCalculator.Services.Interfaces
{
    public interface ICalculator
    {
        Task<Rate?> GetRatesForLocationAsync(Address address);
    }
}
