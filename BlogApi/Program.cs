using BlogApi.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// appsettings.json 讀取連線字串
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// 註冊 DbContext 服務，並告訴它我們要使用 Npgsql (PostgreSQL)
builder.Services.AddDbContext<BlogDbContext>(options =>
    options.UseNpgsql(connectionString)
);
builder.Services.AddControllers();
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
