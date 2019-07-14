using System.Security.Claims;
using System.Text;
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
using EP.Hangman.Web.Constants;
using EP.Hangman.Web.Filters;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
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
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie()
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
                {
                    opt.RequireHttpsMetadata = true;
                    opt.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidIssuer = HangmanConstants.ISSUER_NAME,
                        ValidateIssuer = true,
                        ValidAudience = HangmanConstants.AUDIENCE_NAME,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(HangmanConstants.SECRET)),
                        ValidateLifetime = true,
                        RequireExpirationTime = true,
                        RequireSignedTokens = true
                    };
                })
                .AddGoogle("Google", opt =>
                {
                    opt.CallbackPath = new PathString("/api/google");
                    opt.ClientId = Configuration["Google:ClientId"];
                    opt.ClientSecret = Configuration["Google:ClientSecret"];
                    opt.UserInformationEndpoint = "https://www.googleapis.com/oauth2/v2/userinfo";
                })
                .AddFacebook("Facebook", opt =>
                {
                    opt.CallbackPath = new PathString("/api/facebook");
                    opt.AppId = Configuration["Facebook:AppId"];
                    opt.AppSecret = Configuration["Facebook:AppSecret"];
                });
            services.AddAuthorization(opt => opt.AddPolicy("google", 
                cfg => cfg.AddAuthenticationSchemes("Google").RequireAuthenticatedUser()));
            services.AddMemoryCache();
            services.AddSwaggerDocument(conf => conf.SchemaType = SchemaType.OpenApi3);
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
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(o =>
                o.AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin());

            app.UseAuthentication();

            mediator.Send(new CreateDatabaseCommand()).Wait();
            app.UseOpenApi();
            app.UseSwaggerUi3();
            app.UseMvc(routes => routes.MapRoute("default", "{controller = PlayHangman}/{action = Index}/{id?}"));

            Log.Logger = new LoggerConfiguration()
                //By default we have up to 31 latest log files with file size limited up to 1GB
                //Shared parameter means that several processes can log simultaneously
                .WriteTo.RollingFile("Logs/hangman_log_{Date}.txt", shared:true)
                .CreateLogger();
        }
    }
}
