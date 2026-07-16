using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using ValidateAddressUSPS.Server.Models;

namespace ValidateAddressUSPS.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UspsController : ControllerBase
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly UspsOptions _options;
        private readonly ILogger<UspsController> _logger;

        public UspsController(IHttpClientFactory httpClientFactory, IOptions<UspsOptions> options, ILogger<UspsController> logger)
        {
            _httpClientFactory = httpClientFactory;
            _options = options.Value;
            _logger = logger;
        }

        [HttpPost("validate")]
        public Task<IActionResult> ValidateAddress([FromBody] AddressDto address, CancellationToken cancellationToken)
        {
            // Basic validation: require at least one non-empty address field
            if (address == null)
                return Task.FromResult<IActionResult>(BadRequest(new { DisplayText = "Address is required." }));

            if (string.IsNullOrWhiteSpace(address.Address1) &&
                string.IsNullOrWhiteSpace(address.Address2) &&
                string.IsNullOrWhiteSpace(address.City) &&
                string.IsNullOrWhiteSpace(address.State) &&
                string.IsNullOrWhiteSpace(address.Zip5))
            {
                return Task.FromResult<IActionResult>(BadRequest(new { DisplayText = "Address is empty." }));
            }

            // Build a single large text field to pass back to the UI. This will contain
            // either the validation error or the validated address lines. For now we
            // return a mocked validated address since USPS API is commented out.
            var lines = new List<string>();
            lines.Add("Validated Address (mock)");
            if (!string.IsNullOrWhiteSpace(address.Address1)) lines.Add(address.Address1);
            if (!string.IsNullOrWhiteSpace(address.Address2)) lines.Add(address.Address2);
            var cityStateZip = string.Join(" ", new[] { address.City, address.State }.Where(s => !string.IsNullOrWhiteSpace(s)));
            if (!string.IsNullOrWhiteSpace(cityStateZip) || !string.IsNullOrWhiteSpace(address.Zip5))
            {
                var line = cityStateZip;
                if (!string.IsNullOrWhiteSpace(address.Zip5)) line = string.IsNullOrWhiteSpace(line) ? address.Zip5 : line + " " + address.Zip5;
                lines.Add(line);
            }
            lines.Add(string.Empty);
            lines.Add("Note: USPS API not called (mock result)");

            var displayText = string.Join("\r\n", lines);

            return Task.FromResult<IActionResult>(Ok(new { DisplayText = displayText }));

            /*
            // Original USPS API call (commented out until USPS UserId is available):
            if (string.IsNullOrWhiteSpace(_options.UserId))
                return StatusCode(StatusCodes.Status500InternalServerError, "USPS user id not configured.");

            var xml = new XElement("AddressValidateRequest",
                new XAttribute("USERID", _options.UserId),
                new XElement("Address",
                    new XAttribute("ID", "0"),
                    new XElement("Address1", address.Address1 ?? string.Empty),
                    new XElement("Address2", address.Address2 ?? string.Empty),
                    new XElement("City", address.City ?? string.Empty),
                    new XElement("State", address.State ?? string.Empty),
                    new XElement("Zip5", address.Zip5 ?? string.Empty),
                    new XElement("Zip4", string.Empty)
                )
            );

            var url = $"ShippingAPI.dll?API=Verify&XML={Uri.EscapeDataString(xml.ToString(SaveOptions.DisableFormatting))}";

            try
            {
                var client = _httpClientFactory.CreateClient("usps");
                var response = await client.GetAsync(url, cancellationToken);
                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogWarning("USPS returned {StatusCode}", response.StatusCode);
                    return StatusCode((int)response.StatusCode, "USPS service error");
                }

                var responseXml = await response.Content.ReadAsStringAsync(cancellationToken);

                var doc = XDocument.Parse(responseXml);
                var addrEl = doc.Descendants("Address").FirstOrDefault();
                var normalized2 = new
                {
                    Address1 = (string?)addrEl?.Element("Address1"),
                    Address2 = (string?)addrEl?.Element("Address2"),
                    City = (string?)addrEl?.Element("City"),
                    State = (string?)addrEl?.Element("State"),
                    Zip5 = (string?)addrEl?.Element("Zip5"),
                    Zip4 = (string?)addrEl?.Element("Zip4"),
                    ReturnText = (string?)addrEl?.Element("ReturnText")
                };

                return Ok(normalized2);
            }
            catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested)
            {
                _logger.LogInformation("Request cancelled by client");
                return StatusCode(StatusCodes.Status499ClientClosedRequest);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error calling USPS");
                return StatusCode(StatusCodes.Status502BadGateway, "Error calling USPS");
            }
            */
        }
    }
}
