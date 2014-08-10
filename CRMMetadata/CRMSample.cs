using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Office365.OAuth;

namespace CRMMetadata
{
    public static class CRMSample
    {
        static DiscoveryContext _discoveryContext;

        private static Uri ServiceEndpointUri = new Uri(
            "https://sjkpdev07.crm4.dynamics.com/XRMServices/2011/OrganizationData.svc/");

        private static string serviceEndpointTemplate = "https://{0}.crm4.dynamics.com/XRMServices/2011/OrganizationData.svc/";

        private static string ServiceResourceId = "Microsoft.CRM";

        public static async Task<string> GetMetadata(string tenantName)
        {
            var client = await EnsureClientCreated(tenantName);


            return await client.GetMetadata();
        }

        public static async Task<string> Workspace(string tenantName)
        {
            var client = await EnsureClientCreated(tenantName);


            return await client.GetWorkspace();
        }

        private static async Task<CRMClient> EnsureClientCreated(string tenantName)
        {
            if (_discoveryContext == null)
            {
                _discoveryContext = await DiscoveryContext.CreateAsync();
            }

            var dcr = await _discoveryContext.DiscoverResourceAsync(ServiceResourceId);

            var user = dcr.UserId;
            return new CRMClient(new Uri(string.Format(serviceEndpointTemplate,tenantName)), async () =>
            {
                var accesToken = (await _discoveryContext.AuthenticationContext.AcquireTokenByRefreshTokenAsync(new FixedSessionCache().Read("RefreshToken"), new Microsoft.IdentityModel.Clients.ActiveDirectory.ClientCredential(_discoveryContext.AppIdentity.ClientId, _discoveryContext.AppIdentity.ClientSecret), ServiceResourceId)).AccessToken;

                return accesToken;
            });
        }
    }
}