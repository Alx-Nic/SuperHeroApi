using HeroMSVC.Api;
using HeroMSVC.Models;
using HeroMSVC.Models.SuperHero;
using HeroMSVC.Repo.Abstract;
using HeroMSVC.Repo.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme()
{
    In = ParameterLocation.Header,
    Name = "Authorization",
    Type = SecuritySchemeType.ApiKey
});

options.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddDbContext<SuperHeroDataContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConfiguration"), migrationOptions => migrationOptions.MigrationsAssembly("HeroMSVC.Models"));
});

builder.Services.AddAuthorization();

builder.Services.AddIdentityApiEndpoints<IdentityUser>().AddEntityFrameworkStores<SuperHeroDataContext>();

builder.Services.Configure<IdentityOptions>(options =>
{
    options.User.RequireUniqueEmail = true;

    options.Password.RequiredLength = 5;
});

//Repo services
builder.Services.AddScoped<ISuperHeroRepository, SuperHeroRepository>();
builder.Services.AddScoped<FooService>();

builder.Services.AddSingleton<ISuperHeroFilterParams, SuperHeroFilterParamsV1>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapIdentityApi<IdentityUser>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
