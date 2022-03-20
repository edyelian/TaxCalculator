using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using TaxCalculator.Services.Models;

namespace TaxCalculator.Services.Calculators.TaxJar.Models.Requests
{
    public class TaxJarNexusAddress
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("country")]
        public string? Country { get; set; }

        [JsonPropertyName("zip")]
        public string? Zip { get; set; }

        [JsonPropertyName("state")]
        public string? State { get; set; }

        [JsonPropertyName("city")]
        public string? City { get; set; }

        [JsonPropertyName("street")]
        public string? Street { get; set; }
    }

    public class TaxJarLineItem
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("quantity")]
        public int? Quantity { get; set; }

        [JsonPropertyName("product_tax_code")]
        public string? ProductTaxCode { get; set; }

        [JsonPropertyName("unit_price")]
        public float? UnitPrice { get; set; }

        [JsonPropertyName("discount")]
        public float? Discount { get; set; }
    }

    public class TaxJarOrderRequest
    {
        [JsonPropertyName("from_country")]
        public string? FromCountry { get; set; }

        [JsonPropertyName("from_zip")]
        public string? FromZip { get; set; }

        [JsonPropertyName("from_state")]
        public string? FromState { get; set; }

        [JsonPropertyName("from_city")]
        public string? FromCity { get; set; }

        [JsonPropertyName("from_street")]
        public string? FromStreet { get; set; }

        [JsonPropertyName("to_country")]
        public string? ToCountry { get; set; }

        [JsonPropertyName("to_zip")]
        public string? ToZip { get; set; }

        [JsonPropertyName("to_state")]
        public string? ToState { get; set; }

        [JsonPropertyName("to_city")]
        public string? ToCity { get; set; }

        [JsonPropertyName("to_street")]
        public string? ToStreet { get; set; }

        [JsonPropertyName("amount")]
        public float? Amount { get; set; }

        [JsonPropertyName("shipping")]
        public float? Shipping { get; set; }

        [JsonPropertyName("nexus_addresses")]
        public List<TaxJarNexusAddress>? NexusAddresses { get; set; }

        [JsonPropertyName("line_items")]
        public List<TaxJarLineItem>? LineItems { get; set; }

        public TaxJarOrderRequest(SalesOrder salesOrder)
        {
            FromCountry = salesOrder?.FromAddress?.Country;
            FromZip = salesOrder?.FromAddress?.Zip;
            FromState = salesOrder?.FromAddress?.State;
            FromCity = salesOrder?.FromAddress?.City;
            FromStreet = salesOrder?.FromAddress?.Street;
            ToCountry = salesOrder?.ToAddress?.Country;
            ToZip = salesOrder?.ToAddress?.Zip;
            ToState = salesOrder?.ToAddress?.State;
            ToCity = salesOrder?.ToAddress?.City;
            ToStreet = salesOrder?.ToAddress?.Street;
            Amount = salesOrder?.Amount;
            Shipping = salesOrder?.Shipping;

            NexusAddresses = salesOrder?.NexusAddresses?.Select(a =>
                new TaxJarNexusAddress()
                {
                    Id = a.Id,
                    Country = a.Country,
                    Zip = a.Zip,
                    State = a.State,
                    City = a.City,
                    Street = a.Street
                }).ToList();

            LineItems = salesOrder?.Items?.Select(s =>
                new TaxJarLineItem()
                {
                    Id = s.Id.ToString(),
                    Quantity = s.Quantity,
                    ProductTaxCode = s.TaxCode,
                    UnitPrice = s.UnitPrice,
                    Discount = s.Discount
                }).ToList();
        }

        public string ToJson() => JsonSerializer.Serialize(this, new JsonSerializerOptions { DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull, WriteIndented = true });
    }
}
