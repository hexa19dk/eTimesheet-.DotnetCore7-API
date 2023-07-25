using e_TimesheetNET7.Config;
using e_TimesheetNET7.Repositories.Interfaces;
using e_TimesheetNET7.Repositories;
using e_TimesheetNET7.Usecase;
using e_TimesheetNET7.Repositories.SQL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IConnectionFactoryDb>(
    _ => new ConnectionFactoryDb(
        builder.Configuration.GetValue<string>("DatabaseSettings:ODBCConnection")!, 
        builder.Configuration.GetValue<string>("DatabaseSettings:bbdbserver03")!
));


// Contract Service
builder.Services.AddScoped<IContractDb, ContractDb>();
builder.Services.AddScoped<IContractRepository, ContractRepository>();
builder.Services.AddScoped<IContractUsecase, ContractUsecase>();

// Timesheet Service
builder.Services.AddScoped<ITimesheetDb, TimesheetDb>();
builder.Services.AddScoped<ITimesheetRepository, TimesheetRepository>();
builder.Services.AddScoped<ITimesheetUsecase, TimesheetUsecase>();

// Driver Services 
builder.Services.AddScoped<IDriverDb, DriverDb>();
builder.Services.AddScoped<IDriverRepository, DriverRepository>();
builder.Services.AddScoped<IDriverUsecase, DriverUsecase>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();
app.UseSwagger();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerUI();
}
else
{
    //app.UseSwaggerUI(c => c.SwaggerEndpoint("http://eTimesheet/swagger/v1/swagger.json", "api v1"));    
    app.UseSwaggerUI(c => c.SwaggerEndpoint($"{builder.Configuration.GetValue<string>("SwaggerUrl")}/swagger/v1/swagger.json", "api v1"));
}


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
