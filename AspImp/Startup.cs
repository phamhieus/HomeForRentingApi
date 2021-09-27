using AspImp.Extensions;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using NLog;

using System.IO;

namespace AspImp
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.ConfigureCors();
      services.ConfigureModelError();
      services.ConfigureIISIntegration();
      services.ConfigureSqlContext(Configuration);
      services.ConfigureRepositoryManager();
      services.ConfigureJWT(Configuration);
      services.ConfigureLoggerService();
      services.ConfigureIdentity();
      services.ConfigureAuth();

      services.AddControllers();
      services.AddAuthentication();
      services.AddHttpContextAccessor();
      services.AddAutoMapper(typeof(Startup));
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseHttpsRedirection();
      app.UseStaticFiles();

      app.UseRouting();

      app.UseAuthentication();
      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
