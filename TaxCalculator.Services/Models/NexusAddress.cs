using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxCalculator.Services.Models
{
    public class NexusAddress : Address
    {
        public string? Id { get; set; }
        public NexusAddress(string? id, string? country, string? zip, string? state, string? city, string? street) : 
                           base(country, zip, state, city, street)
        {
            Id = id;
            Country = country;
            Zip = zip;
            State = state;
            City = city;
            Street = street;
        }
    }
}
