using System;
using System.Text;
using System.Security.Claims;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using MediatR;
using NJsonSchema;
using EP.TicTacToe.Logic.Commands;
using EP.TicTacToe.Logic.Profiles;
using EP.TicTacToe.Logic.Queries;
using EP.TicTacToe.Logic.Services;
using EP.TicTacToe.Logic.Validators;
using EP.TicTacToe.Web.Constants;
using EP.TicTacToe.Web.Filters;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using NSwag;
using NSwag.AspNetCore;
using Serilog;

#pragma warning disable 618

namespace EP.TicTacToe.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddAuthentication(o =>
                {
                    o.DefaultAuthenticateScheme =
                        CookieAuthenticationDefaults.AuthenticationScheme;
                    o.DefaultChallengeScheme = GoogleDefaults.AuthenticationScheme;
                })
                .AddCookie()
                .AddIdentityServerAuthentication(JwtBearerDefaults.AuthenticationScheme,
                    opt =>
                    {
                        opt.Authority = "http://localhost:5000";
                        opt.RequireHttpsMetadata = false;
                    })
                .AddGoogle().Services
                .Configure<GoogleOptions>(GoogleDefaults.AuthenticationScheme,
                    Configuration.GetSection("Authentication:Google"));

            services.AddAuthorization(opt => opt.AddPolicy("google",
                cfg => cfg.AddAuthenticationSchemes("Google")
                    .RequireAuthenticatedUser()));

            services.AddSwaggerDocument(cfg =>
            {
                cfg.Title = "TicTacToe game API";
                cfg.DocumentName = "API";
                cfg.SchemaType = SchemaType.OpenApi3;
                cfg.AddSecurity("oauth", new[] {"tictactoe_api"},
                    new OpenApiSecurityScheme()
                    {
                        Flow = OpenApiOAuth2Flow.Implicit,
                        Type = OpenApiSecuritySchemeType.OAuth2,
                        AuthorizationUrl = "http://localhost:5000/connect/authorize",
                        Scopes = new Dictionary<string, string>()
                        {
                            {
                                "tictactoe_api", "Access to TicTacToe game api"
                            }
                        }
                    });
            });

            services.AddAutoMapper(typeof(PlayerProfile).Assembly);
            services.AddMediatR(typeof(GetAllPlayers).Assembly);
            services.AddGameServices();
            services.AddCors();

            services.AddMvc(opt =>
                {
                    opt.Filters.Clear();
                    opt.Filters.Add(typeof(GlobalExceptionFilter));
                    //opt.Filters.Add(typeof(GlobalActionFilter));
                    //opt.Filters.Add(typeof(LocalActionFilterAttribute));
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddFluentValidation(cfg =>
                {
                    cfg.RegisterValidatorsFromAssemblyContaining<GetGameValidator>();
                    cfg.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                });

            services.AddLogging();
        }
        
        
        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="mediator"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
                              IMediator mediator)
        {
            if (env.IsDevelopment()) app.UseDeveloperExceptionPage();

            app.UseCors(o =>
                o.AllowAnyHeader()
                    .AllowAnyMethod()
                    .WithOrigins("http://localhost:4200")
                    .AllowCredentials()
            );

            app.UseAuthentication();

            app.UseOpenApi().UseSwaggerUi3(opt => opt.OAuth2Client =
                new OAuth2ClientSettings()
                {
                    AppName = "TicTacToe game",
                    ClientId = "swagger"
                });

            mediator.Send(new CreateDatabaseCommand()).Wait();
            app.UseOpenApi();
            app.UseSwagger().UseSwaggerUi3();
            app.UseMvcWithDefaultRoute();

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Console()
                .WriteTo.Debug()
                .WriteTo.RollingFile(
                    Environment.CurrentDirectory +
                    $@".LoggerData\log-{DateTime.UtcNow.Kind}.txt",
                    shared: true)
                .WriteTo.SQLite(Environment.CurrentDirectory +
                                @".LoggerData\Logger.db")
                .Enrich.WithProperty("App TicTacToe", "Serilog Web App TicTacToe")
                .CreateLogger();
        }
    }
}