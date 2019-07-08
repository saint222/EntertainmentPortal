using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

using EP._15Puzzle.Logic.Queries;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using EP._15Puzzle.Logic.Profiles;
using EP._15Puzzle.Logic;
using EP._15Puzzle.Logic.Commands;
using EP._15Puzzle.Logic.Validators;
using EP._15Puzzle.Web.Filters;
using FluentValidation.AspNetCore;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.AccessTokenValidation;
using IdentityServer4.Models;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace EP._15Puzzle.Web
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
            services.AddAuthenticationCore();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie()
                //.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
                //{
                //    opt.RequireHttpsMetadata = true;
                //    opt.TokenValidationParameters = new TokenValidationParameters()
                //    {
                //        ValidIssuer = AuthConstants.ISSUER_NAME,
                //        ValidateIssuer = true,
                //        ValidAudience = AuthConstants.AUDIENCE_NAME,
                //        ValidateAudience = true,
                //        ValidateIssuerSigningKey = true,
                //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AuthConstants.SECRET)),
                //        ValidateLifetime = true,
                //        RequireExpirationTime = true,
                //        RequireSignedTokens = true
                //    };
                //})
                .AddIdentityServerAuthentication(JwtBearerDefaults.AuthenticationScheme, opt =>
                {
                    opt.Authority = "https://localhost:44380";
                    opt.NameClaimType = JwtClaimTypes.Name;
                    opt.RoleClaimType = JwtClaimTypes.Role;
                    opt.RequireHttpsMetadata = false;
                    opt.SupportedTokens = SupportedTokens.Jwt;
                    opt.ApiName = "api";
                })
                .AddGoogle("Google", opt =>
                {
                    opt.ClientId = "734768643870-2ls26lml1ifn9kdcfoppvfmagujj8nki.apps.googleusercontent.com";
                    opt.ClientSecret = "KnuFajDb0Y-xTuaoodohxSEa";
                })
                .AddFacebook("Facebook", opt =>
                {
                    opt.AppId = "1257326831111548";
                    opt.AppSecret = "a0a9b3ced9bed2aae3cfb0b92a8e9d30";
                }); 

            services.AddAuthorization(opt =>
            {
                opt.AddPolicy("bearer",
                    cfg => cfg.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                        .RequireAuthenticatedUser());
                opt.AddPolicy("google",
                    cfg => cfg.AddAuthenticationSchemes("Google")
                        .RequireAuthenticatedUser());
                opt.AddPolicy("facebook",
                    cfg => cfg.AddAuthenticationSchemes("Facebook")
                        .RequireAuthenticatedUser());
            });


            services.Configure<IdentityOptions>(opt =>
            {
                opt.Password.RequireDigit = false;
                opt.Password.RequiredLength = 6;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireUppercase = false;
            });

            services.AddMediatR(typeof(NewDeckCommand).Assembly);
            services.AddMediatR(typeof(GetDeckQuery).Assembly);
            services.AddSwaggerDocument();
            services.AddAutoMapper(cfg=>cfg.AllowNullCollections=true,typeof(DeckProfile).Assembly);
            services.AddDeckServices();
            SetupSecurity(services);
            services.AddMvc(opt =>
                {
                    opt.Filters.Clear();
                    opt.Filters.Add(typeof(GlobalExceptionFilter));
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2).AddFluentValidation(cfg =>
            {
                cfg.RegisterValidatorsFromAssemblyContaining<MoveTileValidator>();
                cfg.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
            }); ;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IMediator mediator)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(opt =>
                opt.AllowAnyHeader()
                    .AllowAnyMethod()
                    .WithOrigins("http://localhost:4200", "https://localhost:44380", "https://accounts.google.com", "https://www.facebook.com")
                    .AllowCredentials());
            //app.UseAuthentication();
            app.UseIdentityServer();
            mediator.Send(new CreateDatabaseCommand()).Wait();
            app.UseSwagger().UseSwaggerUi3();
            app.UseMvc();
        }













        private IServiceCollection SetupSecurity(IServiceCollection services)
        {
            var builder = services.AddIdentityServer(opt =>
            {
                opt.IssuerUri = "https://localhost:44380";
                opt.Events.RaiseErrorEvents = true;
                opt.Events.RaiseFailureEvents = true;
                opt.Events.RaiseInformationEvents = true;
                opt.Events.RaiseSuccessEvents = true;
            });

            builder.AddInMemoryClients(LoadClients())
                .AddInMemoryApiResources(LoadResources())
                .AddInMemoryPersistedGrants()
                .AddInMemoryIdentityResources(LoadIdentity())
                .AddTestUsers(LoadUsers())
                .AddDeveloperSigningCredential();

            return services;
        }




        private List<IdentityResource> LoadIdentity()
        {
            return new List<IdentityResource>()
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email()
            };
        }


        private List<TestUser> LoadUsers()
        {
            return new List<TestUser>()
            {
                new TestUser()
                {
                    SubjectId = "1234-1234-1234-1234",
                    Username = "admin",
                    Password = "admin",
                    IsActive = true,
                    Claims = new List<Claim>()
                    {
                        new Claim(JwtClaimTypes.Name, "tester"),
                        new Claim(JwtClaimTypes.Email, "jupitel1993trash@gmail.com")
                    }
                }
            };
        }

        private IEnumerable<ApiResource> LoadResources()
        {
            return new[]
            {
                new ApiResource("api")
                {
                    Scopes = new List<Scope>()
                    {
                        new Scope("deck_api")
                    }
                },
            };
        }


        private IEnumerable<Client> LoadClients()
        {
            return new[]
            {
                new Client()
                {
                    ClientId = "swagger",
                    ClientSecrets = new List<Secret>() {new Secret("secret".Sha256())},
                },
                new Client()
                {
                    ClientId = "pyatnashki-front",
                    ClientSecrets = new List<Secret>() {new Secret("secret".Sha256())},
                    AllowedScopes = new List<string>(){
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "deck_api"
                    },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    AlwaysIncludeUserClaimsInIdToken = true
                },
            };
        }
    }
}
