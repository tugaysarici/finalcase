using System.Data.Entity;
using System.Reflection;
using System.Text;
using AutoMapper;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Oms.Base.Logger;
using Serilog;
using StackExchange.Redis;
using Oms.Base.Logger;
using Oms.Base.Token;
using Oms.Data.Context;
using Oms.Data.Uow;
using Oms.Operation;
using Oms.Operation.Mapper;
using Oms.Operation.Validation;
using Oms.Api.Middleware;
using static Oms.Operation.Cqrs.UserCqrs;

namespace Oms.Api;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }


    public void ConfigureServices(IServiceCollection services)
    {
        string connection = Configuration.GetConnectionString("MsSqlConnection");

        services.AddDbContext<OmsDbContext>(opts => opts.UseSqlServer(connection));

        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy",
                builder => builder
                    .WithOrigins("http://localhost:4200")  // Allow requests from this origin
                    .AllowAnyMethod()                       // Allow all HTTP methods
                    .AllowAnyHeader()                       // Allow all HTTP headers
                    .AllowCredentials()                     // Allow credentials (cookies, etc.)
                );
        });

        var JwtConfig = Configuration.GetSection("JwtConfig").Get<JwtConfig>();
        services.Configure<JwtConfig>(Configuration.GetSection("JwtConfig"));

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddMediatR(typeof(CreateUserCommand).GetTypeInfo().Assembly);

        var config = new MapperConfiguration(cfg => { cfg.AddProfile(new MapperConfig()); });
        services.AddSingleton(config.CreateMapper());

        services.AddControllers().AddFluentValidation(x =>
        {
            x.RegisterValidatorsFromAssemblyContaining<BaseValidator>();
        });

        services.AddMemoryCache();

        // redis
        var redisConfig = new ConfigurationOptions();
        redisConfig.EndPoints.Add(Configuration["Redis:Host"], Convert.ToInt32(Configuration["Redis:Port"]));
        redisConfig.DefaultDatabase = 0;
        services.AddStackExchangeRedisCache(opt =>
        {
            opt.ConfigurationOptions = redisConfig;
            opt.InstanceName = Configuration["Redis:InstanceName"];
        });

        services.AddControllersWithViews(options =>
            options.CacheProfiles.Add("Cache100", new CacheProfile
            {
                Duration = 100,
                Location = ResponseCacheLocation.Any,
            }));

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "OmsApi Order Management System", Version = "v1.0" });

            var securityScheme = new OpenApiSecurityScheme
            {
                Name = "OmsApi Order Management System for TSRC Pharmaceutical Company",
                Description = "Enter JWT Bearer token **_only_**",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "bearer",
                BearerFormat = "JWT",
                Reference = new OpenApiReference
                {
                    Id = JwtBearerDefaults.AuthenticationScheme,
                    Type = ReferenceType.SecurityScheme
                }
            };
            c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                { securityScheme, new string[] { } }
            });
        });

        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = true;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidIssuer = JwtConfig.Issuer,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(JwtConfig.Secret)),
                ValidAudience = JwtConfig.Audience,
                ValidateAudience = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.FromMinutes(2)
            };
        });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "OmsApi v1"));
        }


        app.UseMiddleware<ErrorHandlerMiddleware>();
        app.UseMiddleware<HeartBeatMiddleware>();
        Action<RequestProfilerModel> requestResponseHandler = requestProfilerModel =>
        {
            Log.Information("-------------Request-Begin------------");
            Log.Information(requestProfilerModel.Request);
            Log.Information(Environment.NewLine);
            Log.Information(requestProfilerModel.Response);
            Log.Information("-------------Request-End------------");
        };
        app.UseMiddleware<RequestLoggingMiddleware>(requestResponseHandler);

        app.UseHttpsRedirection();

        app.UseCors("CorsPolicy");

        // auth
        app.UseAuthentication();
        app.UseRouting();
        app.UseAuthorization();

        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}