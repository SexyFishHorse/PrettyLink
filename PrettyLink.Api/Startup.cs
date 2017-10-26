﻿namespace PrettyLink.Api
{
    using System.IO;
    using JetBrains.Annotations;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.PlatformAbstractions;
    using Swashbuckle.AspNetCore.Swagger;

    [UsedImplicitly(ImplicitUseTargetFlags.WithMembers)]
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(
                setup =>
                    {
                        setup.SwaggerEndpoint("/swagger/v1/swagger.json", "PrettyLink API V1");
                    });

            app.UseMvc();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            services.AddSwaggerGen(
                setup =>
                    {
                        setup.SwaggerDoc("v1", new Info { Title = "PrettyLink API V1", Version = "1.0" });
                        setup.IncludeXmlComments(
                            Path.Combine(PlatformServices.Default.Application.ApplicationBasePath, "PrettyLink.Api.xml"));
                    });
        }
    }
}