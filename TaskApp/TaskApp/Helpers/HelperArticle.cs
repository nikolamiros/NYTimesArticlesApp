using System;
using System.Net.Http;

namespace TaskApp.Helpers
{
    public class HelperArticle
    {
        public HttpClient Initial()
        {
            var Client = new HttpClient();
            Client.BaseAddress = new Uri("https://api.nytimes.com/svc/search/v2/");
            return Client;
        }
    }
}
