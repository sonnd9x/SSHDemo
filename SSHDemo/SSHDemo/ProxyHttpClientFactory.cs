using Flurl.Http.Configuration;
using MihaZupan;
using SocksSharp;
using SocksSharp.Proxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SSHDemo
{
    public class ProxyHttpClientFactory : DefaultHttpClientFactory
    {
        private string _address;
        private int _port;

        public ProxyHttpClientFactory(string address, int port)
        {
            _address = address;
            _port = port;
        }

        public override HttpMessageHandler CreateMessageHandler()
        {
            return new HttpClientHandler
            {
                Proxy = new HttpToSocks5Proxy(_address, _port),
                UseProxy = true
            };
        }
    }
}
