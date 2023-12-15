using fStore.Business;
using fStore.Core;
using fStore.WEBAPI;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// declare services
builder.Services.AddScoped<IUserService, UserService>(); // tell the program to create insteace of class UserService
builder.Services.AddScoped<IUserRepo, UserRepo>();
// builder.Services.AddTransient()
// builder.Services.AddSingleton();

//add automapper dependency injection
// builder.Services.AddAutoMapper(typeof(Program).Assembly);
//builder.Services.AddAutoMapper(typeof(UserService).Assembly);
builder.Services.AddAutoMapper(typeof(MapperProfile).Assembly);

// Add Error handler Middleware
builder.Services.AddTransient<ExceptionHandlerMiddleware>();

// Add database contect service
builder.Services.AddDbContext<DataBaseContext>(options => options.UseNpgsql());

var app = builder.Build();

app.UseMiddleware<ExceptionHandlerMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();

