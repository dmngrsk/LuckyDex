using LuckyDex.Api.Interfaces.Repositories;
using LuckyDex.Api.Models.AppSettings;
using LuckyDex.Api.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LuckyDex.Api
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            var dataStorage = Configuration.GetSection("DataStorage").Get<DataStorageSettings>();
            services.AddTransient(s => dataStorage.TableStorage);

            services.AddSingleton<ITrainerRelationshipRepository, TableStorageTrainerRelationshipRepository>();
            services.AddSingleton<IPokémonRelationshipRepository, TableStoragePokémonRelationshipRepository>();
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
