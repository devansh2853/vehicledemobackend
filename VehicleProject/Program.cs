using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using VehicleProject.Data;
using VehicleProject.Services;
using VehicleProject.Services.Interfaces;

var  MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddControllers();

builder.Services.AddScoped<ICarService, CarService>();
builder.Services.AddDbContext<VehicleProjectContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));


builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy  =>
                      {
                          policy.WithOrigins("http://localhost:5173", "https://witty-bay-0ed78dd00.4.azurestaticapps.net")
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                      });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}


app.UseCors(MyAllowSpecificOrigins);

app.UseHttpsRedirection();




app.MapControllers();

app.Run();
