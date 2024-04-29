using ETicaretAPI.Application.Abstraction.Storage;
using ETicaretAPI.Application.Token;
using ETicaretAPI.Infrasracture.Services;
using ETicaretAPI.Infrasracture.Services.Storage;
using ETicaretAPI.Infrasracture.Services.Token;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Infrasracture
{
    public static class ServiceRegistration
    {
        public static void AddInfrastractureServices(this IServiceCollection services)
        {
            services.AddScoped<IStorageService,StorageService>();
            services.AddScoped<ITokenHandler,TokenHandler>();

        }

        public static void AddStorage<T>(this IServiceCollection services) where T : Storage,IStorage
        {
            services.AddScoped<IStorage,T>();
        }
    }
}
