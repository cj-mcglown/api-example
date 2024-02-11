using Microsoft.EntityFrameworkCore;
using System.Configuration;

using WebApplication1.Modelss;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


//builder.Services.AddScoped<IDependencyOne, DependencyOne
//
builder.Services.AddDbContext<BuiltContext>(options =>
         options.UseSqlServer("Server=localhost;Database=Built;Trusted_Connection=True;TrustServerCertificate=True"),
         ServiceLifetime.Transient);

builder.Services.AddMvc(options =>
{
    options.SuppressAsyncSuffixInActionNames = false;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
