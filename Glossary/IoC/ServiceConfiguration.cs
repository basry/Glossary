
using Glossary.Models;
using Glossary.Repository.Glossary;
using Glossary.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Glossary.IoC
{
    public static class ServiceConfiguration
    {
        public static ServiceProvider Init(IConfiguration config)
        {
            var serviceCollection = new ServiceCollection();
            serviceCollection.ConfigureServices(config);
            return serviceCollection.BuildServiceProvider();
        }

        public static void ConfigureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var filepath = configuration["FilePath"];
            services.AddTransient<GlossaryStoreContext>(provider => new GlossaryStoreContext(filepath));
            services.AddTransient<IGlossaryRepository, GlossaryRepository>();
            services.AddTransient<IGlossaryService<GlossaryModel>, GlossaryService>();



        }
    }
}
