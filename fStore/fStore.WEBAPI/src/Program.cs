using System.Security.Claims;
using System.Text;
using fStore.Business;
using fStore.Core;
using fStore.WEBAPI;
using fStore.WEBAPI.src.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Npgsql;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // Define the OAuth2.0 scheme that's in use (i.e. Implicit Flow)
    c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Description = "Bearer token authentication",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Scheme = "Bearer"
    });
    c.OperationFilter<SecurityRequirementsOperationFilter>();
});


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

builder.Services.AddScoped<IOrderRepo, OrderRepo>();
builder.Services.AddScoped<IOrderService, OrderService>();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<IAuthorizationHandler, AdminOrOwnerHandler>();



// COnfig authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
    options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true
        };
    }
);

builder.Services.AddAuthorization(policy =>
{
    policy.AddPolicy("AdminOrOwner", policy => policy.Requirements.Add(new AdminOrOwnerRequirement()));
    policy.AddPolicy("SuperAdmin", policy => policy.RequireClaim(ClaimTypes.Email, "admin@mail.com"));
    policy.AddPolicy("Admin", policy => policy.RequireRole(ClaimTypes.Role, "Admin"));
    policy.AddPolicy("Customer", policy => policy.RequireRole(ClaimTypes.Role, "Customer"));
});

builder.Services.AddAutoMapper(typeof(MapperProfile).Assembly);

// Add Error handler Middleware
builder.Services.AddTransient<ExceptionHandlerMiddleware>();

var dataSourceBuilder = new NpgsqlDataSourceBuilder(builder.Configuration.GetConnectionString("MyStoreDb"));
dataSourceBuilder.MapEnum<Role>();
dataSourceBuilder.MapEnum<OrderStatus>();
var dataSource = dataSourceBuilder.Build();

// Add database contect service
builder.Services.AddDbContext<DataBaseContext>(options =>
{
    options.UseNpgsql(dataSource)
           .UseSnakeCaseNamingConvention()
           .AddInterceptors(new TimeStampInterceptor());
});


var app = builder.Build();

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseSwagger();
app.UseSwaggerUI(/* opt =>
{
    opt.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    opt.RoutePrefix = string.Empty;
} */);

app.UseHttpsRedirection();

// Use CORS with the defined policy
app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

