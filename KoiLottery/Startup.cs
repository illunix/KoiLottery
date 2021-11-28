using FluentValidation.AspNetCore;
using KoiLottery.Infrastructure.Behaviors;
using KoiLottery.Infrastructure.Data;
using KoiLottery.Infrastructure.Filters;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace KoiLottery
{
    public partial class Startup
    {
        private readonly IConfiguration _configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews(options =>
            {
                options.Filters
                    .Add(typeof(ValidatorActionFilter));
            })
                .AddFeatureFolders()
                .AddFluentValidation(options =>
                    options.RegisterValidatorsFromAssembly(typeof(Program).Assembly));

            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(_configuration["ef:connectionString"]));

            services
                .AddMediatR(typeof(Startup))
                .AddTransient(
                    typeof(IPipelineBehavior<,>),
                    typeof(LoggingBehavior<,>)
                );
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
