using congestion.calculator.Queries.Services;
using congestion.calculator.Services;
using DBContext;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<TollCalculationDBContext>(op => {
    op.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddTransient<IRepository,Repository>();
builder.Services.AddScoped<IGetSpecialTimesTollFee,GetSpecialTimesTollFee>();
builder.Services.AddScoped<IGetyearDayType,GetyearDayType>(); 
builder.Services.AddScoped<IAddNewSpecialTimeTollFeeService,AddNewSpecialTimeTollFeeService>();
builder.Services.AddScoped<IVehicle, Car>();
builder.Services.AddScoped<IVehicle, Motorbike>();

var app = builder.Build();
 
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthorization();

app.MapControllers();

app.Run();