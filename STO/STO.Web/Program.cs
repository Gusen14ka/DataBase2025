using Microsoft.OpenApi.Models;
using STO.Services.Extensions;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Подключение к SQLite
builder.Services.AddApplicationServices(builder.Configuration);

//// 🔹 Добавление AutoMapper
//builder.Services.AddAutoMapper(typeof(Program)); //я вынес это в serivces.extensions

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
