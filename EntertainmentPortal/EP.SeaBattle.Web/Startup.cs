using EP.SeaBattle.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using EP.SeaBattle.Logic.Commands;
using EP.SeaBattle.Logic.Profiles;
using AutoMapper;
using FluentValidation.AspNetCore;
using EP.SeaBattle.Logic.Validators;
using Microsoft.AspNetCore.Authentication.Cookies;

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
            services.AddAuthenticationCore();
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie();

            services.AddAuthorization(opt =>
            {
                opt.AddPolicy("hobby",
                    cfg => cfg.RequireAuthenticatedUser()
                        .RequireClaim("hobby"));
                opt.AddPolicy("google",
                    cfg => cfg.AddAuthenticationSchemes("Google")
                        .RequireAuthenticatedUser());
            });

            services.AddMemoryCache();

            services.AddDistributedMemoryCache();
            services.AddSession();



            services.AddLogging(cfg => cfg.AddConsole().AddDebug());
            services.AddSeaBattleServices();
            services.AddMediatR(typeof(AddNewPlayerCommand).Assembly);
            services.AddAutoMapper(typeof(CellProfile).Assembly);
            services.AddSwaggerDocument();
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
                app.UseSwagger().UseSwaggerUi3();
            }

            app.UseAuthentication();
            mediator.Send(new CreateDatabaseCommand()).Wait();
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseSession();
            app.UseMvc();
        }
    }
}
