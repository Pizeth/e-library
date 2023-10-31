using Microsoft.EntityFrameworkCore;
using e_library.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Options;
using Newtonsoft.Json;
using NuGet.DependencyResolver;
using Microsoft.AspNetCore.Authentication;
using e_library.Handlers;
using System.Configuration;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication("BasicAuthentication")
    .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);

var jwtSection = builder.Configuration.GetSection("JWTSettings");
builder.Services.Configure<JWTSetting>(jwtSection);

//////to validate the token which has been sent by clients
var appSettings = jwtSection.Get<JWTSetting>();
var key = Encoding.ASCII.GetBytes(appSettings.SecretKey);


// For Identity  
//builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
//                .AddEntityFrameworkStores<ApplicationDbContext>()
//                .AddDefaultTokenProviders();
// Adding Authentication  
var config = builder.Configuration;
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})

// Adding Jwt Bearer  
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    //ValidateIssuer = true,
                    //ValidateAudience = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    //ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    //ValidIssuer = config["JwtSettings:Issuer"],
                    //ValidAudience = config["JwtSettings:Audience"],
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWTSettings:SecretKey"]))
                };
            });

builder.Services.AddDbContext<ElearningDbContext>(options => options.UseSqlServer(
builder.Configuration.GetConnectionString("DefaultConnection")
));

//builder.Services.AddMvc(option => option.EnableEndpointRouting = false)
//                .SetCompatibilityVersion(CompatibilityVersion.Latest)
//                .AddNewtonsoftJson(opt => opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore);

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
