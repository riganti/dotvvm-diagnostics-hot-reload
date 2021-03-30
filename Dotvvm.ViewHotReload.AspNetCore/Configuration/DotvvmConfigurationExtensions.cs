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

        public static void AddViewHotReload(this DotvvmConfiguration config)
        {
            if (config.Resources.FindResource("signalr") == null)
            {
                config.Resources.Register("signalr", new ScriptResource(new UrlResourceLocation("https://www.unpkg.com/@microsoft/signalr@5.0.4/dist/browser/signalr.min.js")));
            }

            config.Resources.Register("dotvvm-viewhotreload", new ScriptResource(new EmbeddedResourceLocation(typeof(DotvvmConfigurationExtensions).Assembly, "Dotvvm.ViewHotReload.AspNetCore.Scripts.dotvvm.viewhotreload.js"))
            {
                Dependencies = new[] { "signalr", "dotvvm" }
            });
        }
    }
}
