#nullable disable
using KTWTourPackages.Interfaces;
using KTWTourPackages.Models;
using KTWTourPackages.Services;
using KTWTourPackages.Services.Commands;
using KTWTourPackages.Services.Queries;
using KTWTourPackages.Utilities.Adapters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
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

//User Defined Services
builder.Services.AddDbContext<PackageContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration.GetConnectionString("TourConn"));
});
builder.Services.AddScoped<ICommandRepo<TourPackage, string>, PackCommandRepo>();
builder.Services.AddScoped<ICommandRepo<Itinerary, string>, ItineraryCommandRepo>();
builder.Services.AddScoped<ICommandRepo<ItineraryItem, string>, ItemCommandRepo>();
builder.Services.AddScoped<IQueryRepo<TourPackage, string>, PackageQueryRepo>();
builder.Services.AddScoped<IQueryRepo<Itinerary, string>, ItineraryQueryRepo>();
builder.Services.AddScoped<IQueryRepo<ItineraryItem, string>, ItemQueryRepo>();
builder.Services.AddScoped<ITourPackService, PackageService>();
builder.Services.AddScoped<IAdapter, PackageAdapter>();

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
