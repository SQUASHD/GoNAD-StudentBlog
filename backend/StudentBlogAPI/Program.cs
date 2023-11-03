using System.Text;
using System.Text.Json.Serialization;
using System.Threading.RateLimiting;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using StudentBlogAPI.Data;
using StudentBlogAPI.Mappers;
using StudentBlogAPI.Mappers.Interfaces;
using StudentBlogAPI.Middleware;
using StudentBlogAPI.Repository;
using StudentBlogAPI.Repository.Implementations;
using StudentBlogAPI.Repository.Interfaces;
using StudentBlogAPI.Services;
using StudentBlogAPI.Services.Implementations;
using StudentBlogAPI.Services.Interfaces;
using StudentBlogAPI.Validators;

var port = Environment.GetEnvironmentVariable("PORT") ?? "8080";
var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls($"http://*:{port}");
// Add services to the container.

// This is to allow the examiners to run the application without having to set the environment variable
var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL") ??
                  builder.Configuration.GetConnectionString("DefaultConnection");

// Entity Framework
builder.Services.AddDbContext<StudentBlogDbContext>(options =>
    options.UseMySql(databaseUrl,
        new MySqlServerVersion(new Version(8, 0))));

builder.Services.AddControllers(options =>
    {
        // Adding the ValidationErrorHandlingFilter
        options.Filters.Add(new ValidationErrorHandlingFilter());

        // Building and adding the global authorization filter
        var authorizationPolicy = new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .Build();
        options.Filters.Add(new AuthorizeFilter(authorizationPolicy));
    })
    .AddJsonOptions(options =>
    {
        // Convert enums to strings in JSON
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

// Make the API return validation errors in the response body
builder.Services.Configure<ApiBehaviorOptions>(options => { options.SuppressModelStateInvalidFilter = true; });

// Authentication
// JWT middleware
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidateAudience = false,
            ValidIssuer = builder.Configuration["Jwt:AccessIssuer"],
            IssuerSigningKey =
                new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Environment.GetEnvironmentVariable("JWT_KEY") ??
                                                                throw new InvalidOperationException()))
        };
    });

// Rate limiting middleware
builder.Services.AddRateLimiter(l =>
    l.AddFixedWindowLimiter("fixed", options =>
    {
        options.PermitLimit = 100;
        options.Window = TimeSpan.FromSeconds(12);
        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        options.QueueLimit = 20;
    }));


// Authorization
// Enable admin only authorization
builder.Services.AddAuthorizationBuilder()
    .AddPolicy("AdminOnly", policy => { policy.Requirements.Add(new AdminRoleRequirement()); });

builder.Services.AddScoped<IAuthorizationHandler, AdminRoleHandler>();

// Logging
builder.Host.UseSerilog((context, configuration) => { configuration.ReadFrom.Configuration(context.Configuration); });

// Swagger
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
// TODO: Figure out how to enable headers in Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependency injection
// Let's the application know which implementation to use when a service is requested
// User
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserMapper, UserMapper>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Post
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<IPostMapper, PostMapper>();
builder.Services.AddScoped<IPostRepository, PostRepository>();

// Comment
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<ICommentMapper, CommentMapper>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();

// Auth
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IAuthMapper, AuthMapper>();

// Jwt
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<ITokenValidator, TokenValidator>();
builder.Services.AddScoped<IRevokedTokenRepository, RevokedTokenRepository>();
builder.Services.AddScoped<ITokenMapper, TokenMapper>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();


app.UseCors();
// app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();