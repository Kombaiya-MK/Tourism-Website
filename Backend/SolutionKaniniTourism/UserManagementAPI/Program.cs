using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using System.Text;
using System.Windows.Input;
using UserAPI.Interfaces;
using UserAPI.Services;
using UserManagementAPI.Interfaces;
using UserManagementAPI.Models;
using UserManagementAPI.Services;
using UserManagementAPI.Services.Commands;
using UserManagementAPI.Services.Queries;
using UserManagementAPI.Utilities.Adapters;

namespace UserManagementAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //User Defined Services Injections
            builder.Services.AddDbContext<UserManagementContext>(opts =>
            {
                opts.UseSqlServer(builder.Configuration.GetConnectionString("UserConn"));
            });
            builder.Services.AddScoped<ICommandRepo<User,string>,UserCommandsRepo>();
            builder.Services.AddScoped<ICommandRepo<UserDetails, string>, UserDetailsCommandsRepo>();
            builder.Services.AddScoped<ICommandRepo<TravelAgent, string>, TravelAgentCommandsRepo>();
            builder.Services.AddScoped<ICommandRepo<VerificationCodes, string>, VerificationCodesCommandsRepo>();
            builder.Services.AddScoped<IQueryRepo<User, string>, UserQueryRepo>();
            builder.Services.AddScoped<IQueryRepo<UserDetails, string>, UserDetailsQueryRepo>();
            builder.Services.AddScoped<IQueryRepo<TravelAgent, string>, TravelAgentQueryRepo>();
            builder.Services.AddScoped<IQueryRepo<VerificationCodes, string>, VerificationCodesQueryRepo>();
            builder.Services.AddScoped<IManageUser,ManageUserService>();
            builder.Services.AddScoped<ITokenGenerate,TokenService>();
            builder.Services.AddScoped<IAdapter,UserAdapter>();


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
        }
    }
}