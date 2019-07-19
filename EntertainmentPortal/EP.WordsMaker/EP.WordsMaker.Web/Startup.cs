using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MediatR;
using EP.WordsMaker.Logic.Queries;
using AutoMapper;
using EP.WordsMaker.Logic.Commands;
using EP.WordsMaker.Logic.Extensions;
using EP.WordsMaker.Logic.Profiles;
using NJsonSchema;
using CSharpFunctionalExtensions;
using EP.WordsMaker.Logic.Validators;
using EP.WordsMaker.Web.Constants;
using EP.WordsMaker.Web.Filters;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using NSwag;
using NSwag.AspNetCore;

namespace EP.WordsMaker.Web
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
			services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
				.AddCookie()
				//.AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
				//{
				//	opt.RequireHttpsMetadata = true;
				//	opt.TokenValidationParameters = new TokenValidationParameters()
				//	{
				//		ValidIssuer = WordsMakerConstants.ISSUER_NAME,
				//		ValidateIssuer = true,
				//		ValidAudience = WordsMakerConstants.AUDIENCE_NAME,
				//		ValidateAudience = true,
				//		ValidateIssuerSigningKey = true,
				//		IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(WordsMakerConstants.SECRET)),
				//		ValidateLifetime = true,
				//		RequireExpirationTime = true,
				//		RequireSignedTokens = true
				//	};
				//})
                .AddIdentityServerAuthentication(JwtBearerDefaults.AuthenticationScheme, opt =>
                    {
                        opt.Authority = "http://localhost:5000";
                        opt.RequireHttpsMetadata = false;
                    }
                  )
				.AddFacebook("Facebook", opt =>
					{
						opt.CallbackPath = new PathString("/api/facebook");
						opt.ClientId = "689466208162671";
						opt.ClientSecret = "024b114f793bdbda75839893e4e6c612";
					}
				);

			services.AddAuthorization(opt =>
				{
					opt.AddPolicy("admin", cfg => cfg.RequireAuthenticatedUser()
						.RequireClaim("admin"));
					opt.AddPolicy("user", cfg => cfg.RequireAuthenticatedUser()
						.RequireClaim("user"));
				}
			);

			services.AddMemoryCache();
			services.AddDistributedMemoryCache();
			services.AddSession();

            services.AddSwaggerDocument(cfg =>
            {
                cfg.SchemaType = SchemaType.OpenApi3;
                cfg.AddSecurity("oauth", new[] { "wordsmaker_api" }, new OpenApiSecurityScheme()
                {
                    Flow = OpenApiOAuth2Flow.Implicit,
                    Type = OpenApiSecuritySchemeType.OAuth2,
                    AuthorizationUrl = "http://localhost:5000/connect/authorize",
                    Scopes = new Dictionary<string, string>()
                    {
                        {"wordsmaker_api", "Access to wordsmaker api"}
                    }
                });
            });
            services.AddMediatR(typeof(GetAllPlayers).Assembly);
            services.AddCors();
            services.AddMediatR(typeof(GetAllGames).Assembly);
			services.AddAutoMapper(typeof(PlayerProfile).Assembly);
            services.AddAutoMapper(typeof(GameProfile).Assembly);
			//services.AddAutoMapper(cfg => cfg.AddProfile(new PlayerProfile()));
			services.AddPlayerServices();
            services.AddMvc(opt => opt.Filters.Add(typeof(GlobalExceptionFilter))).SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .AddFluentValidation(cfg =>
                {
                    cfg.RegisterValidatorsFromAssemblyContaining<AddNewPlayerValidator>();
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
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCors(opt => opt.AllowAnyHeader() // CORS configuration.
                .AllowAnyMethod()
                .WithOrigins("http://localhost:4200")
                .AllowCredentials());

            app.UseAuthentication();

            mediator.Send(new CreateDatabaseCommand()).Wait();
            app.UseOpenApi().UseSwaggerUi3(opt => opt.OAuth2Client = new OAuth2ClientSettings()
            {
                AppName = "WordsMaker",
                ClientId = "swagger"
            });
            app.UseSession();
            app.UseMvc();
		}
	}
}
