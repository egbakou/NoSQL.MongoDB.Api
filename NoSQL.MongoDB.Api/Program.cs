using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Bson.Serialization.Conventions;
using NoSQL.MongoDB.Api.Extensions;
using NoSQL.MongoDB.Api.Features.Actors;
using NoSQL.MongoDB.Api.Features.Movies;
using NoSQL.MongoDB.Api.Helpers;
using NoSQL.MongoDB.Api.Interfaces.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMongoDb();
builder.Services.AddScoped<IMovieService, MovieService>();
builder.Services.AddScoped<IActorService, ActorService>();

builder.Services.AddControllers();

// enforce lowercase for routes
builder.Services.Configure<RouteOptions>(options =>
{
    options.LowercaseUrls = true;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.OperationFilter<HttpResultsOperationFilter>();
    options.SwaggerDoc("v1", new() { Title = "NoSQL.MongoDB.Api", Version = "v1" });
    // add xml comments
    var xmlFilePath = Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");
    options.IncludeXmlComments(xmlFilePath);
});

// A convention that sets the element name the same as the member name with the first character lower cased.
var camelCaseConvention = new ConventionPack { new CamelCaseElementNameConvention() };
ConventionRegistry.Register("CamelCase", camelCaseConvention, _ => true);
// A convention that sets whether to ignore extra elements encountered during deserialization.
var ignoreExtraElementsConvention = new ConventionPack { new IgnoreExtraElementsConvention(true) };
ConventionRegistry.Register("IgnoreExtraElements", ignoreExtraElementsConvention, _ => true);

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