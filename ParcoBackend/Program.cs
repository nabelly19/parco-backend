using Microsoft.EntityFrameworkCore;
using ParcoBackend.Model;
using ParcoBackend.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ParcoContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IVehicleService, VehicleService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Middleware do Swagger (ambiente de desenvolvimento)
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection(); // opcional, se quiser manter HTTPS

app.UseAuthorization();
app.MapControllers();

app.Run();
