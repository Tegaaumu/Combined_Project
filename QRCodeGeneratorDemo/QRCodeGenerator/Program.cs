using Microsoft.Extensions.Logging.Console;
using QRCodeGenerator.UrlShortener.Services;

var builder = WebApplication.CreateBuilder(args);
//builder.Host.ConfigureLogging(logging =>
//{
//    logging.ClearProviders();
//    logging.AddConsole(options =>
//    {
//        options.FormatterName = ConsoleFormatterNames.Systemd; // or other suitable formatter
//        options.TimestampFormat = "[HH:mm:ss] ";
//    });
//});
builder.Logging.ClearProviders(); 
builder.Logging.AddConsole(options =>
{
    options.FormatterName = ConsoleFormatterNames.Systemd; // or other suitable formatter
    //options.TimestampFormat =  "[HH:mm:ss] ";
});

builder.Services.AddHttpClient<IUrlShortenerServices, UrlShortenerServices>(client =>
    client.BaseAddress = new Uri("https://localhost:7171/")
);



// Add services to the container.
builder.Services.AddControllersWithViews();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
