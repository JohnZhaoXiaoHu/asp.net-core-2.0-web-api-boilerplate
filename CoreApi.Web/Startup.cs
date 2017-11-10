using System.IO;
using System.Reflection;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using CoreApi.DataContext.Core;
using CoreApi.DataContext.Extensions;
using CoreApi.DataContext.Infrastructure;
using CoreApi.Repositories.Angular;
using CoreApi.Repositories.Core;
using CoreApi.Services.Core;
using CoreApi.Web.MyConfigurations;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using SharedSettings.Settings;
using Swashbuckle.AspNetCore.Swagger;

namespace CoreApi.Web
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
            services.AddDbContext<CoreContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddMvc(options =>
            {
                options.OutputFormatters.Remove(new XmlDataContractSerializerOutputFormatter());
            });

            // AutoMapper Auto Scan this Assembly for Configuration Files
            services.AddAutoMapper();

            services.AddScoped<IUnitOfWork, CoreContext>();
            services.AddScoped(typeof(ICoreService<>), typeof(CoreService<>));
            services.AddScoped<IUploadedFileRepository, UploadedFileRepository>();
            services.AddScoped<IClientRepository, ClientRepository>();

            //FileProvider
            var physicalProvider = new PhysicalFileProvider(Directory.GetCurrentDirectory());
            var embeddedProvider = new EmbeddedFileProvider(Assembly.GetEntryAssembly());
            var compositeProvider = new CompositeFileProvider(physicalProvider, embeddedProvider);
            services.AddSingleton<IFileProvider>(compositeProvider);

            // Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = CoreApiSettings.CoreApiResource.DisplayName, Version = "v1" });
            });

            // Identity Server
            services.AddMvcCore()
                .AddAuthorization()
                .AddJsonFormatters();

            services.AddAuthentication("Bearer")
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = AuthorizationServerSettings.AuthorizationServerBase;
                    options.RequireHttpsMetadata = false;

                    options.ApiName = CoreApiSettings.CoreApiResource.Name;
                });

            services.AddCors(options =>
            {
                // this defines a CORS policy called "default"
                options.AddPolicy(CoreApiSettings.CorsPolicyName, policy =>
                {
                    policy.WithOrigins(CoreApiSettings.CorsOrigin)
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, CoreContext coreContext)
        {
            app.UseCors(CoreApiSettings.CorsPolicyName);

            app.UseStaticFiles();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler();
            }

            app.UseStatusCodePages();
            
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Core APIs V1");
            });
            
            // Config Serilog
            app.ConfigureSerilog(Configuration);
            
            // Identity Server
            app.UseAuthentication();

            app.UseMvc();

        }
    }
}
