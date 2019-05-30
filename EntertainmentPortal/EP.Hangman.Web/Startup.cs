using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EP.Hagman.Data;
using EP.Hangman.Logic.Queries;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using MediatR;
using NJsonSchema;
using AutoMapper;
using EP.Hangman.Logic.Profiles;

namespace EP.Hangman.Web
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
            services.AddSwaggerDocument(conf => conf.SchemaType = SchemaType.OpenApi3);
            services.AddMediatR(typeof(GetHangman).Assembly);
            services.AddMediatR(typeof(PutHangman).Assembly);
            services.AddMediatR(typeof(PostHangman).Assembly);
            services.AddMediatR(typeof(SetWord).Assembly);
            services.AddAutoMapper(typeof(HangmanDataResponseProfile).Assembly);
            services.AddSingleton(typeof(HangmanTemporaryData));
            services.AddSingleton(typeof(HangmanWordsData));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger().UseSwaggerUi3();
            app.UseMvc();
        }
    }
}
