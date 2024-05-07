using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SuperHeroApi;
using SuperHeroApi.Data;
using SuperHeroApi.Repo.Abstract;
using SuperHeroApi.Repo.Concrete;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<SuperHeroDataContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConfiguration"));
});

//Repo services
builder.Services.AddScoped<ISuperHeroRepository, SuperHeroRepository>();

builder.Services.AddTransient<FooService>();

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
