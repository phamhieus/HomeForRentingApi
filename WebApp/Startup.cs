using AspImp.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NLog;
using System.IO;
using Microsoft.AspNetCore.HttpOverrides;
using System.Net;
using System;
using AspImp.Services;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.ResponseCompression;
using System.Linq;
using Lib.AspNetCore.ServerSentEvents;
using AspImp.Services.Interfaces;
using AspImp.Services.Impliments;

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

      services.Configure<ForwardedHeadersOptions>(options =>
      {
        options.KnownProxies.Add(IPAddress.Parse("10.0.0.100"));
      });

      services.Configure<FormOptions>(options =>
      {
        options.MemoryBufferThreshold = Int32.MaxValue;
      });

      // Register default ServerSentEventsService.
      services.AddServerSentEvents();
      // Registers custom ServerSentEventsService which will be used by second middleware, otherwise they would end up sharing connected users.
      services.AddServerSentEvents<IServerSentEventsService, ServerSentEventsService>(options =>
      {
        options.ReconnectInterval = 5000;
      });
      services.AddResponseCompression(options =>
      {
        options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "text/event-stream" });
      });

      services.ConfigureSqlContext(Configuration);
      services.ConfigureRepositoryManager();
      services.ConfigureJWT(Configuration);
      services.ConfigureLoggerService();
      services.ConfigureIdentity();
      services.ConfigureSwagger();
      services.ConfigureAuth();
      services.ConfigureSSE();

      services.AddControllers();
      services.AddAuthentication();
      services.AddHttpContextAccessor();
      services.AddSingleton<FileService>();

      services.AddAutoMapper(typeof(Startup));
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }

      app.UseForwardedHeaders(new ForwardedHeadersOptions
      {
        ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
      });

      app.UseCors(x => x
         .AllowAnyOrigin()
         .AllowAnyMethod()
         .AllowAnyHeader());
      
      app.UseResponseCompression();
      app.UseHttpsRedirection();
      app.UseStaticFiles();
      app.UseRouting();

      app.UseAuthentication();
      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
        endpoints.MapServerSentEvents<ServerSentEventsService>("/notifications");
      });

      app.UseSwagger();
      app.UseSwaggerUI();
    }
  }
}
