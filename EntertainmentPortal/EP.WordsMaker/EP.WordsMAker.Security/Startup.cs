// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using EP.WordsMaker.Security.Data;
using EP.WordsMaker.Security.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using IdentityServer4;

namespace EP.WordsMaker.Security
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IHostingEnvironment Environment { get; }

        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            services.AddMvc().SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_1);

            services.Configure<IISOptions>(iis =>
            {
                iis.AuthenticationDisplayName = "Windows";
                iis.AutomaticAuthentication = false;
            });

            var builder = services.AddIdentityServer(options =>
            {
                options.Events.RaiseErrorEvents = true;
                options.Events.RaiseInformationEvents = true;
                options.Events.RaiseFailureEvents = true;
                options.Events.RaiseSuccessEvents = true;
            })
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddInMemoryApiResources(Config.GetApis())
                .AddInMemoryClients(Config.GetClients())
                .AddAspNetIdentity<ApplicationUser>();

            if (Environment.IsDevelopment())
            {
                builder.AddDeveloperSigningCredential();
            }
            else
            {
                throw new Exception("need to configure key material");
            }

            services.AddAuthentication()
			   .AddGoogle("Google",options =>
			   {
				   //options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

				   // register your IdentityServer with Google at https://console.developers.google.com
				   // enable the Google+ API
				   // set the redirect URI to http://localhost:5000/signin-google
				   options.ClientId = "445562339895-20etr500bnd7bsb5e2tprm2e6a8hp1o6.apps.googleusercontent.com";
				   options.ClientSecret = "HY52KtIcpdgdaFjKJPqvNgp5";
			   })
			   .AddFacebook(options =>
				   {
					   //options.SignInScheme = IdentityServerConstants.ExternalCookieAuthenticationScheme;

					   // register your IdentityServer with Google at https://console.developers.google.com
					   // enable the Google+ API
					   // set the redirect URI to http://localhost:5000/signin-google
					   options.ClientId = "689466208162671";
					   options.ClientSecret = "024b114f793bdbda75839893e4e6c612";
				   }
			   );

		}

		public void Configure(IApplicationBuilder app)
        {
            if (Environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseIdentityServer();
            app.UseMvcWithDefaultRoute();
        }
    }
}