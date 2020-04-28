using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TaskApp.Data;
using TaskApp.Helpers;
using TaskApp.Models;

namespace TaskApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly ApiKeySettings _apiKeySettings;
        private readonly HelperArticle _api = new HelperArticle();

        public SearchController(IOptions<ApiKeySettings> apiKeySettings, DataContext context)
        {
            _context = context;
            _apiKeySettings = apiKeySettings.Value;
        }
        [HttpGet]
        [Route("SearchTask")]
        public async Task<IActionResult> SearchTask(string query)
        {
            var searches = new Searches
            {
                Query = query,
                SearchTime = DateTime.Now
            };

            _context.Searcheses.Add(searches);
            await _context.SaveChangesAsync();

            var articals = new List<Artical>();

            var examples = new Article.Example();

            HttpClient client = _api.Initial();

            var res = await client.GetAsync($"articlesearch.json?api-key={_apiKeySettings.ApiKey}&q={query}");
            if (res.IsSuccessStatusCode)
            {
                var result = res.Content.ReadAsStringAsync().Result;
                examples = JsonConvert.DeserializeObject<Article.Example>(result);

                foreach (var item in examples.Response.Documents)
                {
                    articals.Add(new Artical
                    {
                        Headline = item.Headline.Main,
                        Content = item.LeadParagraph,
                        Url = item.WebUrl,
                        PublishDate = item.PublishDate
                    });
                }
            }

            return Ok(articals);
        }

    }

    public class Artical
    {
        public string Headline { get; set; }
        public string Content { get; set; }
        public string Url { get; set; }
        public DateTime PublishDate { get; set; }

    }
}