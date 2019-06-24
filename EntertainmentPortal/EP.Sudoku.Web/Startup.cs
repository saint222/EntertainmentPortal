using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using EP.Sudoku.Logic;
using EP.Sudoku.Logic.Commands;
using EP.Sudoku.Logic.Profiles;
using EP.Sudoku.Logic.Queries;
using EP.Sudoku.Logic.Validators;
using EP.Sudoku.Web.Filters;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using NJsonSchema;
using Serilog;
using Serilog.Events;

namespace EP.Sudoku.Web
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
            services.AddMediatR(typeof(GetAllPlayers).Assembly);            
            services.AddAutoMapper(typeof(PlayerProfile).Assembly);            
            services.AddSwaggerDocument();
            services.AddSudokuServices();
            services.AddCors(); // to enable CrossOriginResourceSharing
            services.AddMvc(opt =>
            {
                opt.Filters.Clear();
                opt.Filters.Add(typeof(GlobalExceptionFilter));

            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddFluentValidation(cfg =>
                {
                    cfg.RegisterValidatorsFromAssemblyContaining<SetCellValueValidator>();
                    cfg.RunDefaultMvcValidationAfterFluentValidationExecutes = false;
                });
            services.AddLogging();
            services.AddMemoryCache();
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseCors(opt => opt.AllowAnyHeader() // CORS configuration.
                .AllowAnyMethod()
                .AllowAnyOrigin());

            mediator.Send(new CreateDatabaseCommand()).Wait();
            app.UseSwagger().UseSwaggerUi3();            
            app.UseMvc();

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Console()
                .WriteTo.Debug()
                .WriteTo.RollingFile("SudokuLogData/log-{Date}.txt", shared:true)
                .WriteTo.SQLite(Environment.CurrentDirectory + @"\sudoku.db")
                .CreateLogger();
        }
    }
}

