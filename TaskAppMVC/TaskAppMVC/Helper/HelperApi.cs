using System;
using System.Net.Http;

namespace TaskAppMVC.Helper
{
    public class HelperApi
    {
        public HttpClient Initial()
        {
            var Client = new HttpClient();
            Client.BaseAddress = new Uri("https://localhost:44324/");
            return Client;
        }
    }
}
