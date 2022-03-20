using System.Net.Http.Headers;
using TaxCalculator.Services;
using TaxCalculator.Services.Calculators.TaxJar;
using TaxCalculator.Services.Calculators.TaxJar.Interfaces;
using TaxCalculator.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHttpClient<ITaxJarClient, TaxJarClient>()
    .ConfigureHttpClient(httpClient =>
    {
        httpClient.BaseAddress = new Uri(builder.Configuration["TaxJar:BaseUrl"]);
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Token", $"token=\"{builder.Configuration["TaxJar:ApiKey"]}\"");
    });


builder.Services.AddScoped<TaxCalculatorService>();
builder.Services.AddScoped<ICalculator, TaxJarCalculator>();

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
