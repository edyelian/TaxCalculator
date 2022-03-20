using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using TaxCalculator.API.Models;
using TaxCalculator.Services;
using TaxCalculator.Services.Models;

namespace TaxCalculator.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaxCalculatorController : ControllerBase
    {
        private readonly TaxCalculatorService _taxService;

        public TaxCalculatorController(TaxCalculatorService taxService)
        {
            _taxService = taxService;
        }

        [HttpGet("GetRatesForLocation")]
        [ProducesResponseType(typeof(GetRatesForLocationResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<GetRatesForLocationResponse>> GetRatesForLocation([FromQuery] GetRatesForLocationRequest request)
        {
            if (request == null || string.IsNullOrEmpty(request?.Zip))
                return BadRequest();

            var address = new Address(request?.Country, request?.Zip, request?.State, request?.City, request?.Street); // it's better to use AutoMapper
            var locationRates = await _taxService.GetRatesForLocationAsync(address);
            var resp = new GetRatesForLocationResponse(locationRates?.Zip,
                    locationRates?.Country,
                    locationRates?.CountryRate,
                    locationRates?.State,
                    locationRates?.StateRate,
                    locationRates?.County,
                    locationRates?.CountyRate,
                    locationRates?.City,
                    locationRates?.CityRate,
                    locationRates?.CombinedDistrictRate,
                    locationRates?.CombinedRate,
                    locationRates?.FreightTaxable,
                    locationRates?.Name,
                    locationRates?.StandardRate,
                    locationRates?.ReduceRate,
                    locationRates?.SuperReducedRate,
                    locationRates?.ParkingRate,
                    locationRates?.DistanceSalesThreshold); // it's better to use AutoMapper

            return Ok(resp ?? new GetRatesForLocationResponse());
        }
    }
}
