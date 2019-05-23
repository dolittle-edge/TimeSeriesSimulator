/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Dolittle. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using Autofac;
using Dolittle.AspNetCore.Bootstrap;
using Dolittle.Booting;
using Dolittle.DependencyInversion.Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;

namespace Core
{
    /// <summary>
    /// ASP.NET Core Startup class
    /// </summary>
    public partial class Startup
    {
        readonly IHostingEnvironment _hostingEnvironment;
        readonly ILoggerFactory _loggerFactory;
        BootloaderResult _bootResult;

        /// <summary>
        /// Initializes a new instance <see cref="Startup"/>
        /// </summary>
        /// <param name="hostingEnvironment"></param>
        /// <param name="loggerFactory"></param>
        public Startup(IHostingEnvironment hostingEnvironment, ILoggerFactory loggerFactory)
        {
            _hostingEnvironment = hostingEnvironment;
            _loggerFactory = loggerFactory;
        }

        /// <summary>
        /// Configure the services for ASP.NET Core
        /// </summary>
        /// <param name="services"><see cref="IServiceCollection"/> to configure</param>
        public void ConfigureServices(IServiceCollection services)
        {
            if (_hostingEnvironment.IsDevelopment())
            {
                services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new Info { Title = "My API", Version = "v1" });
                });
            }
            services.AddMvc();

            _bootResult = services.AddDolittle(_loggerFactory);
        }

        /// <summary>
        /// Configure the Autofac Container
        /// </summary>
        /// <param name="containerBuilder"></param>
        public void ConfigureContainer(ContainerBuilder containerBuilder)
        {
            containerBuilder.AddDolittle(_bootResult.Assemblies, _bootResult.Bindings);
        }

        /// <summary>
        /// Configure ASP.NET Core
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                });

                app.UseCors(_ => {
                    _.AllowCredentials();
                    _.AllowAnyMethod();
                    _.AllowAnyOrigin();
                    _.AllowAnyHeader();
                });
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseMvc();


            app.UseDolittle();
            app.RunAsSinglePageApplication();
        }

    }
}