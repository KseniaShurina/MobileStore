using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MobileStore.Core.Configurations;
using MobileStore.Presentation.Api.Configurations;
using MobileStore.Presentation.Api.Helpers;
using MobileStore.Presentation.Api.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services registration.
builder.Services.AddCoreDependencies(builder.Configuration);

builder.Services.AddAutoMapperProfiles(o =>
{
    o.AddProfile<PresentationMapperProfile>();
});

builder.Services.AddControllers();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        // JWT configuration object
        var jwtConfig = JwtConfiguration.Create(builder.Configuration);
        // JWT token validation options
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(jwtConfig.SecretKey)),
            ValidIssuer = jwtConfig.Issuer,
            ValidateIssuer = true, // Set to true if you want to validate the issuer
            ValidateAudience = true,
            ValidAudience = jwtConfig.Audience,
            ValidateLifetime = true,
        };
    });

builder.Services.AddAuthorization();


builder.Services.AddCors(options =>
{
    options.AddPolicy("allow-all",
        configurePolicy => configurePolicy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});

// To add services needed to work with API documentation tools
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });

    // Add JWT authentication to Swagger UI
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter your JWT token",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var app = builder.Build();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

app.MapControllers();

app.UseCors("allow-all");

app.UseForwardedHeaders();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();
