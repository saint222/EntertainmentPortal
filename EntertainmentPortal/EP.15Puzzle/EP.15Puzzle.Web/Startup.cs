using System.Collections.Generic;
using System.Data;
using System.Security.Claims;
using AutoMapper;

using EP._15Puzzle.Logic.Queries;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EP._15Puzzle.Logic.Profiles;
using EP._15Puzzle.Logic;
using EP._15Puzzle.Logic.Commands;
using EP._15Puzzle.Logic.Validators;
using EP._15Puzzle.Web.Filters;
using FluentValidation.AspNetCore;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using NSwag.AspNetCore;
using NJsonSchema;
using NSwag;

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
                .AddIdentityServerAuthentication(JwtBearerDefaults.AuthenticationScheme, opt =>
                {
                    opt.Authority = Configuration.GetSection("Urls:Is4").Value;
                    opt.RequireHttpsMetadata = false;
                });

            services.AddAuthorization(opt =>
            {
                opt.AddPolicy("bearer",
                    cfg => cfg.AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                        .RequireAuthenticatedUser());
            });


            //services.Configure<IdentityOptions>(opt =>
            //{
            //    opt.Password.RequireDigit = false;
            //    opt.Password.RequiredLength = 6;
            //    opt.Password.RequireLowercase = false;
            //    opt.Password.RequireNonAlphanumeric = false;
            //    opt.Password.RequireUppercase = false;
            //});

            services.AddMediatR(typeof(NewDeckCommand).Assembly);
            services.AddMediatR(typeof(GetDeckQuery).Assembly);
            services.AddSwaggerDocument(cfg =>
            {
                cfg.SchemaType = NJsonSchema.SchemaType.OpenApi3;
                cfg.AddSecurity("oauth", new[] { "pyatnashki_api" }, new OpenApiSecurityScheme()
                {
                    Flow = OpenApiOAuth2Flow.Implicit,
                    Type = OpenApiSecuritySchemeType.OAuth2,
                    AuthorizationUrl = $"{Configuration.GetSection("Urls:Is4").Value}/connect/authorize",
                    Scopes = new Dictionary<string, string>()
                    {
                        {"pyatnashki_api", "Access to 15Puzzle application." }
                    }

                });
            });
            services.AddAutoMapper(cfg=>cfg.AllowNullCollections=true,typeof(DeckProfile).Assembly);
            services.AddDeckServices();
            //SetupSecurity(services);

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
                    .WithOrigins(Configuration.GetSection("Urls:Api").Value, Configuration.GetSection("Urls:Is4").Value, Configuration.GetSection("Urls:Front").Value)
                    .AllowCredentials());
            app.UseAuthentication();
            //app.UseIdentityServer();
            mediator.Send(new CreateDatabaseCommand()).Wait();

            app.UseOpenApi().UseSwaggerUi3(opt =>
            {
                opt.OAuth2Client = new OAuth2ClientSettings()
                {
                    AppName = "pyatnashki",
                    ClientId = "swagger"
                };
            });
            app.UseMvc();
        }













        //private IServiceCollection SetupSecurity(IServiceCollection services)
        //{
        //    var builder = services.AddIdentityServer(opt =>
        //    {
        //        opt.IssuerUri = "https://localhost:44380";
        //        opt.Events.RaiseErrorEvents = true;
        //        opt.Events.RaiseFailureEvents = true;
        //        opt.Events.RaiseInformationEvents = true;
        //        opt.Events.RaiseSuccessEvents = true;
        //    });

        //    builder.AddInMemoryClients(LoadClients())
        //        .AddInMemoryApiResources(LoadResources())
        //        .AddInMemoryPersistedGrants()
        //        .AddInMemoryIdentityResources(LoadIdentity())
        //        .AddTestUsers(LoadUsers())
        //        .AddDeveloperSigningCredential();

        //    return services;
        //}




        //private List<IdentityResource> LoadIdentity()
        //{
        //    return new List<IdentityResource>()
        //    {
        //        new IdentityResources.OpenId(),
        //        new IdentityResources.Profile(),
        //        new IdentityResources.Email()
        //    };
        //}


        //private List<TestUser> LoadUsers()
        //{
        //    return new List<TestUser>()
        //    {
        //        new TestUser()
        //        {
        //            SubjectId = "1234-1234-1234-1234",
        //            Username = "admin",
        //            Password = "admin",
        //            IsActive = true,
        //            Claims = new List<Claim>()
        //            {
        //                new Claim(JwtClaimTypes.Name, "tester"),
        //                new Claim(JwtClaimTypes.Email, "jupitel1993trash@gmail.com")
        //            }
        //        }
        //    };
        //}

        //private IEnumerable<ApiResource> LoadResources()
        //{
        //    return new[]
        //    {
        //        new ApiResource("api")
        //        {
        //            Scopes = new List<Scope>()
        //            {
        //                new Scope("pyatnashki_api")
        //            }
        //        },
        //    };
        //}


        //private IEnumerable<Client> LoadClients()
        //{
        //    return new[]
        //    {
        //        new Client()
        //        {
        //            ClientId = "swagger",
        //            ClientSecrets = new List<Secret>() {new Secret("secret".Sha256())},
        //        },
        //        new Client()
        //        {
        //            ClientId = "pyatnashki-front",
        //            ClientSecrets = new List<Secret>() {new Secret("secret".Sha256())},
        //            AllowedScopes = new List<string>(){
        //                IdentityServerConstants.StandardScopes.OpenId,
        //                IdentityServerConstants.StandardScopes.Profile,
        //                IdentityServerConstants.StandardScopes.Email,
        //                "deck_api"
        //            },
        //            AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
        //            AlwaysIncludeUserClaimsInIdToken = true
        //        },
        //    };
        //}
    }
}
