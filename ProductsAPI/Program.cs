using InfrastructureLayer.Data;
using InfrastructureLayer.Repositories;
using ApplicationLayer.Interfaces.Repositories;
using ApplicationLayer.Interfaces.Services;
using ApplicationLayer.Services;
using Microsoft.EntityFrameworkCore;

// 1. Initialisation indispensable de builder
var builder = WebApplication.CreateBuilder(args);

// 2. Ajout des services au conteneur DI
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<IProductService, ProductService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 3. Configuration du pipeline HTTP
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ProductsAPI v1");
    c.RoutePrefix = "swagger"; // Configure l'interface explicitement sur /swagger
});

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();