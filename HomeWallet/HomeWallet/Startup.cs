using AutoMapper;
using HomeWallet.Access;
using HomeWallet.Models.Maps;
using HomeWallet.Server.Extentions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using University.Web.Extensions;

namespace HomeWallet
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
            services.AddControllers().AddNewtonsoftJson();

            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new OperationMappingProfile());
                mc.AddProfile(new CategoryMappingProfile());
            });
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);

            var conString = Configuration.GetConnectionString("HWConnectionString");
            services.AddDbContext<HomeWalletDbContext>(options =>
                options.UseSqlServer(conString)
            );

            services.AddHomeWalletServises();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "HomeWallet", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            InitDatabase(app.ApplicationServices);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.ConfigureCustomExceptionMiddleware();

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HomeWallet v1"));

            app.UseCors(policy =>
                policy.AllowAnyMethod()
                      .AllowAnyOrigin()
                      .AllowAnyHeader()
                      .WithExposedHeaders("X-Pagination"));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void InitDatabase(IServiceProvider serviceProvider)
        {
            using IServiceScope serviceScope = serviceProvider.GetService<IServiceScopeFactory>().CreateScope();
            HomeWalletDbContext context = serviceScope.ServiceProvider.GetService<HomeWalletDbContext>();
            context.Database.Migrate();
        }
    }
}