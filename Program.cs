using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using BlazorPerformanceApp;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

// register EventService as singleton (in-memory store for app lifetime)
builder.Services.AddSingleton<BlazorPerformanceApp.Services.EventService>();

builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

await builder.Build().RunAsync();
