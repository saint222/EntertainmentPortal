using AutoMapper;
using EP.Balda.Logic.Commands;
using EP.Balda.Logic.Profiles;
using EP.Balda.Logic.Services;
using EP.Balda.Web.Filters;
using EP.Balda.Web.Validators;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NJsonSchema;
using System;
using System.Threading.Tasks;

namespace EP.Balda.Web
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie()
                .AddGoogle("Google", opt =>
                 {
                     var googleAuthNSection =
                         Configuration.GetSection("Authentication:Google");

                     opt.ClientId = "960534110357-koidlpbpdt5l0jpndqrsgkum1ff5h03m.apps.googleusercontent.com";
                     opt.ClientSecret = "-gMsN9tgw7YJR-gH4664Xk0_";
                     opt.CallbackPath = new PathString("/signin-google");
                 })
                .AddFacebook("Facebook", opt =>
                {
                    var facebookAuthNSection =
                        Configuration.GetSection("Authentication:Facebook");

                    opt.ClientId = "1264093160420061";
                    opt.ClientSecret = "cd8f9ece6350733da4949165f3350dd9";
                    opt.CallbackPath = new PathString("/signin-facebook");
                });

            services.AddAuthorization(opt =>
            {
                opt.AddPolicy("google",
                      cfg => cfg.AddAuthenticationSchemes("Google")
                          .RequireAuthenticatedUser());
                opt.AddPolicy("facebook",
                    cfg => cfg.AddAuthenticationSchemes("Facebook")
                    .RequireAuthenticatedUser());
            });

            services.AddSession();

            services.AddSwaggerDocument(cfg =>
            {
                cfg.SchemaType = SchemaType.OpenApi3;
                cfg.Title = "Balda Game";
                cfg.Description = "Balda is the linguistic board game in which it is necessary to make up words by means of the letters added in the certain way on the square game board. The winner is the person with more score points. One score point is given for one letter of added word.";
            });

            services.AddAutoMapper(typeof(PlayerProfile).Assembly);
            services.AddMediatR(typeof(CreateNewGameCommand).Assembly);
            
            services.AddBaldaGameServices();

            services.AddCors();
            services.AddMvc(opt =>
            {
                opt.Filters.Add(typeof(ModelValidationFilter));
                opt.Filters.Add(typeof(GlobalExceptionFilter));
            })

                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
            .AddFluentValidation(cfg =>
            {
                cfg.RegisterValidatorsFromAssemblyContaining<UserLoginValidator>();
                cfg.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
            }); ;
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
                              IMediator mediator)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                app.UseHsts();

            app.UseCors(opt =>
                opt.AllowAnyHeader()
                .AllowAnyMethod()
                .WithOrigins("http://localhost:4200", "http://localhost:8084")
                .AllowCredentials());

            app.UseAuthentication();

            mediator.Send(new CreateDatabaseCommand()).Wait();

            app.UseOpenApi().UseSwaggerUi3();

            app.UseMvc();

        }
    }
}