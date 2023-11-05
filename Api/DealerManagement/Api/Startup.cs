using Api.Middleware;
using AutoMapper;
using Base.Logger;
using Base.Token;
using Data.Context;
using Data.Uow;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Operation.Cqrs;
using Operation.Mapper;
using Serilog;
using System;
using System.Reflection;
using System.Text;

namespace Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            string connection = Configuration.GetConnectionString("MsSqlConnection");
            services.AddDbContext<DealerDbContext>(opts => opts.UseSqlServer(connection));

            var JwtConfig = Configuration.GetSection("JwtConfig").Get<JwtConfig>();
            services.Configure<JwtConfig>(Configuration.GetSection("JwtConfig"));

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddMediatR(typeof(CreateUserCommand).GetTypeInfo().Assembly);

            var config = new MapperConfiguration(cfg => { cfg.AddProfile(new MapperConfig()); });
            services.AddSingleton(config.CreateMapper());

            services.AddControllers().AddFluentValidation();

            services.AddMemoryCache();

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

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "DealerApi Api Management", Version = "v1.0" });

                var securityScheme = new OpenApiSecurityScheme
                {
                    Name = "DealerApi Management for IT Company",
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
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api v1"));
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

            app.UseAuthentication();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors("CorsPolicy");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
