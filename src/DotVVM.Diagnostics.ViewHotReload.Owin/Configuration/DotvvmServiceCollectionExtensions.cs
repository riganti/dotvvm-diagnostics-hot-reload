﻿using DotVVM.Diagnostics.ViewHotReload;
using DotVVM.Diagnostics.ViewHotReload.Owin.Configuration;
using DotVVM.Diagnostics.ViewHotReload.Owin.Services;
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

        public static void AddViewHotReload(this IDotvvmServiceCollection services, DotvvmViewHotReloadOptions options = null)
        {
            services.Services.AddSingleton<IMarkupFileChangeNotifier, OwinMarkupFileChangeNotifier>();
            services.Services.AddSingleton<IMarkupFileLoader, HotReloadAggregateMarkupFileLoader>();

            services.Services.Configure<DotvvmConfiguration>(config => RegisterResources(config, options ?? new DotvvmViewHotReloadOptions()));
            services.Services.AddTransient<ResourceManager>(provider =>
            {
                var manager = new ResourceManager(provider.GetRequiredService<DotvvmResourceRepository>());
                manager.AddRequiredResource("dotvvm-viewhotreload");
                return manager;
            });
        }

        private static void RegisterResources(DotvvmConfiguration config, DotvvmViewHotReloadOptions options)
        {
            if (config.Resources.FindResource("jquery") == null)
            {
                config.Resources.Register("jquery", new ScriptResource(new UrlResourceLocation("https://unpkg.com/jquery@3.6.0/dist/jquery.min.js")));
            }

            if (config.Resources.FindResource("signalr") == null)
            {
                config.Resources.Register("signalr", new ScriptResource(new UrlResourceLocation("https://unpkg.com/signalr@2.4.0/jquery.signalR.min.js"))
                {
                    Dependencies = new[] { "jquery" }
                });
            }

            if (options.RegisterSignalrHubs)
            {
                config.Resources.Register("signalr-hubs", new ScriptResource(new UrlResourceLocation("~/signalr/hubs"))
                {
                    Dependencies = new[] { "signalr" }
                });
            }

            config.Resources.Register("dotvvm-viewhotreload", new ScriptResource(new EmbeddedResourceLocation(typeof(DotvvmServiceCollectionExtensions).Assembly, "DotVVM.Diagnostics.ViewHotReload.Owin.Scripts.dotvvm.viewhotreload.js"))
            {
                Dependencies = new[] { "signalr-hubs", "dotvvm" }
            });
        }

    }
}
