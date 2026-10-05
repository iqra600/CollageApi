using AutoMapper;
using CollageApi.Configuration.Automapper;
using CollageApi.Data;
using CollageApi.Models;
using CollageApi.Repository;
using CollageApi.ViewModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.VisualBasic;
using Serilog;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography.Xml;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var key = Encoding.ASCII.GetBytes(builder.Configuration.GetValue<string>("JWTSecret"));
var issuerLocalKey = builder.Configuration.GetValue<string>("issuerLocal");
var audianceLocal = builder.Configuration.GetValue<string>("audianceLocal");



//builder.Logging.ClearProviders();
//builder.Logging.AddConsole();
Log.Logger = new LoggerConfiguration()
    .WriteTo.File("log/log.txt", rollingInterval: RollingInterval.Minute)
    .MinimumLevel.Information()
    .CreateLogger();
builder.Host.UseSerilog();//this will overide builtin logger.
builder.Logging.AddSerilog();//and this will allow also buildin logger with this third party logger intigration.
builder.Services.AddDbContext<CollageContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("CollageCS"))
);
// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//configuration for secure endpoints in swagger ui.
builder.Services.AddSwaggerGen(
    options=> {
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme {

                                Description="",
                                Name="Authorization",
                                Scheme="Bearer",
                                In=ParameterLocation.Header

        
        
        });
        options.AddSecurityRequirement(new OpenApiSecurityRequirement() {
            {

                new OpenApiSecurityScheme
                {
                  Reference=  new OpenApiReference
                    {
                        Id="Bearer",
                        Type=ReferenceType.SecurityScheme,




                    },
                  Scheme="oathu2",
                  Name="Bearer",
                  In=ParameterLocation.Header

                },
                new List<string>()

            }



        });
    
    }
    
    );
builder.Services.AddScoped<IStudentRepo, StudentImpliments>();
//builder.Services.AddScoped(typeof(ICommonRepo<>), typeof(CommonRepo<>));
//builder.Services.AddAutoMapper(cfg =>new AutoStudentConfig() );
builder.Services.AddAutoMapper(
    cfg => { },
    typeof(AutoStudentConfig)
);
builder.Services.AddCors(
    Options =>
    {
        Options.AddPolicy("AllowtoEveryone",
            policy =>
            {
                
                policy.AllowAnyHeader();
                policy.AllowAnyOrigin();
                policy.AllowAnyMethod();
            }
            );
        Options.AddPolicy("AllowtLocaolHost",
           policy =>
           {

               policy.WithOrigins("http://127.0.0.1:5500");
              
           }
           );
    }


                       );
//jwt configuration.
builder.Services.AddAuthentication(
    options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }

    ).AddJwtBearer( options =>
    {
        options.SaveToken = true;
        options.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidateAudience = true,
            ValidAudience = audianceLocal,

           
            ValidateIssuer = true,
            ValidIssuer= issuerLocalKey,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey((key)),
        };
    }
    );
    //.AddJwtBearer("JWT2",options =>
    //{
    //    options.SaveToken = true;
    //    options.TokenValidationParameters = new TokenValidationParameters()
    //    {
    //        ValidateAudience = false,
    //        ValidateIssuerSigningKey = true,
    //        ValidateIssuer = false,
    //        IssuerSigningKey = new SymmetricSecurityKey((key)),
    //    };
    //}    
    //);



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors("AllowtoEveryone");

//this middleware allow to apply cors on endpoints direct to map.
//it will overide the applcation level cors if we hit these urls. 
//app.UseEndpoints(
//    endpoints =>
//    {
//        endpoints.MapGet("api/test1", context => context.Response.WriteAsync("reading secret key = "+key)).RequireCors("AllowtoEveryone");

//        endpoints.MapGet("api/test2", context => context.Response.WriteAsync("tesing endpoint 2")).RequireCors("AllowtLocaolHost");


//    }

//    );
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Run();
