using Microsoft.OpenApi.Models;
using STO.Service.Extensions;

var builder = WebApplication.CreateBuilder(args);


// 🔹 Подключение к SQLite
builder.Services.AddApplicationServices(builder.Configuration);

builder.Services.AddDistributedMemoryCache(); // или другой кеш
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddRazorPages();
builder.Services.AddSession();

// 🔹 Добавление контроллеров (CRUD API)
builder.Services.AddControllers();

// 🔹 Включение Swagger (документация API)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "STO API", Version = "v1" });
});

// Правка от гпт (Разрешаем любые запросы (если API будут использоваться извне))
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());
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
app.UseStaticFiles();
app.UseHttpsRedirection();
app.UseCors("AllowAll");

// 🔹 Включаем маршрутизацию контроллеров
app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.MapRazorPages();
app.UseSession();

app.Run();
