using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Shawarma.AspNetCore;
using Shawarma.AspNetCore.Hosting;

namespace Shawarma.CSharp.Example;

public class Startup(IConfiguration configuration)
{
    public IConfiguration Configuration { get; } = configuration;

    // This method gets called by the runtime. Use this method to add services to the container.
    public static void ConfigureServices(IServiceCollection services)
    {
        services.AddRazorPages();

        services
            .AddShawarmaHosting()
            .AddShawarmaService<BackgroundService>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Error");
        }

        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapShawarma();
            endpoints.MapRazorPages();
        });
    }
}
