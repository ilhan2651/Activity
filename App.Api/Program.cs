using App.Api.DI;
using App.Api.Middlewares;
using App.Entities;
using App.Repositories.Context;
using App.Services.Mapping;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using FluentValidation.AspNetCore;

using System.Text;
using App.Services.Validation;
using FluentValidation;
using App.Services.Services.Abstract;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ActivityProjectContext>(options =>
    options.UseNpgsql(connectionString));



builder.WebHost.UseUrls("https://localhost:44344", "http://0.0.0.0:44344");


//---------------IDENTITY---------------------
builder.Services.AddIdentity<AppUser, AppRole>(options =>
{

    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 6;


    options.User.RequireUniqueEmail = true;

})
.AddEntityFrameworkStores<ActivityProjectContext>()
.AddDefaultTokenProviders()
.AddErrorDescriber<TurkishIdentityErrorDescriber>();

//------------------JWT-----------------------
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                Console.WriteLine(" JWT Middleware tetiklendi...");

                if (context.Request.Cookies.ContainsKey("JWTToken"))
                {
                    context.Token = context.Request.Cookies["JWTToken"];
                    Console.WriteLine(" JWTToken çerezden alýndý: " + context.Token);
                }
                else
                {
                    Console.WriteLine(" JWTToken bulunamadý veya boþ. Kimlik doðrulama baþarýsýz olmalý.");
                }

                return Task.CompletedTask;
            },
            OnAuthenticationFailed = context =>
            {
                Console.WriteLine(" JWT Authentication baþarýsýz oldu: " + context.Exception.Message);
                return Task.CompletedTask;
            }
        };

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
    });












builder.Services.AddApplicationServices();
builder.Services.AddAutoMapper(typeof(EventMapping).Assembly);


builder.Services.AddControllers();

builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<RegisterDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateContactValidator>();



builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var corsPolicy = "_myAllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: corsPolicy,
                      policy =>
                      {
                          policy.WithOrigins("https://localhost:7006") // MVC'nin çalýþtýðý portu ekle
                                .AllowAnyHeader()
                                .AllowAnyMethod();
                      });
});

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // wwwroot klasörü için gerekli

app.UseCors(corsPolicy);

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.Use(async (context, next) =>
{
    context.Response.Headers["Cache-Control"] = "no-store, no-cache, must-revalidate";
    context.Response.Headers["Pragma"] = "no-cache";
    context.Response.Headers["Expires"] = "-1";
    await next();
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
