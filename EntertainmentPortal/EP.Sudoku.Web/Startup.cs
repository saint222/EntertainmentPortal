using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EP.Sudoku.Logic;
using EP.Sudoku.Logic.Commands;
using EP.Sudoku.Logic.Models;
using EP.Sudoku.Logic.Profiles;
using EP.Sudoku.Logic.Queries;
using EP.Sudoku.Logic.Services;
using EP.Sudoku.Logic.Validators;
using EP.Sudoku.Web.Controllers;
using EP.Sudoku.Web.Filters;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
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
            services.Configure<IdentityOptions>(opt => 
            {
                opt.Password.RequireNonAlphanumeric = false;                
            });
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie()
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
                {
                    opt.RequireHttpsMetadata = true;
                    opt.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidIssuer = SudokuConstants.ISSUER_NAME,
                        ValidateIssuer = true,
                        ValidAudience = SudokuConstants.AUDIENCE_NAME,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SudokuConstants.SECRET)),
                        ValidateLifetime = true,
                        RequireExpirationTime = true,
                        RequireSignedTokens = true
                    };
                })
                .AddFacebook("Facebook",
                facebookOptions =>
                {
                    facebookOptions.CallbackPath = new PathString("/api/signInFacebook");
                    //facebookOptions.AppId = Configuration["Authentication:Facebook:AppId"];
                    //facebookOptions.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
                    facebookOptions.ClientId = "692105217894763";
                    facebookOptions.ClientSecret = "82669c46d46a697d7c967d96bc2c7ceb";
                })
                .AddGoogle("Google",
                googleOptions =>
                {
                    googleOptions.CallbackPath = new PathString("/api/google");
                    //facebookOptions.AppId = Configuration["Authentication:Facebook:AppId"];
                    //facebookOptions.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
                    googleOptions.ClientId = "274043152218-ud0e3mc8lv0lm4bdjn67kfsudv5j4p0h.apps.googleusercontent.com";
                    googleOptions.ClientSecret = "qQMR0R_keZgQHPbhhe8Tirdq";
                }); ;
            services.AddAuthorization();
            services.AddMediatR(typeof(GetAllPlayers).Assembly);
            services.AddAutoMapper(typeof(PlayerProfile).Assembly);
            services.AddSwaggerDocument(conf => conf.SchemaType = SchemaType.OpenApi3);
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
            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));
            services.AddSingleton<IEmailSenderService, EmailSenderService>();
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
            app.UseAuthentication();
            app.UseCors(opt => opt.AllowAnyHeader() // CORS configuration.
               .AllowAnyMethod()
               .AllowAnyOrigin()
               .WithOrigins("https://localhost:4200")
               .AllowCredentials()
);

            mediator.Send(new CreateDatabaseCommand()).Wait();
            app.UseSwagger().UseSwaggerUi3();            
            app.UseMvc();

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Console()
                .WriteTo.Debug()
                .WriteTo.RollingFile("SudokuLogData/log-{Date}.txt", shared: true)
                .WriteTo.SQLite(Environment.CurrentDirectory + @"\sudoku.db")
                .CreateLogger();
        }
    }
}

