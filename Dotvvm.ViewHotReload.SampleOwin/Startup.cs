using System;
using System.Web.Hosting;
using Microsoft.Owin;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.StaticFiles;
using Owin;
using DotVVM.Framework;
using DotVVM.Framework.Configuration;
using DotVVM.Framework.Routing;
using DotVVM.Framework.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Dotvvm.ViewHotReload.Owin;

[assembly: OwinStartup(typeof(Dotvvm.ViewHotReload.SampleOwin.Startup))]
namespace Dotvvm.ViewHotReload.SampleOwin
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var applicationPhysicalPath = HostingEnvironment.ApplicationPhysicalPath;

            // use DotVVM
            var dotvvmConfiguration = app.UseDotVVM<DotvvmStartup>(applicationPhysicalPath, debug: IsInDebugMode());
            dotvvmConfiguration.AssertConfigurationIsValid();

            app.UseDotvvmViewHotReload();

            // use static files
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileSystem = new PhysicalFileSystem(applicationPhysicalPath)
            });
        }

		private bool IsInDebugMode()
        {
#if !DEBUG
			return false;
#endif
            return true;
        }
    }
}
