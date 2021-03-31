using DotVVM.Diagnostics.ViewHotReload.AspNetCore.Hubs;
using Microsoft.AspNetCore.Builder;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ApplicationBuilderExtensions
    {

        public static void UseDotvvmViewHotReload(this IApplicationBuilder app)
        {
            app.UseSignalR(builder =>
            {
                builder.MapHub<DotvvmViewHotReloadHub>("/_diagnostics/dotvvmViewHotReloadHub");
            });
        }

    }
}
