using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using ImageAPI.Contexts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace ImageAPI
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
            services.AddDbContext<Context>(options =>
               options
                   .UseNpgsql(Configuration
                       .GetConnectionString("DefaultConnection") +
                            Environment.GetEnvironmentVariable("Connection", EnvironmentVariableTarget.Machine)));

            services.AddSingleton<IFileProvider>(
              new PhysicalFileProvider(
                  Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));

            services.AddMvc();

            services.Configure<IISOptions>(options =>
            {
                options.ForwardClientCertificate = true;
                options.AutomaticAuthentication = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseMvc();
        }
    }
}
