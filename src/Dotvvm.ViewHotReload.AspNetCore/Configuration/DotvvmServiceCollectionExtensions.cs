using Dotvvm.ViewHotReload;
using Dotvvm.ViewHotReload.Owin.Services;
using DotVVM.Framework.Hosting;
using DotVVM.Framework.ResourceManagement;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotVVM.Framework.Configuration
{
    public static class DotvvmServiceCollectionExtensions
    {

        public static void AddViewHotReload(this IDotvvmServiceCollection services)
        {
            services.Services.AddSignalR();

            services.Services.AddSingleton<IMarkupFileChangeNotifier, AspNetCoreMarkupFileChangeNotifier>();
            services.Services.AddSingleton<IMarkupFileLoader, HotReloadAggregateMarkupFileLoader>();

            services.Services.Configure<DotvvmConfiguration>(RegisterResources);
            services.Services.AddTransient<ResourceManager>(provider =>
            {
                var manager = new ResourceManager(provider.GetRequiredService<DotvvmResourceRepository>());
                manager.AddRequiredResource("dotvvm-viewhotreload");
                return manager;
            });
        }

        private static void RegisterResources(DotvvmConfiguration config)
        {
            if (config.Resources.FindResource("signalr") == null)
            {
                config.Resources.Register("signalr", new ScriptResource(new UrlResourceLocation("https://www.unpkg.com/@microsoft/signalr@5.0.4/dist/browser/signalr.min.js")));
            }

            config.Resources.Register("dotvvm-viewhotreload", new ScriptResource(new EmbeddedResourceLocation(typeof(DotvvmServiceCollectionExtensions).Assembly, "Dotvvm.ViewHotReload.AspNetCore.Scripts.dotvvm.viewhotreload.js"))
            {
                Dependencies = new[] { "signalr", "dotvvm" }
            });
        }
    }
}
