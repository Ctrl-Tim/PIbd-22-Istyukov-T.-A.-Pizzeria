using PizzeriaBusinessLogic.BusinessLogics;
using PizzeriaContracts.BusinessLogicsContracts;
using PizzeriaContracts.StoragesContracts;
using PizzeriaDatabaseImplement.Implements;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace PizzeriaRestApi
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
            services.AddTransient<IClientStorage, ClientStorage>();
            services.AddTransient<IOrderStorage, OrderStorage>();
            services.AddTransient<IPizzaStorage, PizzaStorage>();
            services.AddTransient<IStorageStorage, StorageStorage>();
            services.AddTransient<IIngredientStorage, IngredientStorage>();

            services.AddTransient<IClientLogic, ClientLogic>();
            services.AddTransient<IOrderLogic, OrderLogic>();
            services.AddTransient<IPizzaLogic, PizzaLogic>();
            services.AddTransient<IStorageLogic, StorageLogic>();
            services.AddTransient<IIngredientLogic, IngredientLogic>();

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PizzeriaRestApi", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PizzeriaRestApi v1"));
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
