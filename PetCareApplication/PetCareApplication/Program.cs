using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using PetCareApplication.MapperProfiles;
using PetCareApplication.Middleware;
using PetCareApplication.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Configure the database context with SQL Server connection
builder.Services.AddDbContext<PetCareDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers()
    .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<Program>());

builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));

builder.Services.AddScoped<PetCareRepository>();
builder.Services.AddScoped<ActivityRepository>();
builder.Services.AddScoped<FoodRepository>();
builder.Services.AddScoped<HealthConditionRepository>();
builder.Services.AddScoped<TrainingReppository>();
builder.Services.AddScoped<UserRepository>();

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

app.UseCustomMiddleware();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});
