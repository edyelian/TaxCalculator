using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TaxCalculator.Services.Calculators.TaxJar.Models.Responses;
using TaxCalculator.Services.Interfaces;
using TaxCalculator.Services.Models;

namespace TaxCalculator.Services.Calculators.TaxJar
{
    public class TaxJarCalculator : ICalculator
    {
        private readonly HttpClient _client;

        public TaxJarCalculator(HttpClient client)
        {
            _client = client;
        }
        public async Task<Rate?> GetRatesForLocationAsync(Address address)
        {
            if (address == null || string.IsNullOrEmpty(address?.Zip))
                return null; // can be handled in a different way. e.g.: throw custom exception error.

            var parameters = new Dictionary<string, string>();

            if (!string.IsNullOrWhiteSpace(address.Country)) parameters["country"] = address.Country;
            if (!string.IsNullOrWhiteSpace(address.State)) parameters["state"] = address.State;
            if (!string.IsNullOrWhiteSpace(address.City)) parameters["city"] = address.City;
            if (!string.IsNullOrWhiteSpace(address.Street)) parameters["street"] = address.Street;

            var queryString = $"?{string.Join('&', parameters.Select(param => $"{param.Key}={param.Value}"))}";


            var request = new HttpRequestMessage(HttpMethod.Get, $"rates/{address.Zip}{queryString}");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            using (var response = await _client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();

                TaxJarRateRootResponse? resp = JsonSerializer.Deserialize<TaxJarRateRootResponse>(content);

                // it would be better if we use AutoMapper --> for simplicity I used this approach
                return new Rate(
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
        }

    }
}
