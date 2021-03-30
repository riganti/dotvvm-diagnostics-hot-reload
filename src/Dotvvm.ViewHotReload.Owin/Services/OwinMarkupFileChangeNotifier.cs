using Dotvvm.ViewHotReload.Owin.Hubs;
using Microsoft.AspNet.SignalR;
using System.Collections.Generic;

namespace Dotvvm.ViewHotReload.Owin.Services
{
    public class OwinMarkupFileChangeNotifier : IMarkupFileChangeNotifier
    {
        public void NotifyFileChanged(IEnumerable<string> virtualPaths)
        {
            var context = GlobalHost.ConnectionManager.GetHubContext<DotvvmViewHotReloadHub>();
            DotvvmViewHotReloadHub.NotifyFileChanged(context, virtualPaths);
        }
    }
}