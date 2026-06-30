using System.Text;
using ApiStudio.Application.Common.Interfaces;
using ApiStudio.Application.Workspaces.Commands;
using ApiStudio.HttpEngine;
using ApiStudio.Persistence;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using ApiStudio.Application.Authentication.Interfaces;
using ApiStudio.Infrastructure.ActiveDirectory;
using Microsoft.AspNetCore.Identity;
using ApiStudio.Infrastructure.Authentication.JWT;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using ApiStudio.Api;
using ApiStudio.Infrastructure.Authentication.Services;
using ApiStudio.Infrastructure.Authentication.Models;
using ApiStudio.Application.Workspaces.Interfaces;
using ApiStudio.Persistence.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
AddDatabaseContext(builder);

AuthenticationConfigs(builder);

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(CreateWorkspaceCommand).Assembly);
});


builder.Services.AddValidatorsFromAssembly(typeof(CreateWorkspaceValidator).Assembly);


builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddScoped<IWorkspaceQueryService, WorkspaceQueryService>();

builder.Services.AddControllers();

builder.Services.AddHttpEngine();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy
                .WithOrigins("http://localhost:4200")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.MapControllers();
app.UseHttpsRedirection();
app.UseCors("AllowFrontend");
app.UseExceptionHandler();
app.UseAuthentication();

app.UseAuthorization();
app.Run();

void AddDatabaseContext(WebApplicationBuilder webApplicationBuilder)
{
    webApplicationBuilder.Services.AddDbContext<ApplicationDbContext>(options =>
    {
        options.UseSqlServer(
            webApplicationBuilder.Configuration.GetConnectionString("DefaultConnection"));
    });
    webApplicationBuilder.Services
        .AddIdentity<ApplicationUser, ApplicationRole>()
        .AddEntityFrameworkStores<ApplicationDbContext>()
        .AddDefaultTokenProviders();

    webApplicationBuilder.Services.AddScoped<IApplicationDbContext>(sp =>
        sp.GetRequiredService<ApplicationDbContext>());

    webApplicationBuilder.Services.AddScoped<IAuthenticationService, AuthenticationService>();


    

}

void AuthenticationConfigs(WebApplicationBuilder webAppbuilder)
{
    webAppbuilder.Services.Configure<ActiveDirectoryOptions>(
        webAppbuilder.Configuration.GetSection(ActiveDirectoryOptions.SectionName));

    webAppbuilder.Services.AddScoped<IExternalAuthenticationProvider,
        ActiveDirectoryAuthenticationProvider>();

    webAppbuilder.Services.AddScoped<IUserProvisioningService, UserProvisioningService>();
    webAppbuilder.Services.Configure<JwtOptions>(
        webAppbuilder.Configuration.GetSection(JwtOptions.SectionName));
    webAppbuilder.Services.AddScoped<IJwtTokenService, JwtTokenService>();


    webAppbuilder.Services
        .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,

                ValidIssuer = builder.Configuration["Jwt:Issuer"],
                ValidAudience = builder.Configuration["Jwt:Audience"],

                IssuerSigningKey =
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(
                            builder.Configuration["Jwt:SecretKey"]!))
            };
        });

    builder.Services.AddAuthorization();
}
