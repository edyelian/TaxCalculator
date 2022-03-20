
namespace TaxCalculator.Services.Models
{
    public class LocationRate
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

        public LocationRate(string? zip, string? country = null, float? countryRate = null,
            string? state = null, float? stateRate = null, string? county = null, float? countyRate = null,
            string? city = null, float? cityRate = null, float? combinedDistrictRate = null, float? combinedRate = null, 
            bool? freightTaxable = null, string? name = null, float? standardRate = null, float? reducedRate = null, float? superReduceRate = null,
            float? parkingRate = null, float? distanceSalesThreshold = null)
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
    }
}
