namespace TaxCalculator.API.Models
{
    public class GetRatesForLocationRequest
    {
        public string? Country { get; set; }
        public string? Zip { get; set; }
        public string? State { get; set; }
        public string? City { get; set; }
        public string? Street { get; set; }
    }
}
