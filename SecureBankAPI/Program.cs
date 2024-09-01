// <copyright file="Program.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

#pragma warning disable SA1200
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;
using SecureBankAPI.Data;
using SecureBankAPI.Models;
using SecureBankAPI.Repository.Clients;
using SecureBankAPI.Repository.Investments;
using SecureBankAPI.Services.Clients;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection(Authentication.AzureADSection));

builder.Services.AddAuthorizationBuilder()
    .AddPolicy(Authentication.ClientsReadAllPolicy, policy =>
        policy.RequireRole(Authentication.ClientsReadAllRole))
    .AddPolicy(Authentication.ClientsManagePolicy, policy =>
        policy.RequireRole(Authentication.ClientsManageRole))
    .AddPolicy(Authentication.InvestmentsManagePolicy, policy =>
        policy.RequireRole(Authentication.InvestmentsManageRole))
    .AddPolicy(Authentication.InvestmentsReadPolicy, policy =>
        policy.RequireRole(Authentication.InvestmentsReadRole));

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "SecureBankAPI", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your token in the text input below. Example: 'Bearer your_token_here'.",
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer",
                },
            },
            new string[] { }
        },
    });
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<SecureBankDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IClientsRepository, ClientsRepository>();
builder.Services.AddScoped<IInvestmentsRepository, InvestmentsRepository>();

builder.Services.AddScoped<IClientService, ClientService>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<SecureBankDBContext>();
    dbContext.Database.Migrate();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "SecureBankAPI v1");
    });
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
