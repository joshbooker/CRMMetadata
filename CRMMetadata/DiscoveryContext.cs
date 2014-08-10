using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Office365.OAuth;

namespace CRMMetadata
{
    public sealed class DiscoveryContext : DiscoveryContext<FixedSessionCache>
  {
    public DiscoveryContext()
      : this(DiscoveryHelper.DiscoverAppIdentity())
    {
    }

    public DiscoveryContext(AppIdentity appIdentity)
      : base(appIdentity)
    {
      if (HttpContext.Current.Session == null)
        throw new Exception("Session Required");
    }

        public static async Task<DiscoveryContext> CreateAsync()
        {
            await Task.Run((Action)(() => { }));
            return new DiscoveryContext();
        }


  }
}