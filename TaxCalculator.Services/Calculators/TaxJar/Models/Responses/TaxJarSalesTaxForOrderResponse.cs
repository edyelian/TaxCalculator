using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace TaxCalculator.Services.Calculators.TaxJar.Models.Responses
{
    public class TaxJarSalesTaxForOrderResponse
    {
        [JsonPropertyName("tax")]
        public Tax? Tax { get; set; }
    }

    public class Jurisdictions
    {
        [JsonPropertyName("country")]
        public string? Country { get; set; }

        [JsonPropertyName("state")]
        public string? State { get; set; }

        [JsonPropertyName("county")]
        public string? County { get; set; }

        [JsonPropertyName("city")]
        public string? City { get; set; }
    }

    public class LineItem
    {
        [JsonPropertyName("id")]
        public string? Id { get; set; }

        [JsonPropertyName("taxable_amount")]
        public float? TaxableAmount { get; set; }

        [JsonPropertyName("tax_collectable")]
        public float? TaxCollectable { get; set; }

        [JsonPropertyName("combined_tax_rate")]
        public float? CombinedTaxRate { get; set; }

        [JsonPropertyName("state_taxable_amount")]
        public float? StateTaxableAmount { get; set; }

        [JsonPropertyName("state_sales_tax_rate")]
        public float? StateSalesTaxRate { get; set; }

        [JsonPropertyName("state_amount")]
        public float? StateAmount { get; set; }

        [JsonPropertyName("county_taxable_amount")]
        public float? CountyTaxableAmount { get; set; }

        [JsonPropertyName("county_tax_rate")]
        public float? CountyTaxRate { get; set; }

        [JsonPropertyName("county_amount")]
        public float? CountyAmount { get; set; }

        [JsonPropertyName("city_taxable_amount")]
        public float? CityTaxableAmount { get; set; }

        [JsonPropertyName("city_tax_rate")]
        public float? CityTaxRate { get; set; }

        [JsonPropertyName("city_amount")]
        public float? CityAmount { get; set; }

        [JsonPropertyName("special_district_taxable_amount")]
        public float? SpecialDistrictTaxableAmount { get; set; }

        [JsonPropertyName("special_tax_rate")]
        public float? SpecialTaxRate { get; set; }

        [JsonPropertyName("special_district_amount")]
        public float? SpecialDistrictAmount { get; set; }
    }

    public class Breakdown
    {
        [JsonPropertyName("taxable_amount")]
        public float? TaxableAmount { get; set; }

        [JsonPropertyName("tax_collectable")]
        public float? TaxCollectable { get; set; }

        [JsonPropertyName("combined_tax_rate")]
        public float? CombinedTaxRate { get; set; }

        [JsonPropertyName("state_taxable_amount")]
        public float? StateTaxableAmount { get; set; }

        [JsonPropertyName("state_tax_rate")]
        public float? StateTaxRate { get; set; }

        [JsonPropertyName("state_tax_collectable")]
        public float? StateTaxCollectable { get; set; }

        [JsonPropertyName("county_taxable_amount")]
        public float? CountyTaxableAmount { get; set; }

        [JsonPropertyName("county_tax_rate")]
        public float? CountyTaxRate { get; set; }

        [JsonPropertyName("county_tax_collectable")]
        public float? CountyTaxCollectable { get; set; }

        [JsonPropertyName("city_taxable_amount")]
        public float? CityTaxableAmount { get; set; }

        [JsonPropertyName("city_tax_rate")]
        public float? CityTaxRate { get; set; }

        [JsonPropertyName("city_tax_collectable")]
        public float? CityTaxCollectable { get; set; }

        [JsonPropertyName("special_district_taxable_amount")]
        public float? SpecialDistrictTaxableAmount { get; set; }

        [JsonPropertyName("special_tax_rate")]
        public float? SpecialTaxRate { get; set; }

        [JsonPropertyName("special_district_tax_collectable")]
        public float? SpecialDistrictTaxCollectable { get; set; }

        [JsonPropertyName("line_items")]
        public List<LineItem>? LineItems { get; set; }
    }

    public class Tax
    {
        [JsonPropertyName("order_total_amount")]
        public float? OrderTotalAmount { get; set; }

        [JsonPropertyName("shipping")]
        public float? Shipping { get; set; }

        [JsonPropertyName("taxable_amount")]
        public float? TaxableAmount { get; set; }

        [JsonPropertyName("amount_to_collect")]
        public float? AmountToCollect { get; set; }

        [JsonPropertyName("rate")]
        public float? Rate { get; set; }

        [JsonPropertyName("has_nexus")]
        public bool? HasNexus { get; set; }

        [JsonPropertyName("freight_taxable")]
        public bool? FreightTaxable { get; set; }

        [JsonPropertyName("tax_source")]
        public string? TaxSource { get; set; }

        [JsonPropertyName("jurisdictions")]
        public Jurisdictions? Jurisdictions { get; set; }

        [JsonPropertyName("breakdown")]
        public Breakdown? Breakdown { get; set; }
    }
}
