using Microsoft.EntityFrameworkCore;
using ParcoBackend.Model;
using ParcoBackend.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ParcoContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IVehicleService, VehicleService>();
builder.Services.AddScoped<IReportService, ReportService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();


// app.UseHttpsRedirection(); // opcional, se quiser manter HTTPS

app.UseAuthorization();
app.MapControllers();

app.Run();
