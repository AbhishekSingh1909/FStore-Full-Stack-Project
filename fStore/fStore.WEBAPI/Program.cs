using fStore.Business;
using fStore.Core;
using fStore.WEBAPI;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true); // make urls in lower case

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();// used to add all controllers in WEBAPI project
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// declare services
builder.Services.AddScoped<IUserService , UserService>();// tell the program to create insteace of class UserService
builder.Services.AddScoped<IUserRepo, UserRepo>();

var app = builder.Build();

// add automapper dependency Injection
//builder.Services.AddAutoMapper(typeof(UserService).Assembly);
builder.Services.AddAutoMapper(typeof(MapperProfile).Assembly);

//add database connect service
builder.Services.AddDbContext<DataBaseContext>(options => options.UseNpgsql());

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();/// used to map all controllers in WEBAPI project
app.Run();

