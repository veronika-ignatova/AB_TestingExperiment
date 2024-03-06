using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Services;
using DataBase;
using DataBase.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
        options.JsonSerializerOptions.DictionaryKeyPolicy = null;
    });

//Conecting to database
builder.Services.AddDbContext<BackendTZContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BackendTZContext") ?? throw new InvalidOperationException("Connection string 'PagesMovieContext' not found.")));

builder.Services.AddTransient<IExperimentRepository, ExperimentRepository>();
builder.Services.AddTransient<IExperimentService, ExperimentService>();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
