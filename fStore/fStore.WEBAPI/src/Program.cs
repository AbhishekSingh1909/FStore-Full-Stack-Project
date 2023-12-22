using System.Security.Claims;
using System.Text;
using fStore.Business;
using fStore.Core;
using fStore.WEBAPI;
using fStore.WEBAPI.src.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

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

builder.Services.AddScoped<IAddressRepo, AddressRepo>();
builder.Services.AddScoped<IAddressService, AddressService>();

builder.Services.AddScoped<ICategoryRepo, CategoryRepo>();
builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddScoped<IProductRepo, ProductRepo>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddScoped<IImageRepo, ImageRepo>();
builder.Services.AddScoped<IImageService, ImageService>();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITokenService, TokenService>();
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

// COnfig authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(o =>
    {
        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey
            (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true
        };
    });

builder.Services.AddAuthorization(policy =>
{
    policy.AddPolicy("Admin", policy => policy.RequireClaim(ClaimTypes.Role, "Admin"));
});


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

