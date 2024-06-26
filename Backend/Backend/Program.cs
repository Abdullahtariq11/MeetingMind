using System.Text;
using Backend.Data;
using Backend.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Connectionstring configuration

builder.Services.AddDbContext<MeetingMindDbContext>(Options=>
    Options.UseNpgsql(builder.Configuration.GetConnectionString("DbConnectionString")));

//Identity setup
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
.AddEntityFrameworkStores<MeetingMindDbContext>()
.AddDefaultTokenProviders();


builder.Services.Configure<IdentityOptions>(options=>
{
    //Password Setting
    options.Password.RequireDigit= true;
    options.Password.RequireLowercase=true;
    options.Password.RequireNonAlphanumeric=true;
    options.Password.RequireUppercase=true;
    options.Password.RequiredLength=6;
    options.Password.RequiredUniqueChars=1;

    //Lockout Setting
    options.Lockout.DefaultLockoutTimeSpan= TimeSpan.FromMinutes(5);
    options.Lockout.MaxFailedAccessAttempts=5;
    options.Lockout.AllowedForNewUsers=true;

    //User Setting
    options.User.AllowedUserNameCharacters="abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@";
    options.User.RequireUniqueEmail=true;
});
/* 
Cookies Implementation

builder.Services.ConfigureApplicationCookie(options=>
{
    //Cookie Settings
    options.Cookie.HttpOnly=true;
    options.ExpireTimeSpan=TimeSpan.FromMinutes(30);
    options.LoginPath="/Account/Login";
    options.AccessDeniedPath="/Account/AccessDenied";
    options.SlidingExpiration=true;
});
*/

// JWT Implemetation

var jwtSettings=builder.Configuration.GetSection("JwtSettings");

builder.Services.AddAuthentication(options=>{
    options.DefaultAuthenticateScheme=JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme=JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options=>{
#pragma warning disable CS8604 // Possible null reference argument.
    options.TokenValidationParameters= new TokenValidationParameters
    {
        ValidateIssuer= true,
        ValidateAudience= true,
        ValidateLifetime=true,
        ValidateIssuerSigningKey=true,
        ValidIssuer=jwtSettings["Issuer"],
        ValidAudience=jwtSettings["Audience"],
        IssuerSigningKey=new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]))
    };

});




var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//Connetion setup to the database


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
