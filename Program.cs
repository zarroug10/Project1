using System.Reflection;

using Microsoft.EntityFrameworkCore;

using web.Data;
using web.Data.Repository;
using web.Extensions;
using web.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddLogging();

builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddApplicationServices();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<CustomDbcontext>(opt =>{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Sale"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI();

app.Run();
