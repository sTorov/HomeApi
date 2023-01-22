using FluentValidation;
using FluentValidation.AspNetCore;
using HomeApi.Configuration;
using HomeApi.MappingProfiles;
using HomeApi.Contracts.Validators.Device;
using HomeApi.Data;
using HomeApi.Data.Repos;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace HomeApi
{
    public class Startup
    {
        private IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        
        /// <summary>
        /// Подключение и настройка сервисов
        /// </summary>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IDeviceRepository, DeviceRepository>();
            services.AddSingleton<IRoomRepository, RoomRepository>();

            string connect = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<HomeApiContext>(options => options.UseSqlServer(connect), ServiceLifetime.Singleton);

            services.AddValidatorsFromAssemblyContaining<AddDeviceRequestValidator>()
                .AddFluentValidationAutoValidation();
            
            var assembly = Assembly.GetAssembly(typeof(MappingProfile));
            services.AddAutoMapper(assembly);
            
            services.Configure<HomeOptions>(Configuration);
            services.Configure<Address>(Configuration.GetSection("Address"));

            services.AddControllers();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo()
                {
                    Version = "v1",
                    Title = "HomeApi",
                    Description = "HomeApi description"
                });
            });
        }

        /// <summary>
        /// Настройка приложения
        /// </summary>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) 
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HomeApi v1"));
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}
