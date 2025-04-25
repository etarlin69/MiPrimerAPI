using Microsoft.EntityFrameworkCore;
using MiPrimerAPI.Data;
using MiPrimerAPI.Repositories;
using MiPrimerAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Agregar controladores
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuración base de datos
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Inyección de dependencias
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ProductService>();

// HttpClient + configuracion para ExternalApiService
builder.Services.AddHttpClient<ExternalApiService>();
builder.Services.AddScoped<ExternalApiService>();

var app = builder.Build();

// Desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
