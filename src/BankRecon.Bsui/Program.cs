using BankRecon.Bsui;
using BankRecon.Bsui.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// In development, Bsui and WebApi run as separate projects on different ports.
// In production, Blazor WASM is hosted by the WebApi, so BaseAddress is correct.
var apiBaseUrl = builder.HostEnvironment.IsDevelopment()
    ? "https://localhost:57134"
    : builder.HostEnvironment.BaseAddress;

builder.Services.AddBsuiClient(apiBaseUrl);
builder.Services.AddMudServices();

await builder.Build().RunAsync();
