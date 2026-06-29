using ApiStudio.Application.Common.Interfaces;
using ApiStudio.Application.Workspaces.Commands;
using ApiStudio.HttpEngine;
using ApiStudio.Persistence;
using Microsoft.EntityFrameworkCore;
using FluentValidation;
using ApiStudio.Application.Authentication.Interfaces;
using ApiStudio.Infrastructure.ActiveDirectory;
using ApiStudio.Infrastructure.Authentication;
using Microsoft.Extensions.Configuration;
using ApiStudio.Infrastructure;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services
    .AddIdentity<ApplicationUser, ApplicationRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.Configure<ActiveDirectoryOptions>(
    builder.Configuration.GetSection(ActiveDirectoryOptions.SectionName));

builder.Services.AddScoped<IExternalAuthenticationProvider,
    ActiveDirectoryAuthenticationProvider>();

builder.Services.AddScoped<IApplicationDbContext>(sp =>
    sp.GetRequiredService<ApplicationDbContext>());


builder.Services.AddScoped<IUserProvisioningService, UserProvisioningService>();

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(CreateWorkspaceCommand).Assembly);
});


builder.Services.AddValidatorsFromAssembly(typeof(CreateWorkspaceValidator).Assembly);
builder.Services.AddControllers();

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(CreateWorkspaceCommand).Assembly);
});

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
app.Run();
