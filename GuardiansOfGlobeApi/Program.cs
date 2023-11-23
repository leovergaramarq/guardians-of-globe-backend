using GuardiansOfGlobe.DataAccess.Models;
using GuardiansOfGlobeApi.Endpoints;
using Microsoft.EntityFrameworkCore;

var env = "dev";
//var env = "prod";

var builder = WebApplication.CreateBuilder(args);

IServiceCollection services = builder.Services;

// Connection
string stringConn = builder.Configuration.GetConnectionString(
    env == "dev" ? "OracleDBConnectionDev" : "OracleDBConnection"
);

services.AddDbContext<ModelContext>((optBuilder) => 
    optBuilder.UseOracle(stringConn, options => options.UseOracleSQLCompatibility("11"))
);

// Add services to the container.

services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
services.AddEndpointsApiExplorer();
services.AddSwaggerGen();

// Configure Endpoints
//Endpoints endpoints = new();
//endpoints.Init(services);

var app = builder.Build();

//app.UsePathBase("/api/v1");

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
