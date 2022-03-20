
namespace TaxCalculator.Services.Calculators.TaxJar.Models.Requests
{
    public class TaxJarRateRequest
    {
        public string? Country { get; set; }
        public string? Zip { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }

        public TaxJarRateRequest(string? country, string? zip, string? state, string? city, string? street)
        {
            Country = country;
            Zip = zip;
            State = state;
            City = city;
            Street = street;
        }
    }
}
