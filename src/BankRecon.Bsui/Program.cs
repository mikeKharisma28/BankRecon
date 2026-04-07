using BankRecon.Bsui;
using BankRecon.Bsui.Client;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// In development, Bsui and WebApi run as separate projects on different ports.
// In production, Blazor WASM is hosted by the WebApi, so BaseAddress is correct.
var apiBaseUrl = builder.Environment.IsDevelopment()
    ? "https://localhost:57134"
    : "https://localhost:57134";

// Register MudBlazor services
builder.Services.AddBsuiClient(apiBaseUrl);
builder.Services.AddMudServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
