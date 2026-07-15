# ValidateAddressUSPS

This solution is a small sample application that demonstrates an ASP.NET (.NET 10) backend and an Angular frontend.

Main features
- Server: provides a simple WeatherForecast API and a USPS address validation endpoint.
- Client: Angular app with two separate pages:
  - `/` (root) — Weather forecast page that fetches data from the server and displays a table of forecasts.
  - `/validate-address` — USPS address validation form that posts address data to the server and shows the result.

Notes
- The weather page includes a compact raw JSON dump at the bottom of the page containing the API response; this is intentionally left for debugging and verification of the data flow.
- The server-side USPS validation endpoint is implemented as a local proxy to external address validation services — for example the USPS Web Tools API or other geographical/address services (Azure Maps, Google Geocoding) — so in production it would forward requests to those external providers and return normalized validation results to the client.

How to run
1. Open `ValidateAddressUSPS.slnx` in Visual Studio 2026 and run the solution (it starts both the server and the client).
2. Or build and run the server and in the `validateaddressusps.client` folder run the Angular dev server (`ng serve`), making sure the proxy config forwards `/weatherforecast` to the backend.

## Screenshots

Weather component (standalone):

![Weather component standalone](./03%20modified%20weather%20component%20as%20standalone.png)

Validate address component (standalone):

![Validate address component standalone](./04%20validate%20address%20component%20as%20standalone.png)

