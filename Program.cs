using System;
using Employee.Routes;
using Microsoft.EntityFrameworkCore;
using Person.Data;
using Person.Routes;
using Position.Routes;


var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Replace 'PersonDbContext' with the actual name of your DbContext class
builder.Services.AddDbContext<PersonContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    )
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Person API v1");
    });
}

app.PersonRoutes();
app.PositionsRoutes();
app.EmployeesRoutes();

app.UseHttpsRedirection();
app.Run();

