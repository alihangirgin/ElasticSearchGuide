using ElasticSearch.Api.Models;
using ElasticSearch.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var elasticConfig = builder.Configuration.GetSection("Elastic").Get<ElasticSearchConfig>();
builder.Services.AddSingleton<IElasticSearchClient,ElasticSearchClient>(cfg => new ElasticSearchClient(elasticConfig ?? new()));
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddScoped<IECommerceService, ECommerceService>();

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
