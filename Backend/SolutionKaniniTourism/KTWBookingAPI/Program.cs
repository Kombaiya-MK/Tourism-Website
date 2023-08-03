#nullable disable
using KTWBookingAPI.Interfaces;
using KTWBookingAPI.Models;
using KTWBookingAPI.Services;
using KTWBookingAPI.Services.Commands;
using KTWBookingAPI.Services.Queries;
using KTWBookingAPI.Utilities.Adapters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAzureClients(clientBuilder =>
{
    clientBuilder.AddBlobServiceClient(builder.Configuration["BookingStore:blob"], preferMsi: true);
    clientBuilder.AddQueueServiceClient(builder.Configuration["BookingStore:queue"], preferMsi: true);
});

//User Defined Services
builder.Services.AddDbContext<BookingContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration.GetConnectionString("BookingConn"));
});
builder.Services.AddScoped<ICommandRepo<Booking, string>, BookingCommandRepo>();
builder.Services.AddScoped<ICommandRepo<PackageBooking, string>, PackageCommandRepo>();
builder.Services.AddScoped<ICommandRepo<Customer, string>, CustomerCommandRepo>();
builder.Services.AddScoped<IQueryRepo<Booking, string>, BookingQueryRepo>();
builder.Services.AddScoped<IQueryRepo<PackageBooking, string>, PackageQueryRepo>();
builder.Services.AddScoped<IQueryRepo<Customer, string>, CustomerQueryRepo>();
builder.Services.AddScoped<IBookingService, BookingService>();
builder.Services.AddScoped<IAdapter, BookingAdapter>();

//CORS Service Injection
builder.Services.AddCors(opts =>
{
    opts.AddPolicy("MyCors", policy =>
    {
        policy.AllowAnyHeader()
        .AllowAnyMethod()
        .AllowAnyOrigin();
    });
});

//Authentication Inside Swagger UI
builder.Services.AddSwaggerGen(c => {
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "JWT Authorization header using the Bearer scheme."
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


//JWT Authentication Service
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["TokenKey"])),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

//Serilog Injection
Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).CreateLogger();
builder.Host.UseSerilog();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("MyCors");
app.UseSerilogRequestLogging();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
