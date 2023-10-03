// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using FantaAstaServer.Services;
using Microsoft.EntityFrameworkCore;
using FantaAstaServer.Services.Repositories;
using FantaAstaServer.Interfaces.Repositories;
using FantaAstaServer.Interfaces;
using Microsoft.Extensions.Configuration;
using FantaAstaServer.Models.Options;
using FantaAstaServer.Common.Constants;
using FantaAstaServer.Interfaces.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using System;
using Microsoft.AspNetCore.Http;
using System.Text.Json.Serialization;
using System.Text.Json;
using FantaAstaServer.JsonConverters;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Routing;
using FantaAstaServer.Utils;
using FantaAstaServer.Hubs;
using Npgsql;
using FantaAstaServer.Enums;
using FantaAstaServer.Interfaces.Services.Mappers;
using FantaAstaServer.Services.Mappers;

var allSupportedCulturesInfo = new CultureInfo[] { new("en-US"), new("it-IT") };
var supportedCultures = allSupportedCulturesInfo.Select(x => x.Name).ToArray();

LocalizedResourceUtils.VerifyKeys(allSupportedCulturesInfo);

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.HttpOnly = true;
        options.Cookie.SameSite = SameSiteMode.None; // will restrict this

        options.SlidingExpiration = true;
        options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
    });

// Add options services
builder.Services.AddOptions<SmtpOptions>().BindConfiguration(Constants.SmtpConfigKey);
builder.Services.AddOptions<PostgreSqlOptions>().BindConfiguration(Constants.PostgresqlConfigKey);
builder.Services.AddOptions<PasswordHasherOptions>().BindConfiguration(Constants.PasswordHasherConfigKey);

builder.Services.AddLocalization();
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.SetDefaultCulture(supportedCultures.First())
        .AddSupportedCultures(supportedCultures)
        .AddSupportedUICultures(supportedCultures);

    options.RequestCultureProviders = new[] { new AcceptLanguageHeaderRequestCultureProvider() };
});
// Add services to the container.


builder.Services.AddControllers()
    .AddJsonOptions(x => {
        x.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<FantaAstaDbContext>();

builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IAuctionRepository, AuctionRepository>();
builder.Services.AddTransient<IBatchRepository, BatchRepository>();
builder.Services.AddTransient<IFootballerRepository, FootballerRepository>();
builder.Services.AddTransient<IOfferRepository, OfferRepository>();
builder.Services.AddTransient<IUserAuctionRepository, UserAuctionRepository>();
builder.Services.AddTransient<IDbUnitOfWork, DbUnitOfWork>();
builder.Services.AddTransient<IAuctionMapper, AuctionMapper>();
builder.Services.AddSingleton<IEmailSender, EmailSender>();

builder.Services.AddSingleton<IPasswordHasher, PasswordHasher>();
builder.Services.AddTransient<ILocalizer, Localizer>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseExceptionHandler("/error");
app.UseDeveloperExceptionPage();
app.UseAuthentication();
app.UseAuthorization();

var options = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(options.Value);

app.MapControllers();
app.MapHub<AuctionHub>("/auctionHub");

app.Run();

public partial class Program { }