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
using FantaAstaServer.Models.Configurations;
using FantaAstaServer.Common.Constants;

var builder = WebApplication.CreateBuilder(args);


var postgreSqlConfig = builder.Configuration.GetSection(Constants.PostgresqlConfigKey).Get<PostgreSqlConfig>();
var npgSqlConnectionString = $"User Id={postgreSqlConfig.Id};Password={postgreSqlConfig.Password};Server={postgreSqlConfig.Server};Port={postgreSqlConfig.Port};Database={postgreSqlConfig.Database}";

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<FantaAstaDbContext>(x => x.UseNpgsql(connectionString: npgSqlConnectionString));

builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IAuctionRepository, AuctionRepository>();
builder.Services.AddTransient<IBatchRepository, BatchRepository>();
builder.Services.AddTransient<IFootballerRepository, FootballerRepository>();
builder.Services.AddTransient<IOfferRepository, OfferRepository>();
builder.Services.AddTransient<IUserActionRepository, UserActionRepository>();
builder.Services.AddTransient<IDbUnitOfWork, DbUnitOfWork>();

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
