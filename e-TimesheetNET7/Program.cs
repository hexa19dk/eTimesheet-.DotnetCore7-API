using e_TimesheetNET7.Config.Interfaces;
using e_TimesheetNET7.Config;
using e_TimesheetNET7.Repositories.Interfaces;
using e_TimesheetNET7.Repositories;
using e_TimesheetNET7.Usecase.Interfaces;
using e_TimesheetNET7.Usecase;
using e_TimesheetNET7.Repositories.SQL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IConnectionFactoryDb>(
    _ => new ConnectionFactoryDb(builder.Configuration.GetValue<string>("DatabaseSettings:ODBCConnection")));

// Data
builder.Services.AddScoped<IContractDb, ContractDb>();
builder.Services.AddScoped<IContractDb, ContractDb>();
builder.Services.AddScoped<ITimesheetDb, TimesheetDb>();

// Repository
builder.Services.AddScoped<IContractRepository, ContractRepository>();
//builder.Services.AddScoped<IDriverRepository, DriverRepository>();
builder.Services.AddScoped<ITimesheetRepository, TimesheetRepository>();

// Usecase
builder.Services.AddScoped<IContractUsecase, ContractUsecase>();
builder.Services.AddScoped<ITimesheetUsecase, TimesheetUsecase>();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseSwaggerUI(c => c.SwaggerEndpoint("http://eTimesheet/swagger/v1/swagger.json", "api v1"));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
