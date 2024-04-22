using AutoMapper;
using DbUp;
using JustclickCoreModules.Validators;
using MagicVilla_DB.Data;
using MagicVilla_DB.Data.Repositories.Implementation;
using MagicVilla_DB.Exceptions;
using MagicVilla_DB.Mappers;
using MagicVilla_DB.Services;
using MagicVilla_DB.Services.Proxies;
using MagicVilla_DB.Utils;
using MagicVilla_DB.Utils.Filters;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;
configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
configuration.AddEnvironmentVariables();

string connectionString = configuration["ConnectionStrings:DefaultPostgreConnection"]
    .Replace("__DB_SERVER__", configuration["DB_SERVER"])
    .Replace("__DB_PORT__", configuration["DB_PORT"])
    .Replace("__DB_NAME__", configuration["DB_NAME"])
    .Replace("__DB_USERNAME__", configuration["DB_USERNAME"])
    .Replace("__DB_PASSWORD__", configuration["DB_PASSWORD"]);

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(option => {
    //option.UseNpgsql(builder.Configuration.GetConnectionString("DefaultPostgreConnection"));
    option.UseNpgsql(connectionString);
});


//DbUp execute
var upgrader = DeployChanges.To
                .PostgresqlDatabase(connectionString)
                .WithScriptsFromFileSystem("DbUp_Migrations")
                .Build();
var result = upgrader.PerformUpgrade();
if(result.Successful)
{
    System.Diagnostics.Debug.WriteLine("DbUp Execute Success!");
}else
{
    System.Diagnostics.Debug.WriteLine(result.Error.Message);
}

//HttpClient config
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();
builder.Services.AddScoped(typeof(HttpClientService<>));

//Mapper config
IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

//Global Exception Handler
builder.Services.AddTransient<GlobalExceptionHandler>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<RequestValidator>();
builder.Services.AddScoped<TownRepository>();
builder.Services.AddScoped<VillaRepository>();
builder.Services.AddScoped<TownService>();
builder.Services.AddScoped<VillaService>();

//proxy services
builder.Services.AddScoped<CouponProxyService>();

//filter,searching,sorting util
builder.Services.AddScoped(typeof(FilterUtil<>));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<GlobalExceptionHandler>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
