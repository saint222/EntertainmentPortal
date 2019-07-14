using AutoMapper;
using EP.DotsBoxes.Logic;
using EP.DotsBoxes.Logic.Commands;
using EP.DotsBoxes.Logic.Profiles;
using EP.DotsBoxes.Logic.Queries;
using EP.DotsBoxes.Logic.Validators;
using EP.DotsBoxes.Web.Filters;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NJsonSchema;
using NSwag;
using NSwag.AspNetCore;
using System.Collections.Generic;

namespace EP.DotsBoxes.Web
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
                .AddIdentityServerAuthentication(JwtBearerDefaults.AuthenticationScheme, opt =>
                {
                    opt.Authority = "http://localhost:5000";
                    opt.RequireHttpsMetadata = false;
                })
                .AddGoogle("Google", opt =>
                {
                    opt.CallbackPath = new PathString("/api/google");
                    opt.ClientId = "1097896526158-k9dr1a3jppmunnu0ehg88l9ve5jibrdm.apps.googleusercontent.com";
                    opt.ClientSecret = "xTjiJQXW9xmqLfwQtF_IfzgA";
                });

            services.AddAuthorization(opt =>
            {
                opt.AddPolicy("google",
                cfg => cfg.AddAuthenticationSchemes("Google")
                .RequireAuthenticatedUser());
            });

            services.AddSwaggerDocument(cfg =>
            {
                cfg.SchemaType = SchemaType.OpenApi3;
                cfg.AddSecurity("oauth", new[] { "dotsboxes_api" }, new OpenApiSecurityScheme()
                {
                    Flow = OpenApiOAuth2Flow.Implicit,
                    Type = OpenApiSecuritySchemeType.OAuth2,
                    AuthorizationUrl = "http://localhost:5000/connect/authorize",
                    Scopes = new Dictionary<string, string>()
                    {
                        {"dotsboxes_api", "Access to dots boxes game api."}
                    }
                });
            });
            services.AddMediatR(typeof(GetAllPlayers).Assembly);
            services.AddMediatR(typeof(GetGameBoard).Assembly);
            services.AddAutoMapper(typeof(PlayerProfile).Assembly);
            services.AddAutoMapper(typeof(GameBoardProfile).Assembly);
            services.CreateGameBoardServices();
            services.AddCors(); // To enable CrossOriginResourceSharing.
            services.AddMvc(opt =>
            {
                opt.Filters.Clear();
                opt.Filters.Add(typeof(GlobalExceptionFilter));
            })
            .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
            .AddFluentValidation(cfg =>
            {
                cfg.RegisterValidatorsFromAssemblyContaining<AddPlayerValidator>();
                cfg.RegisterValidatorsFromAssemblyContaining<NewGameBoardValidator>();
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

            // CORS configuration.
            app.UseCors(opt =>
                opt.AllowAnyHeader()
                .AllowAnyMethod()
                .WithOrigins("http://localhost:4200")
                .AllowAnyOrigin());

            app.UseAuthentication();

            app.UseOpenApi().UseSwaggerUi3(opt => opt.OAuth2Client = new OAuth2ClientSettings()
            {
                AppName = "Dots Boxes game",
                ClientId = "swagger"
            });

            //mediator.Send(new CreateDatabaseCommand()).Wait();           
            app.UseMvc();
        }
    }
}
