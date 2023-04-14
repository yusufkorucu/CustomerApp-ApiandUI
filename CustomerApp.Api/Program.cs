using CustomerApp.Api.Data;
using CustomerApp.Api.Handlers.Command;
using CustomerApp.Api.Handlers.Queries;
using CustomerApp.Api.Repositories.Customer;
using CustomerApp.Api.Security.Encyption;
using CustomerApp.Api.Security.Jwt;
using CustomerApp.Api.Services.Abstract;
using CustomerApp.Api.Services.Concrete;
using CustomerApp.Api.Validators;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var tokenOptions = builder.Configuration.GetSection("TokenOptions").Get<TokenOptions>();
// Add services to the container.
builder.Services.AddMediatR(config => config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddFluentValidation(x => x.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddScoped<IValidator<CustomerAddCommand>, CustomerAddCommandValidator>();
builder.Services.AddScoped<IValidator<GetCustomerByIdQuery>, GetCustomerByIdQueryValidator>();
builder.Services.AddScoped<IValidator<CustomerDeleteCommand>, CustomerDeleteCommandValidator>();
builder.Services.AddScoped<IValidator<GetCustomerByTcknoQuery>, GetCustomerByTcknoQueryValidator>();
builder.Services.AddScoped<IValidator<GetFilteredCustomerQuery>, GetFilteredCustomerQueryValidator>();
builder.Services.AddScoped<IValidator<LoginCommand>, LoginCommandValidator>();
builder.Services.AddScoped<ITokenHelper, JwtHelper>();
builder.Services.AddDbContext<CustomerAppDbContext>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddControllers();
builder.Services.AddMemoryCache();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ICustomerService, CustomerService>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opts =>
{
    opts.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidIssuer = tokenOptions.Issuer,
        ValidAudience = tokenOptions.Audience,
        IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey),

        ValidateIssuerSigningKey = true,
        ValidateAudience = true,
        ValidateIssuer = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

#region Swagger
builder.Services.AddSwaggerGen(options =>
{
    if (builder.Environment.IsDevelopment())
    {
        options.SwaggerDoc("v1", new OpenApiInfo { Title = "Customer App API", Version = "v1" });
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please enter a valid token",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "Bearer"
        });
        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type=ReferenceType.SecurityScheme,
                        Id="Bearer"
                    }
                },
                new string[]{}
            }
        });
    }
});
#endregion

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});


var app = builder.Build();

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

