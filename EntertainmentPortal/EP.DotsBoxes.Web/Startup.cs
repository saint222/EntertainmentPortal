using AutoMapper;
using EP.DotsBoxes.Logic;
using EP.DotsBoxes.Logic.Commands;
using EP.DotsBoxes.Logic.Profiles;
using EP.DotsBoxes.Logic.Queries;
using EP.DotsBoxes.Logic.Validators;
using EP.DotsBoxes.Web.Filters;
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
using Microsoft.IdentityModel.Tokens;
using NJsonSchema;
using System.Text;

namespace EP.DotsBoxes.Web
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
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
                 {
                     opt.RequireHttpsMetadata = true;                     
                     opt.TokenValidationParameters = new TokenValidationParameters()
                     {
                         ValidIssuer = DotsBoxesConstants.ISSUER_NAME,
                         ValidateIssuer = true,
                         ValidAudience = DotsBoxesConstants.AUDIENCE_NAME,
                         ValidateAudience = true,
                         ValidateIssuerSigningKey = true,
                         IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(DotsBoxesConstants.SECRET)),
                         ValidateLifetime = true,
                         RequireExpirationTime = true,
                         RequireSignedTokens = true
                     };
                 })
                .AddGoogle("Google", opt =>
                {
                    opt.CallbackPath = new PathString("/api/google");
                    opt.ClientId = "1097896526158-k9dr1a3jppmunnu0ehg88l9ve5jibrdm.apps.googleusercontent.com";               
                    opt.ClientSecret = "xTjiJQXW9xmqLfwQtF_IfzgA";
                });

            services.AddAuthorization(opt => 
                 {
                     opt.AddPolicy("google",
                    cfg => cfg.AddAuthenticationSchemes("Google")
                        .RequireAuthenticatedUser());
                 });

            services.AddSwaggerDocument(cfg => cfg.SchemaType = SchemaType.OpenApi3);
            services.AddSwaggerDocument(cfg =>
            {
                cfg.SchemaType = SchemaType.OpenApi3;
                cfg.Version = "v1";
                cfg.Title = "Dots and Boxes game";
                cfg.Description = "ASP.NET Core Web API";
            });
            services.AddMediatR(typeof(GetAllPlayers).Assembly);
            services.AddMediatR(typeof(GetGameBoard).Assembly);
            services.AddAutoMapper(typeof(PlayerProfile).Assembly);
            services.AddAutoMapper(typeof(GameBoardProfile).Assembly);
            services.CreateGameBoardServices();
            services.AddCors(); // To enable CrossOriginResourceSharing.
            services.AddMvc(opt =>
            {
                opt.Filters.Add(typeof(GlobalExceptionFilter));
            })
               .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddFluentValidation(cfg =>
                {
                    cfg.RegisterValidatorsFromAssemblyContaining<AddPlayerValidator>();
                    cfg.RegisterValidatorsFromAssemblyContaining<NewGameBoardValidator>();
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


            // CORS configuration.
            app.UseCors(opt =>
                opt.AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin());
            app.UseAuthentication();           

            mediator.Send(new CreateDatabaseCommand()).Wait();
            app.UseSwagger().UseSwaggerUi3();
            app.UseMvc();
        }
    }
}
