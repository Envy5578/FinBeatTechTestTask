using FinBeatTechTestTask.DAL;
using FinBeatTechTestTask.DAL.Interfaces;
using FinBeatTechTestTask.DAL.Repositories;
using FinBeatTechTestTask.Domain.Entity;
using FinBeatTechTestTask.Service.Implementation;
using FinBeatTechTestTask.Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(config =>
{
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    config.IncludeXmlComments(xmlPath);
});

builder.Services.AddScoped<IBaseRepository<PairValueEntity>, PairValueRepository>();
builder.Services.AddScoped<IPairValueService, PairValueService>();

var connetionString = builder.Configuration.GetConnectionString("PgSql");

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql(connetionString);
});

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
