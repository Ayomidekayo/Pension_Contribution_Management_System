using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using PensionContributionMgmt.Application.Infrastructure;
using PensionContributionMgmt.Infrastructure.Data;
using PensionContributionMgmt.Infrastructure.Repository;
using PensionContributionMgmt.Infrastructure.Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

// Add DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("ConntectionToDataBase"));
});
// Add Hangfire Configuration
//builder.Services.AddHangfire(config =>
//    config.UseSqlServerStorage(builder.Configuration.GetConnectionString("DefaultConnection")));


// Register Unit of Work
builder.Services.AddScoped<IUnitOfwork, UnitOfwork>();

// Register Services
builder.Services.AddScoped<IMemberService, MemberService>();

// Register AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
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
