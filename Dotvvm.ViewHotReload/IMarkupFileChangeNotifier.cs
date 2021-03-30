using System;
using System.Collections.Generic;
using System.Text;

namespace Dotvvm.ViewHotReload
{
    public interface IMarkupFileChangeNotifier
    {

        void NotifyFileChanged(IEnumerable<string> virtualPaths);

    }
}
