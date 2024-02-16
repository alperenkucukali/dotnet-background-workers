using Background.Worker.Core.Options;
using Background.Worker.Core.Services;
using Background.Worker.Core.Services.Interfaces;
using Background.Worker.Repositories;
using Background.Worker.Repositories.Interfaces;
using Background.Worker.Services;
using Background.Worker.Services.Interfaces;
using Background.Worker.Workers;
using Background.Worker.Workers.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.Configuration
    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", true, true);

//Configurations
builder.Services.Configure<WorkerOptions>(builder.Configuration.GetSection(nameof(WorkerOptions)));
builder.Services.Configure<AwsOptions>(builder.Configuration.GetSection(nameof(AwsOptions)));
builder.Services.Configure<SlackOptions>(builder.Configuration.GetSection(nameof(SlackOptions)));

builder.Services.AddHttpClient();
builder.Services.AddStackExchangeRedisCache(opt =>
{
    opt.Configuration = builder.Configuration["RedisSetting:ConnectionString"];
});

//Core Services
builder.Services.AddScoped<IImageService, S3ImageService>();
builder.Services.AddScoped<ISlackService, SlackService>();

//Repositories
builder.Services.AddScoped<IReminderRepository, ReminderRepository>();
builder.Services.AddScoped<IScraperRepository, ScraperRepository>();

//Services
builder.Services.AddScoped<IReminderService, ReminderService>();
builder.Services.AddScoped<IScraperService, ScraperService>();

//Workers
builder.Services.AddHostedService<ScopedWorker>();
builder.Services.AddScoped<IScopedProcessingService, ReminderWorker>();
builder.Services.AddScoped<IScopedProcessingService, ScraperWorker>();


var app = builder.Build();

app.Run();