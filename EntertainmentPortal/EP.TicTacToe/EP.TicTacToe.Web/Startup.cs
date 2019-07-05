using System;
using AutoMapper;
using EP.TicTacToe.Logic.Commands;
using EP.TicTacToe.Logic.Profiles;
using EP.TicTacToe.Logic.Queries;
using EP.TicTacToe.Logic.Services;
using EP.TicTacToe.Logic.Validators;
using EP.TicTacToe.Web.Filters;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NJsonSchema;
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

            services.AddSwaggerDocument(cfg => cfg.SchemaType = SchemaType.OpenApi3);
            services.AddAutoMapper(typeof(PlayerProfile).Assembly);
            services.AddMediatR(typeof(GetAllPlayers).Assembly);
            services.AddGameServices();

            services.AddCors();

            services.AddMvc(opt =>
                {
                    opt.Filters.Clear();
                    opt.Filters.Add(typeof(GlobalExceptionFilter));
                })
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddFluentValidation(cfg =>
                {
                    cfg.RegisterValidatorsFromAssemblyContaining<AddNewPlayerValidator>();
                    cfg.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                });

            services.AddLogging();
            services.AddMemoryCache();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env,
                              IMediator mediator)
        {
            if (env.IsDevelopment())
                app.UseDeveloperExceptionPage();
            else
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();

            app.UseCors(opt =>
                opt.AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin()
                    .AllowCredentials());
            app.UseAuthentication();

            mediator.Send(new CreateDatabaseCommand()).Wait();
            app.UseSwagger().UseSwaggerUi3();
            app.UseSession();
            app.UseMvc();

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