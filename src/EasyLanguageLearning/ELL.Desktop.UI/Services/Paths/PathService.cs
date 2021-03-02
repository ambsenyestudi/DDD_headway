using ELL.Desktop.UI.Models;
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
        private string url = "https://localhost:5001/api/LearningPath?iso=en";
        private readonly HttpClient client;

        public PathService(HttpClient client)
        {
            this.client = client;
        }
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
