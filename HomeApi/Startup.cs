using FluentValidation.AspNetCore;
using HomeApi.Configuration;
using HomeApi.MappingProfiles;
using HomeApi.Contracts.Validators.Device;
using Microsoft.OpenApi.Models;
using System.Reflection;
using FluentValidation;
using HomeApi.Data.Repos;
using HomeApi.Data;
using Microsoft.EntityFrameworkCore;

namespace HomeApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Загрузка конфигурации из файла json
        /// </summary>
        private IConfiguration Configuration { get; }
        //{ get; } = new ConfigurationBuilder()
        //    .AddJsonFile("appsettings.json")
        //    .AddJsonFile("appsettings.Development.json")
        //    .AddJsonFile("HomeOptions.json")
        //    .Build();

        public void ConfigureServices(IServiceCollection services)
        {
            //Регистрация сервиса репозитория для взаимодействия с базой данных
            services.AddSingleton<IDeviceRepository, DeviceRepository>();
            services.AddSingleton<IRoomRepository, RoomRepository>();

            string connect = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<HomeApiContext>(options => options.UseSqlServer(connect), ServiceLifetime.Singleton);

            //Подключение FluentValidation
            services.AddValidatorsFromAssemblyContaining<AddDeviceRequestValidator>()
                .AddFluentValidationAutoValidation();

            //Подключаем автомаппер
            var assembly = Assembly.GetAssembly(typeof(MappingProfile));
            services.AddAutoMapper(assembly);

            //Добавляем новый сервис для опций (IOption<HomeOptions>)
            services.Configure<HomeOptions>(Configuration);
            services.Configure<HomeOptions>(opt =>      //Переопределение свойства, указанного в json-файле
            {
                opt.Area = 120;
            });
            services.Configure<Address>(Configuration.GetSection("Address"));   //Получаем только адрес (вложенный json-объект), создание на его основе TOptions

            // Нам не нужны представления, но в MVC бы здесь стояло AddControllersWithViews()
            services.AddControllers();
            // поддерживает автоматическую генерацию документации WebApi с использованием Swagger
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

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //Достаём значение из свойства, вложенного в несколько объектов json-файла
            string str = Configuration.GetSection("Logging").GetSection("LogLevel").GetValue<string>("Default");

            if (env.IsDevelopment()) 
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HomeApi v1"));
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
