using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Ajax.Utilities;

namespace CRMMetadata
{
    class CRMClient
    {

        private Func<Task<string>> _accessTokenGetter;
        private object _syncLock = new object();

        private string _accessToken;
        private Uri _serviceRoot;

        public CRMClient(Uri serviceRoot, System.Func<Task<string>> accessTokenGetter)
        {
            _accessTokenGetter = accessTokenGetter;
            _serviceRoot = serviceRoot;

        }

        private async Task SetToken()
        {
            string token = await this._accessTokenGetter();
            lock (this._syncLock)
                this._accessToken = token;
        }

        public async Task<string> GetWorkspace()
        {
            await SetToken();
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, _serviceRoot);
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer",_accessToken);


            var res = await client.SendAsync(request).ConfigureAwait(true);
            return await res.Content.ReadAsStringAsync();
        }

        public async Task<string> GetMetadata()
        {
            await SetToken();
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Get, _serviceRoot + "/$metadata");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _accessToken);


            var res = await client.SendAsync(request).ConfigureAwait(true);
            return await res.Content.ReadAsStringAsync();
        }
    }
}
