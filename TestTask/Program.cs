using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TestTask;
using TestTask.Controllers;
using TestTask.DataModel;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//IConfiguration Configuration;
var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json");
var AppConfiguration = configuration.Build();
var a = AppConfiguration.GetSection("ConnectionStrings:DefaultConnection").Value;
builder.Services.AddDbContext<TestDbContext>(options => options.UseSqlServer(a));
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