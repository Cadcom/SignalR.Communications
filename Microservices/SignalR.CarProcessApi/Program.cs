using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using SignalR.Business.Abstract;
using SignalR.Business.Concrete;
using SignalR.CarProcessApi.Hubs;
using SignalR.CarProcessApi.Middelwares;
using SignalR.CarProcessApi.Subscriptions;
using SignalR.Data;
using SignalR.Data.Abstract;
using SignalR.Data.Concrate;

using SignalR.Shared.Entities;
using System.Globalization;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSignalR();
builder.Services.AddSingleton<DatabaseSubscription<Car>>();
builder.Services.AddDbContext<appContext>(options =>
{
    options.UseSqlServer(builder.Configuration["myConnectionString"], b => b.MigrationsAssembly("SignalR.Data"));
});

builder.Services.AddScoped<DbContext>(provider => provider.GetService<appContext>());


builder.Services.AddScoped<IDatabaseHelper, DatabaseHelper>();

builder.Services.AddScoped<IDatabaseService, DatabaseService>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = true,
            ValidateIssuer = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JWT:Issuer"],
            ValidAudience = builder.Configuration["JWT:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Security"])),
            ClockSkew = TimeSpan.Zero
        };
    });



builder.Services.AddCors(options => options.AddDefaultPolicy(policiy => 
            policiy.AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()
            .WithOrigins("https://localhost:4000", "http://localhost:4001","https://localhost:7237", "http://localhost:5237")
            .SetIsOriginAllowed(origin => true)
            )
);



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors();
app.UseHttpsRedirection();
app.UseRouting();
app.UseDatabaseSubscription<DatabaseSubscription<Car>>("Cars");


app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHub<ProcessHub>("/ProcessHub", options =>
    {
        options.Transports = HttpTransportType.WebSockets;
    });

    endpoints.MapControllers();
    

   
});

app.Run();
