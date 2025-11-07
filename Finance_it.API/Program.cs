using System.Text;
using Finance_it.API.Data.AppDbContext;
using Finance_it.API.Infrastructure.BackgroundServices;
using Finance_it.API.Infrastructure.Security;
using Finance_it.API.Repositories.GenericRepositories;
using Finance_it.API.Services.AuthServices;
using Finance_it.API.Services.FinancialAgregatesServices;
using Finance_it.API.Services.FinancialEntryServices;
using Finance_it.API.Services.MonthlyAgregateServices;
using Finance_it.API.Services.RefreshTokenServices;
using Finance_it.API.Services.UserServices;
using Finance_it.API.Services.WeeklyAgregateServices;
using Finance_it.API.Services.YearlyAgregatesServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("EFCoreDBConnection")));
builder.Services.AddAutoMapper(typeof(Program).Assembly);

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options => 
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
        });

builder.Services.AddHostedService<YearlyBackgroundService>();
builder.Services.AddHostedService<MonthlyBackgroundService>();
builder.Services.AddHostedService<WeeklyBackgroundService>();

builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IRefreshTokenService, RefreshTokenService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IFinancialEntryService, FinancialEntryService>();
builder.Services.AddScoped<IFinancialAgregatesService, FinancialAgregatesService>();
builder.Services.AddScoped<IWeeklyAgregatesService, WeeklyAgregatesService>();
builder.Services.AddScoped<IMonthlyAgregatesService, MonthlyAgregatesService>();
builder.Services.AddScoped<IYearlyAgregatesService, YearlyAgregatesService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy.WithOrigins("http://localhost:5173") 
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
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
app.UseCors("AllowFrontend");

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
