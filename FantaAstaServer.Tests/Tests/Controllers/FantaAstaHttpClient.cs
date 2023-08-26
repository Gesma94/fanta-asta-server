using FantaAstaServer.Tests.Sources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace FantaAstaServer.Tests.Tests.Controllers
{
    internal class FantaAstaHttpClientFactory : IDisposable
    {
        private FantaAstaApplicationFactory _factory = new();

        public HttpClient Create()
        {
            return _factory.CreateClient();
        }

        public void Dispose()
        {
            _factory.Dispose();
        }
    }

    internal class FantaAstaHttpClientWrapper : IDisposable
    {
        private FantaAstaApplicationFactory _factory = new();


        public FantaAstaHttpClientWrapper()
        {
            Client = _factory.CreateClient();
        }
        
        public void Dispose()
        {
            Client.Dispose();
            _factory.Dispose();
        }


        public static FantaAstaHttpClientWrapper New()
        {
            return new FantaAstaHttpClientWrapper();
        }


        public HttpClient Client { get; }    
    }
}
