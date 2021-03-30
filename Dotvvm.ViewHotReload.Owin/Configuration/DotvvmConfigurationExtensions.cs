using Dotvvm.ViewHotReload.Owin;
using Dotvvm.ViewHotReload.Owin.Configuration;
using DotVVM.Framework.Configuration;
using DotVVM.Framework.ResourceManagement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotVVM.Framework.Configuration
{
    public static class DotvvmConfigurationExtensions
    {

        public static void AddViewHotReload(this DotvvmConfiguration config, DotvvmViewHotReloadOptions options = null)
        {
            if (options == null)
            {
                options = new DotvvmViewHotReloadOptions();
            }

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

            config.Resources.Register("dotvvm-viewhotreload", new ScriptResource(new EmbeddedResourceLocation(typeof(DotvvmConfigurationExtensions).Assembly, "Dotvvm.ViewHotReload.Owin.Scripts.dotvvm.viewhotreload.js"))
            {
                Dependencies = new[] { "signalr-hubs", "dotvvm" }
            });
        }
    }
}
