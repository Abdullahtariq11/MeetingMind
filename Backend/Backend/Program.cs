using Backend.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Connectionstring configuration

builder.Services.AddDbContext<MeetingMindDbContext>(Options=>
    Options.UseNpgsql(builder.Configuration.GetConnectionString("DbConnectionString")));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Connetion setup to the database


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
