namespace TaxCalculator.API.Models
{
    public class GetRatesForLocationResponse
    {
        public string? Zip { get; set; }
        public string? Country { get; set; }
        public float? CountryRate { get; set; }
        public string? State { get; set; }
        public float? StateRate { get; set; }
        public string? County { get; set; }
        public float? CountyRate { get; set; }
        public string? City { get; set; }
        public float? CityRate { get; set; }
        public float? CombinedDistrictRate { get; set; }
        public float? CombinedRate { get; set; }
        public bool? FreightTaxable { get; set; }
        public string? Name { get; set; }
        public float? StandardRate { get; set; }
        public float? ReduceRate { get; set; }
        public float? SuperReducedRate { get; set; }
        public float? ParkingRate { get; set; }
        public float? DistanceSalesThreshold { get; set; }

        public GetRatesForLocationResponse(string? zip, string? country, float? countryRate,
            string? state, float? stateRate, string? county, float? countyRate,
            string? city, float? cityRate, float? combinedDistrictRate, float? combinedRate,
            bool? freightTaxable, string? name, float? standardRate, float? reducedRate, float? superReduceRate,
            float? parkingRate, float? distanceSalesThreshold)
        {
            Zip = zip;
            Country = country;
            CountryRate = countryRate;
            State = state;
            StateRate = stateRate;
            County = county;
            CountyRate = countyRate;
            City = city;
            CityRate = cityRate;
            CombinedDistrictRate = combinedDistrictRate;
            CombinedRate = combinedRate;
            FreightTaxable = freightTaxable;
            Name = name;
            StandardRate = standardRate;
            ReduceRate = reducedRate;
            SuperReducedRate = superReduceRate;
            ParkingRate = parkingRate;
            DistanceSalesThreshold = distanceSalesThreshold;
        }

        public GetRatesForLocationResponse()
        {

        }
    }
}
