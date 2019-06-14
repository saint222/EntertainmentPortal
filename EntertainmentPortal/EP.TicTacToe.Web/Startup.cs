using AutoMapper;
using EP.TicTacToe.Logic;
using EP.TicTacToe.Logic.Commands;
using EP.TicTacToe.Logic.Profiles;
using EP.TicTacToe.Logic.Queries;
using EP.TicTacToe.Logic.Validators;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NJsonSchema;

namespace EP.TicTacToe.Web
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
            services.AddAutoMapper(typeof(PlayerProfile).Assembly);
            services.AddPlayerServices();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
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

            mediator.Send(new CreateDatabaseCommand()).Wait();
            app.UseSwagger().UseSwaggerUi3();
            app.UseMvc();
        }
    }
}
