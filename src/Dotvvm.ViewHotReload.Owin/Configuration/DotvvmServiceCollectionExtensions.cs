using Dotvvm.ViewHotReload;
using Dotvvm.ViewHotReload.Owin.Services;
using DotVVM.Framework.Hosting;
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
            services.Services.AddSingleton<IMarkupFileChangeNotifier, OwinMarkupFileChangeNotifier>();
            services.Services.AddSingleton<IMarkupFileLoader, HotReloadAggregateMarkupFileLoader>();
        }

    }
}
