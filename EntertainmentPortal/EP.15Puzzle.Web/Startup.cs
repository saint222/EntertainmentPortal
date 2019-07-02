using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
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
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
                .AddGoogle("Google", opt =>
                {
                    opt.ClientId = "734768643870-2ls26lml1ifn9kdcfoppvfmagujj8nki.apps.googleusercontent.com";
                    opt.ClientSecret = "KnuFajDb0Y-xTuaoodohxSEa";
                }); 

            services.AddAuthorization(opt =>
            {
                opt.AddPolicy("google",
                    cfg => cfg.AddAuthenticationSchemes("Google")
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
                    .WithOrigins("http://localhost:4200")
                    .AllowCredentials());
            app.UseAuthentication();
            mediator.Send(new CreateDatabaseCommand()).Wait();
            app.UseSwagger().UseSwaggerUi3();
            app.UseMvc();
           
        }
    }
}
