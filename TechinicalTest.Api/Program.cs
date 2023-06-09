using Microsoft.EntityFrameworkCore;
using TechinicalTest.Api.MiddleWare;
using TechnicalTest.Core.Interfaces;
using TechnicalTest.Infrastructure.Data;
using TechnicalTest.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);


const string ConnectionName = "EdwardTest";
var connectionString = builder.Configuration.GetConnectionString(ConnectionName);

builder.Services.AddSqlite<EdwardTestContext>("Data Source=../Sqlite.Db");


// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddTransient<IPersonServices, PersonServices>();
builder.Services.AddTransient<GlobalExceptionHandlerMiddleware>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scoped = app.Services.CreateScope())
{
    var dbcontext = scoped.ServiceProvider.GetRequiredService<EdwardTestContext>();
    dbcontext.Database.Migrate();
}

// Configure the HTTP request pipeline.

    app.UseSwagger();
    app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

app.MapControllers();

app.Run();
