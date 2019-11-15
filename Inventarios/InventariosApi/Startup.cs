using InventariosApi.Application;
using InventariosApi.Data;
using InventariosApi.Helper;
using InventariosApi.Model;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace InventariosApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<InventarioContext>(options => options.UseSqlServer(Configuration.GetConnectionString("ConectionString")));

            var appSettigs = Configuration.GetSection("Configuration").Get<Configuration>();
            services.AddSession();
            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_0)
                .AddXmlSerializerFormatters();
            services.AddMediatR(); //MediatR supports two kinds of messages: Request/Response and Notification.
            services.AddTransient<IInventarioService, IInventarioService>();
            services.AddCors();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            var appSettigs = Configuration.GetSection("Configuration").Get<Configuration>();
            app.UseSession();
            app.UseMiddleware(typeof(ErrorMiddleware), appSettigs.FileDir);
            app.UseHsts();

            app.UseCors(builder =>
            {
                builder.AllowAnyHeader();
                builder.AllowAnyMethod();
                builder.AllowAnyOrigin();
            });
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
