using System;
using System.Collections.Generic;
using System.IO;
using AutoMapper;
using EP.Sudoku.Logic;
using EP.Sudoku.Logic.Commands;
using EP.Sudoku.Logic.Models;
using EP.Sudoku.Logic.Profiles;
using EP.Sudoku.Logic.Queries;
using EP.Sudoku.Logic.Services;
using EP.Sudoku.Logic.Validators;
using EP.Sudoku.Web.Filters;
using EP.Sudoku.Web.Hubs;
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
using Serilog;
using NSwag;
using NSwag.AspNetCore;

namespace EP.Sudoku.Web
{
    /// <summary>
    /// This class uses by the runtime. Used to configure services.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Used to configure services.
        /// </summary>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Represents a set of key/value application configuration properties.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie()
                .AddIdentityServerAuthentication(JwtBearerDefaults.AuthenticationScheme, opt =>
                {
                    opt.Authority = "https://localhost:44366/";
                    opt.RequireHttpsMetadata = true;
                })
                //.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
                //{
                //    opt.RequireHttpsMetadata = true;
                //    opt.TokenValidationParameters = new TokenValidationParameters()
                //    {
                //        ValidIssuer = SudokuConstants.ISSUER_NAME,
                //        ValidateIssuer = true,
                //        ValidAudience = SudokuConstants.AUDIENCE_NAME,
                //        ValidateAudience = true,
                //        ValidateIssuerSigningKey = true,
                //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SudokuConstants.SECRET)),
                //        ValidateLifetime = true,
                //        RequireExpirationTime = true,
                //        RequireSignedTokens = true
                //    };
                //})
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
                    googleOptions.ClientId = "274043152218-ud0e3mc8lv0lm4bdjn67kfsudv5j4p0h.apps.googleusercontent.com";
                    googleOptions.ClientSecret = "qQMR0R_keZgQHPbhhe8Tirdq";
                }); ;
            services.AddAuthorization();
            services.AddMediatR(typeof(GetAllPlayers).Assembly);
            services.AddAutoMapper(typeof(PlayerProfile).Assembly);
            services.AddSwaggerDocument(cfg =>
            {
                //cfg.SchemaType = SchemaType.OpenApi3;
                cfg.SchemaType = SchemaType.Swagger2;
                cfg.Title = "Sudoku game";
                cfg.AddSecurity("oauth", new[] { "sudoku_api" }, new OpenApiSecurityScheme()
                {
                    Flow = OpenApiOAuth2Flow.Implicit,
                    Type = OpenApiSecuritySchemeType.OAuth2,
                    AuthorizationUrl = "https://localhost:44366/connect/authorize",
                    Scopes = new Dictionary<string, string>()
                    {
                        {"sudoku_api", "Access to sudoku game api" }
                    }
                });
            });

            services.AddSudokuServices();            
            services.AddLogging();
            services.AddMemoryCache();
            services.Configure<EmailSettings>(Configuration.GetSection("EmailSettings"));
            services.AddSingleton<IEmailSenderService, EmailSenderService>();
            //services.AddCors(options => options.AddPolicy("CorsPolicy",
            //builder =>
            //{
            //    builder.AllowAnyHeader()
            //        .AllowAnyMethod()
            //        .WithOrigins("http://localhost:4200")
            //        .AllowCredentials();
            //}));
            services.AddCors();
            services.AddSignalR();
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
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
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

            //app.UseCors("CorsPolicy");
            app.UseCors(opt =>
                opt.AllowAnyHeader()
                    .AllowAnyMethod()
                    .WithOrigins("http://localhost:4200")
                    .AllowCredentials());

            app.UseAuthentication();

            app.UseOpenApi().UseSwaggerUi3(opt => opt.OAuth2Client = new OAuth2ClientSettings()
            {
                AppName = "Sudoku game",
                ClientId = "swagger"
            });

            mediator.Send(new CreateDatabaseCommand()).Wait();
            app.UseSignalR(cfg =>
            {
                cfg.MapHub<SudokuHub>("/sudoku");
            });
            app.UseMvc();

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Console()
                .WriteTo.Debug()
                .WriteTo.RollingFile("SudokuLogData/log-{Date}.txt", shared: true)
                .WriteTo.SQLite(Path.Combine(Environment.CurrentDirectory, @"..\EP.Sudoku.Data\sudoku.db"))                
                .CreateLogger();
        }
    }
}

