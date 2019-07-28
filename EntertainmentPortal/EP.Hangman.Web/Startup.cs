using System.Collections.Generic;
using EP.Hangman.Logic.Queries;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MediatR;
using NJsonSchema;
using AutoMapper;
using EP.Hangman.Logic;
using EP.Hangman.Logic.Commands;
using EP.Hangman.Logic.Profiles;
using EP.Hangman.Logic.Validators;
using EP.Hangman.Web.Filters;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Logging;
using NSwag;
using NSwag.AspNetCore;
using Serilog;

namespace EP.Hangman.Web
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
            IdentityModelEventSource.ShowPII = true;
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie()
                .AddIdentityServerAuthentication(JwtBearerDefaults.AuthenticationScheme, opt =>
                {
                    opt.Authority = "http://localhost:8088";
                    opt.RequireHttpsMetadata = false;
                })
                .AddGoogle("Google", opt =>
                {
                    opt.CallbackPath = new PathString("/api/google");
                    opt.ClientId = "989223055328-e62itaec1uvah9nmupbia4tcknc7mhcs.apps.googleusercontent.com";
                    opt.ClientSecret = "WvH2nOhEfFSnMZaovmLr_2sS";
                    opt.UserInformationEndpoint = "https://www.googleapis.com/oauth2/v2/userinfo";
                })
                .AddFacebook("Facebook", opt =>
                {
                    opt.CallbackPath = new PathString("/api/facebook");
                    opt.AppId = "1457803857684622";
                    opt.AppSecret = "f03c47bcbffa666f79877e98dcac4557";
                });
            services.AddAuthorization(opt => opt.AddPolicy("google", 
                cfg => cfg.AddAuthenticationSchemes("Google").RequireAuthenticatedUser()));
            services.AddMemoryCache();

            services.AddSwaggerDocument(cfg =>
            {
                cfg.SchemaType = SchemaType.OpenApi3;
                cfg.AddSecurity("oauth", new[] {"hangman_api"}, new OpenApiSecurityScheme()
                {
                    Flow = OpenApiOAuth2Flow.Implicit,
                    Type = OpenApiSecuritySchemeType.OAuth2,
                    AuthorizationUrl = "http://localhost:8088/connect/authorize",
                    Scopes = new Dictionary<string, string>()
                    {
                        {"hangman_api", "Access to hangman game api" }
                    }
                });
                cfg.Title = "Hangman API";
                cfg.Description = "AlMak 2019";
            });

            services.AddMediatR(typeof(GetUserSession).Assembly);
            services.AddMediatR(typeof(CheckLetterCommand).Assembly);
            services.AddAutoMapper(typeof(MapperProfile).Assembly);
            services.AddGameServices();
            services.AddCors();

            services.AddMvc(opt => opt.Filters.Add(typeof(GlobalExceptionFilter)))
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddFluentValidation(cfg =>
                {
                    cfg.RegisterValidatorsFromAssemblyContaining<DeleteGameValidator>();
                    cfg.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                });
            
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IMediator mediator)
        {
            app.UseCors(o =>
                o.AllowAnyHeader()
                .WithMethods("GET","POST","PUT", "DELETE", "OPTIONS")
                .WithOrigins("http://localhost:4200", "http://frontend:8084")
                .AllowCredentials()
                );

            app.UseAuthentication();

            app.UseOpenApi().UseSwaggerUi3(opt => opt.OAuth2Client = new OAuth2ClientSettings()
            {
                AppName = "Hangman game",
                ClientId = "swagger"
            });

            mediator.Send(new CreateDatabaseCommand()).Wait();
            app.UseOpenApi();
            app.UseSwaggerUi3();
            app.UseMvc(routes => routes.MapRoute("default", "{controller = PlayHangman}/{action = Index}/{id?}"));

            Log.Logger = new LoggerConfiguration()
               //By default we have up to 31 latest log files with file size limited up to 1GB
                //Shared parameter means that several processes can log simultaneously
               .WriteTo.RollingFile("Logs/hangman_log_{Date}.txt", shared:true)
               .WriteTo.Console()
               .CreateLogger();
        }
    }
}
