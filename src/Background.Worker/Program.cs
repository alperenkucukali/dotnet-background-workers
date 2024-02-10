using Background.Worker.Core.Options;
using Background.Worker.Core.Services;
using Background.Worker.Core.Services.Interfaces;
using Background.Worker.Services;
using Background.Worker.Services.Interfaces;
using Background.Worker.Workers;
using Background.Worker.Workers.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

//Configurations
builder.Services.Configure<WorkerOptions>(builder.Configuration.GetSection(nameof(WorkerOptions)));
builder.Services.Configure<AwsOptions>(builder.Configuration.GetSection(nameof(AwsOptions)));
builder.Services.Configure<SlackOptions>(builder.Configuration.GetSection(nameof(SlackOptions)));

builder.Services.AddHttpClient();

//Core Services
builder.Services.AddScoped<IImageService, S3ImageService>();
builder.Services.AddScoped<ISlackService, SlackService>();

//Services
builder.Services.AddScoped<IReminderService, ReminderService>();
builder.Services.AddScoped<IScraperService, ScraperService>();

//Workers
builder.Services.AddHostedService<ScopedWorker>();
builder.Services.AddScoped<IReminderWorker, ReminderWorker>();
builder.Services.AddScoped<IScraperWorker, ScraperWorker>();


var app = builder.Build();

app.Run();