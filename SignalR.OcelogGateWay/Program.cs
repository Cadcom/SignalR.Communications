using Microsoft.EntityFrameworkCore;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Tracing.Butterfly;
using Ocelot.Values;
using SignalR.Business.Abstract;
using SignalR.Business.Concrete;
using SignalR.Data;
using SignalR.Data.Abstract;
using SignalR.Data.Concrate;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("ocelot.json");

// Add services to the container.
builder.Services.AddDbContext<appContext>(options =>
{
    options.UseSqlServer(builder.Configuration["myConnectionString"], b => b.MigrationsAssembly("SignalR.Data"));
});
builder.Services.AddScoped<DbContext>(provider => provider.GetService<appContext>());
builder.Services.AddTransient<IDatabaseHelper, DatabaseHelper>();
builder.Services.AddTransient<IDatabaseService, DatabaseService>();


builder.Services.AddCors(options => options.AddDefaultPolicy(policiy => 
            policiy.AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials()
            .WithOrigins("https://localhost:7237", "http://localhost:5237")
            .SetIsOriginAllowed(origin => true)
        )
);
//builder.Services.AddCors(o => o.AddPolicy("CorsPolicy", builder =>
//{
//    builder.AllowAnyHeader()
//                   .AllowAnyMethod()
//                   .SetIsOriginAllowed((host) => true)
//                   .AllowCredentials();
//}));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddOcelot(); // this comes from Ocelot.Tracing.Butterfly package
    //.AddButterfly(option =>
    //{
    //    //this is the url that the butterfly collector server is running on...
    //    option.CollectorUrl = "http://localhost:4005";
    //    option.Service = "Ocelot";
    //});



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


app.UseWebSockets();
app.UseOcelot().Wait();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();
