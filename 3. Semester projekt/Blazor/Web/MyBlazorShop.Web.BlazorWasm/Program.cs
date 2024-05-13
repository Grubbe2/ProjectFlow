using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MyBlazorShop.Web.BlazorWasm;
using MyBlazorShop.Libraries.Services.Product;
using MyBlazorShop.Libraries.Services.Storage;
using MyBlazorShop.Libraries.Services.ShoppingCart;
using System.Net.Http.Headers;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var http = new HttpClient()
{
    BaseAddress = new Uri("https://localhost:7054/")
};
http.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue
{
    NoCache = true
};

// I .NET 6 har Blazor WASM ikke noget koncept om DI scopes. Derfor opfører de sig som singleton uanset
builder.Services.AddScoped(sp => http);
builder.Services.AddSingleton<IStorageService, StorageService>();
builder.Services.AddSingleton<IShoppingCartService, ShoppingCartService>();
builder.Services.AddTransient<IProductService, ProductService>();

builder.Services.AddBlazorBootstrap(); // Add this line

await builder.Build().RunAsync();
