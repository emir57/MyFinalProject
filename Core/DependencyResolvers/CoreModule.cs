using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Core.CrossCuttingConcerns.Caching.Redis;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace Core.DependencyResolvers
{
    public class CoreModule : ICoreModule
    {
        public void Load(IServiceCollection serviceCollection)
        {
            serviceCollection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            serviceCollection.AddSingleton<Stopwatch>();

            serviceCollection.AddSingleton<RedisServer>();
            serviceCollection.AddMemoryCache();
            serviceCollection.AddSingleton<ICacheManager, MemoryCacheManager>();
            //serviceCollection.AddSingleton<ICacheManager, RedisCacheManager>();

        }
    }
}
