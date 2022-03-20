using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaxCalculator.Services.Models
{
    public class SalesOrder
    {
        public Address? FromAddress { get; set; }
        public Address? ToAddress { get; set; }
        public float? Shipping { get; set; }
        public float? Amount => Items?.Count > 0 ? Items?.Sum(i => i.UnitPrice * i.Quantity) : 0.0F;
        public List<Item> Items { get; set; } = new List<Item>();
        public List<Address> NexusAddresses { get; set; } = new List<Address>();
    }

    public class Item
    {
        public Item(int? id = null, int? quantity = null, string? taxCode = null, float? unitPrice = null, float? discount = null)
        {
            Id = id;
            Quantity = quantity;
            TaxCode = taxCode;
            UnitPrice = unitPrice;
            Discount = discount;
        }

        public int? Id { get; set; }
        public int? Quantity { get; set; }
        public string? TaxCode { get; set; }
        public float? UnitPrice { get; set; }
        public float? Discount { get; set; }
    }
}
