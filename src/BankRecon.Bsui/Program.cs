using BankRecon.Bsui;
using BankRecon.Bsui.Client;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

// Register Bsui.Client services (API clients)
builder.Services.AddBsuiClient(builder.HostEnvironment.BaseAddress);

builder.Services.AddMudServices();

await builder.Build().RunAsync();
