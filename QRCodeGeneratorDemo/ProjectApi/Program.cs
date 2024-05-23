using Microsoft.EntityFrameworkCore;
using ProjectApi;
using ProjectApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(o =>
{
    o.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection"));
});
builder.Services.AddScoped<UrlShorteningService>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
//builder.Services.AddApiVersioning();
//builder.Services.AddVersionedApiExplorer();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
