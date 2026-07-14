using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ValidateAddressUSPS.Server.Models;

namespace ValidateAddressUSPS.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UspsController : ControllerBase
    {

        private readonly HttpClient _http;

        public UspsController(IHttpClientFactory httpClientFactory)
        {
            _http = httpClientFactory.CreateClient();
        }

        [HttpPost("validate")]
        public async Task<IActionResult> ValidateAddress([FromBody] AddressDto address)
        {
            string userId = "YOUR_USPS_USER_ID";

            // Build USPS XML request
            string xmlRequest = $@"
            <AddressValidateRequest USERID=""{userId}"">
                <Address ID=""0"">
                    <Address1>{address.Address1}</Address1>
                    <Address2>{address.Address2}</Address2>
                    <City>{address.City}</City>
                    <State>{address.State}</State>
                    <Zip5>{address.Zip5}</Zip5>
                    <Zip4></Zip4>
                </Address>
            </AddressValidateRequest>";

            // Prepare URL
            string url = $"https://secure.shippingapis.com/ShippingAPI.dll" +
                         $"?API=Verify&XML={Uri.EscapeDataString(xmlRequest)}";

            // Make the USPS request
            var uspsResponse = await _http.GetStringAsync(url);

            return Ok(uspsResponse);
        }
    }

}
