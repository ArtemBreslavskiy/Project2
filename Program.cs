using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Project2.Data;
using Project2.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseInMemoryDatabase("ProductsDb");
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    dbContext.Products.AddRange(
        new Product { Name = "Logitech MX Master 3S", Category = "Периферия", Price = 8990m, InStock = true },
        new Product { Name = "Keychron K2 Pro", Category = "Периферия", Price = 11490m, InStock = true },
        new Product { Name = "Dell UltraSharp U2723QE 27\"", Category = "Мониторы", Price = 54900m, InStock = false },
        new Product { Name = "LG 27GP850-B 27\" 165Hz", Category = "Мониторы", Price = 39990m, InStock = true },
        new Product { Name = "Apple MacBook Air 13 M3 16/512", Category = "Ноутбуки", Price = 139900m, InStock = true },
        new Product { Name = "Lenovo ThinkPad X1 Carbon Gen 12", Category = "Ноутбуки", Price = 189990m, InStock = false },
        new Product { Name = "Samsung 990 PRO 2TB NVMe", Category = "Накопители", Price = 18490m, InStock = true },
        new Product { Name = "WD My Passport 4TB", Category = "Накопители", Price = 9990m, InStock = true },
        new Product { Name = "Sony WH-1000XM5", Category = "Аудио", Price = 32990m, InStock = true },
        new Product { Name = "Audio-Technica ATH-M50x", Category = "Аудио", Price = 14990m, InStock = false },
        new Product { Name = "Anker PowerCore 20000 PD", Category = "Аксессуары", Price = 4590m, InStock = true },
        new Product { Name = "USB-C хаб Baseus 8-в-1", Category = "Аксессуары", Price = 3290m, InStock = true }
    );

    dbContext.SaveChanges();
}

app.Run();
