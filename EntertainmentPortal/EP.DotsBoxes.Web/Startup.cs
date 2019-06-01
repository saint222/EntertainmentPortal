using AutoMapper;
using EP.DotsBoxes.Logic;
using EP.DotsBoxes.Logic.Commands;
using EP.DotsBoxes.Logic.Profiles;
using EP.DotsBoxes.Logic.Queries;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NJsonSchema;

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
            services.AddSwaggerDocument(cfg => cfg.SchemaType = SchemaType.OpenApi3);
            services.AddMediatR(typeof(GetAllPlayers).Assembly);
            services.AddMediatR(typeof(GetGameBoard).Assembly);
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile(new PlayerProfile());
                cfg.AddProfile(new GameBoardProfile());
            });
            services.AddPlayerServices();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IMediator mediator)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
          
            mediator.Send(new CreateDatabaseCommand()).Wait();
            app.UseSwagger().UseSwaggerUi3();
            app.UseMvc();
        }
    }
}
