using CryptoWatcher.Core.Interface;
using CryptoWatcher.Core.Interface.Repositories;
using CryptoWatcher.Core.Interface.Services;
using CryptoWatcher.Core.Servicies;
using CryptoWatcher.Infra.Context;
using CryptoWatcher.Infra.Repositories;
using CryptoWatcher.Jobs;
using Hangfire;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<CryptoWatcherContext>(options => options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), b => b.MigrationsAssembly("CryptoWatcher.WorkerService")));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHangfire(configuration => 
    configuration.UseSimpleAssemblyNameTypeSerializer()
    .UseRecommendedSerializerSettings()
    .UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection"))
    );
builder.Services.AddHangfireServer();

builder.Services.AddHttpClient();

builder.Services.AddScoped<IProfileRepository, ProfileRepository>();
builder.Services.AddScoped<ICryptoInfoRepository, CryptoInfoRepository>();
builder.Services.AddScoped<IConfigurationService, ConfigurationService>();
builder.Services.AddScoped<IExternalAPIService, ExtrenalAPIService>();
builder.Services.AddScoped<IProfileJobService, ProfileJobService>();
builder.Services.AddScoped<ICryptoInfoJobService, CryptoInfoJobService>();
builder.Services.AddScoped<IBackgroundJobClient, BackgroundJobClient>();

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

app.UseHangfireDashboard();

RecurringJob.AddOrUpdate<CryptoInfoJobService>(
    job => job.UpdateCryptoCurrencyHourly(), Cron.Hourly
    );

app.Run();
