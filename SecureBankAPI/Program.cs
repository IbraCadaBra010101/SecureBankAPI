// <copyright file="Program.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

#pragma warning disable SA1200
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Web;
using Microsoft.OpenApi.Models;
using SecureBankAPI.Data;
using SecureBankAPI.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration);

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
});

builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<SecureBankDBContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<SecureBankDBContext>();
    dbContext.Database.Migrate();
}

app.Use(async (context, next) =>
{
    var logger = app.Services.GetRequiredService<ILogger<Program>>();
    logger.LogInformation("Incoming request: {method} {url}", context.Request.Method, context.Request.Path);

    await next();

    logger.LogInformation("Response status code: {statusCode}", context.Response.StatusCode);
});

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
