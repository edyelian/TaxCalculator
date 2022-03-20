using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Threading.Tasks;
using TaxCalculator.Services.Calculators.TaxJar.Interfaces;
using TaxCalculator.Services.Calculators.TaxJar.Models.Requests;
using TaxCalculator.Services.Calculators.TaxJar.Models.Responses;

namespace TaxCalculator.Services.Calculators.TaxJar
{
    public class TaxJarClient: ITaxJarClient
    {
        private readonly HttpClient _client;

        public TaxJarClient(HttpClient client)
        {
            _client = client;
        }
        public async Task<TaxJarRateRootResponse?> GetRatesForLocationAsync(TaxJarRateRequest req)
        {
            if (req == null || string.IsNullOrEmpty(req?.Zip))
                return null; // can be handled in a different way. e.g.: throw custom exception error.

            var parameters = new Dictionary<string, string>();

            if (!string.IsNullOrWhiteSpace(req.Country)) parameters["country"] = req.Country;
            if (!string.IsNullOrWhiteSpace(req.State)) parameters["state"] = req.State;
            if (!string.IsNullOrWhiteSpace(req.City)) parameters["city"] = req.City;
            if (!string.IsNullOrWhiteSpace(req.Street)) parameters["street"] = req.Street;

            var queryString = $"?{string.Join('&', parameters.Select(param => $"{param.Key}={param.Value}"))}";


            var request = new HttpRequestMessage(HttpMethod.Get, $"rates/{req.Zip}{queryString}");
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            using (var response = await _client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();

                TaxJarRateRootResponse? resp = JsonSerializer.Deserialize<TaxJarRateRootResponse>(content);

                return resp;
            }
        }

    }
}
