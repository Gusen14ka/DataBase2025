using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using STOAPI.Data;
using AutoMapper;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Подключение к SQL Server
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// 🔹 Добавление AutoMapper
builder.Services.AddAutoMapper(typeof(Program));

// 🔹 Добавление контроллеров (CRUD API)
builder.Services.AddControllers();

// 🔹 Включение Swagger (документация API)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "STO API", Version = "v1" });
});

var app = builder.Build();

// 🔹 Подключаем Swagger UI в режиме разработки
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "STO API v1");
    });
}

// 🔹 Включаем HTTPS
app.UseHttpsRedirection();

// 🔹 Включаем маршрутизацию контроллеров
app.UseAuthorization();
app.MapControllers();

app.Run();
