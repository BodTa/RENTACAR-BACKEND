using Core.CrossCuttingConcerns.Caching;
using Core.CrossCuttingConcerns.Caching.Microsoft;
using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

namespace Core.IoC
{
    public class CoreModule : ICoreModule
    {
        //Burası instance olaylarımızı yaptığımız alan. Sıralama Apı ==> Business ==> DataAccess==> Entities
        // olarak gittiği için bu alanı önemli olan yerlere ekleme yapmak için kullanıyoruz.
        // İşlevini yapıyor reis.
        public void Load(IServiceCollection serviceCollection)
        {
            serviceCollection.AddMemoryCache();
            serviceCollection.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            serviceCollection.AddSingleton<ICacheManager, MemoryCacheManager>();
            serviceCollection.AddSingleton<Stopwatch>();
        }
    }
}
