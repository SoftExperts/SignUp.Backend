using Microsoft.Net.Http.Headers;
using SignUp.UI.Services;
using SignUp.UI.Services.Auth;
using System.Net.Mime;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add HttpContextAccessor and HttpClient services.
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();

// Register the HttpClientService and AuthService implementations.
builder.Services.AddTransient<IHttpClientService, HttpClientService>();
builder.Services.AddTransient<IAuthService, AuthService>();

// Configure named HttpClient for SignUpAPI.
builder.Services.AddHttpClient("SignUpAPI", x =>
{
    x.BaseAddress = new Uri("https://localhost:7280/v1/");
    x.DefaultRequestHeaders.Add(HeaderNames.Accept, MediaTypeNames.Application.Json);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // Use exception handler for non-development environments.
    app.UseExceptionHandler("/Home/Error");

    // Enable HTTP Strict Transport Security (HSTS).
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Register}/{id?}");

app.Run();
