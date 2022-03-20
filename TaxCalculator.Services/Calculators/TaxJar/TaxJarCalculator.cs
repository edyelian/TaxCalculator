using System.Threading.Tasks;
using TaxCalculator.Services.Calculators.TaxJar.Interfaces;
using TaxCalculator.Services.Calculators.TaxJar.Models.Requests;
using TaxCalculator.Services.Interfaces;
using TaxCalculator.Services.Models;

namespace TaxCalculator.Services.Calculators.TaxJar
{
    public class TaxJarCalculator : ICalculator
    {
        private readonly ITaxJarClient _client;

        public TaxJarCalculator(ITaxJarClient client)
        {
            _client = client;
        }
        public async Task<LocationRate?> GetRatesForLocationAsync(Address address)
        {
            var req = new TaxJarRateRequest(address.Country, address.Zip, address.State,address.City, address.Street);

            var resp = await _client.GetRatesForLocationAsync(req);

            return resp is null? null : new LocationRate(
                    resp?.Rate?.Zip,
                    resp?.Rate?.Country,
                    string.IsNullOrEmpty(resp?.Rate?.CountryRate) ? null : float.Parse(resp?.Rate?.CountryRate),
                    resp?.Rate?.State,
                    string.IsNullOrEmpty(resp?.Rate?.StateRate) ? null : float.Parse(resp?.Rate?.StateRate),
                    resp?.Rate?.County,
                    string.IsNullOrEmpty(resp?.Rate?.CountyRate) ? null : float.Parse(resp?.Rate?.CountyRate),
                    resp?.Rate?.City,
                    string.IsNullOrEmpty(resp?.Rate?.CityRate) ? null : float.Parse(resp?.Rate?.CityRate),
                    string.IsNullOrEmpty(resp?.Rate?.CombinedDistrictRate) ? null : float.Parse(resp?.Rate?.CombinedDistrictRate),
                    string.IsNullOrEmpty(resp?.Rate?.CombinedRate) ? null : float.Parse(resp?.Rate?.CombinedRate),
                    resp?.Rate?.FreightTaxable,
                    resp?.Rate?.Name,
                    string.IsNullOrEmpty(resp?.Rate?.StandardRate) ? null : float.Parse(resp?.Rate?.StandardRate),
                    string.IsNullOrEmpty(resp?.Rate?.ReducedRate) ? null : float.Parse(resp?.Rate?.ReducedRate),
                    string.IsNullOrEmpty(resp?.Rate?.SuperReducedRate) ? null : float.Parse(resp?.Rate?.SuperReducedRate),
                    string.IsNullOrEmpty(resp?.Rate?.ParkingRate) ? null : float.Parse(resp?.Rate?.ParkingRate),
                    string.IsNullOrEmpty(resp?.Rate?.DistanceSaleThreshold) ? null : float.Parse(resp?.Rate?.DistanceSaleThreshold)
                    );
        }

        public async Task<float?> GetSalesTaxForOrderAsync(SalesOrder salesOrder)
        {
            var req = new TaxJarOrderRequest(salesOrder);

            var resp = await _client.GetSalesTaxForOrderAsync(req);

            return resp?.Tax?.AmountToCollect;
        }
    }
}
