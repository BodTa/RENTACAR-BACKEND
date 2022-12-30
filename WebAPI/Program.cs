using Autofac;
using Autofac.Core;
using Autofac.Extensions.DependencyInjection;
using Business.DependencyResolvers.Autofac;
using Core.Extentions;
using Core.IoC;
using Core.Utilites.Security.Encrypiton;
using Core.Utilites.Security.Jwt;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Net;
using System.Reflection.PortableExecutable;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory())
    .ConfigureContainer<ContainerBuilder>(builder =>
    {
        builder.RegisterModule(new AutofacBusinessModule());
    });

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors();
builder.Services.AddSwaggerGen(x =>
{
    x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the bearer scheme",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });
    x.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {new OpenApiSecurityScheme
            {
            Reference = new OpenApiReference
            {
                Id = "Bearer",
                Type = ReferenceType.SecurityScheme,
            }
            }, new List<string>()}
    });
});
TokenOptions? tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidIssuer = tokenOptions.Issuer,
        ValidAudience = tokenOptions.Audience,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey)
    };

    options.Events = new JwtBearerEvents
    {
        OnChallenge = context =>
        {
            Console.WriteLine("OnChallange: ");
            return Task.CompletedTask;
        },
        OnAuthenticationFailed = context =>
        {
            Console.WriteLine("OnAuthenticationFailed:");
            return Task.CompletedTask;
        },
        OnMessageReceived = context =>
        {
            Console.WriteLine("OnMessageReceived:");
            return Task.CompletedTask;
        },
        OnTokenValidated = context =>
        {
            Console.WriteLine("OnTokenValidated:");
            return Task.CompletedTask;
        },
    };
});
;
builder.Services.AddDependencyResolvers(new ICoreModule[] { new CoreModule() });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
//app.ConfigureCustomExceptionMiddleware();

app.UseCors(c => c.WithOrigins("http://localhost:3000", "http://localhost:4200").AllowCredentials().AllowAnyHeader());

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
