using ELL.Desktop.UI.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ELL.Desktop.UI.Services.Paths
{
    public class PathService : IPathService
    {
        private const string DEFAULT_ISO = "en";
        private readonly string url;
        private readonly HttpClient client;
        private readonly PathServiceSettings settings;

        public PathService(HttpClient client, IOptions<PathServiceSettings> options)
        {
            this.client = client;
            settings = options.Value;
            url = BuildUrl(settings);
        }
        private string BuildUrl(PathServiceSettings settings) =>
            new StringBuilder(settings.BaseUrl)
                .Append($"/{settings.GetMethod}")
                .Append($"?iso={DEFAULT_ISO}").ToString();
        
        public async Task<List<LearningPath>> GetPaths()
        {
            var response = await client.GetAsync(url);
            if(response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<LearningPath>>(json);
            }
            return new List<LearningPath>();
        }
    }
}
