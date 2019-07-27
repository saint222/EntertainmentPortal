using System.Collections.Generic;
using AutoMapper;
using EP.SeaBattle.Logic;
using EP.SeaBattle.Logic.Commands;
using EP.SeaBattle.Logic.Profiles;
using EP.SeaBattle.Logic.Validators;
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

namespace EP.SeaBattle.Web
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
                    opt.Authority = "https://seabattle.me:44360";
                    opt.RequireHttpsMetadata = false;
                })
                //.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
                //{
                //    opt.RequireHttpsMetadata = true;
                //    opt.TokenValidationParameters = new TokenValidationParameters()
                //    {
                //        ValidIssuer = SeaBattleConstants.ISSUER_NAME,
                //        ValidateIssuer = true,
                //        ValidAudience = SeaBattleConstants.AUDIENCE_NAME,
                //        ValidateAudience = true,
                //        ValidateIssuerSigningKey = true,
                //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SeaBattleConstants.SECRET)),
                //        ValidateLifetime = true,
                //        RequireExpirationTime = true,
                //        RequireSignedTokens = true
                //    };
                //})

                .AddGoogle("Google", opt =>
                {
                    opt.CallbackPath = new PathString("/api/google");
                    opt.ClientId = "182553425793-e7el4sro3tql8qrr554cmrb654efjg1n.apps.googleusercontent.com";
                    opt.ClientSecret = "CXPn7PLScBMv64EPyTE7rOlk";
                });

            services.AddAuthorization(opt =>
            {
                opt.AddPolicy("google",
                    cfg => cfg.AddAuthenticationSchemes("Google")
                        .RequireAuthenticatedUser());
            });

            services.AddMemoryCache();

            services.AddSwaggerDocument(cfg =>
            {
                cfg.SchemaType = SchemaType.Swagger2;
                cfg.Title = "sea-battle-2019";
                cfg.AddSecurity("oauth", new[] { "sea-battle-2019" }, new OpenApiSecurityScheme()
                {
                    Flow = OpenApiOAuth2Flow.Implicit,
                    Type = OpenApiSecuritySchemeType.OAuth2,
                    //AuthorizationUrl = "http://localhost:5000/connect/authorize",
                    AuthorizationUrl = "https://seabattle.me:44360/connect/authorize",
                    Scopes = new Dictionary<string, string>()
                    {
                        {"sea-battle-2019", "Access to sea-battle-2019 game api" }
                    }
                });
            });

            services.AddLogging();
            services.AddMediatR(typeof(AddNewPlayerCommand).Assembly);
            services.AddAutoMapper(typeof(CellProfile).Assembly);
            services.AddSeaBattleServices();
            services.AddCors();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddFluentValidation(cfg =>
                {
                    cfg.RegisterValidatorsFromAssemblyContaining<ShipAddValidation>();
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
            else
            {
                app.UseHsts();
            }




            app.UseAuthentication();
            app.UseOpenApi().UseSwaggerUi3(opt => opt.OAuth2Client = new OAuth2ClientSettings()
            {
                AppName = "sea-battle-2019",
                ClientId = "swagger"
            });
            //app.UseIdentityServer();

            mediator.Send(new CreateDatabaseCommand()).Wait();
            //app.UseSession();
            app.UseMvc();
            app.UseCors(o =>
                        o.AllowAnyHeader()
                        .AllowAnyMethod()
                        //.WithOrigins("*")//http://localhost:4200")
                        .AllowAnyOrigin()
                        .AllowCredentials()
                        );
        }
    }
}
