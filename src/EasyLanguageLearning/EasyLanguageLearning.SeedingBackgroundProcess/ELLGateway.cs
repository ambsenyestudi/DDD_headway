using EasyLanguageLearning.Application.LanguageCatalog;
using EasyLanguageLearning.Application.LearningPaths;
using EasyLanguageLearning.Domain.LanguageCatalogs.Aggregate;
using EasyLanguageLearning.Domain.LearningPaths.Aggregate;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace EasyLanguageLearning.SeedingBackgroundProcess
{
    public class ELLGateway
    {
        //https://localhost:5001/api/Ready
        public const string READY_ENDPOINT = "Ready";
        public const string LEARNING_PATH_ENDPOINT = "LearningPath";
        public const string LANGUAGE_CATALOG_ENDPOINT = "Catalog";

        private readonly HttpClient client;

        public ELLGateway(HttpClient client)
        {
            this.client = client;
        }
        
        public async Task GetReadyState()
        {
            var response = await client.GetAsync(READY_ENDPOINT);
            response.EnsureSuccessStatusCode();
        }
        public async Task<bool> GetLearningPath()
        {
            var response = await client.GetAsync(LEARNING_PATH_ENDPOINT+"?iso=en");
            response.EnsureSuccessStatusCode();
            var jsonContent = await response.Content.ReadAsStringAsync();
            var paths = JsonConvert.DeserializeObject<object[]>(jsonContent);
            var isEmpty = !paths.Any();
            return isEmpty;
        }

        public async Task PostCatalog(LearningLanguageDTO lang)
        {
            var json = JsonConvert.SerializeObject(lang);

            var response = await PostAsync(LANGUAGE_CATALOG_ENDPOINT, json);
            response.EnsureSuccessStatusCode();
        }

        

        public async Task PostLeaningPath(LearningPathDTO leaningPath)
        {
            var json = JsonConvert.SerializeObject(leaningPath);

            var response = await PostAsync(LEARNING_PATH_ENDPOINT, json);
            response.EnsureSuccessStatusCode();
        }

        private async Task<HttpResponseMessage> PostAsync(string enpoint, string json)
        {
            StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
            return await client.PostAsync(enpoint, content);
        }

        public string ComposeEnpdpoints(params string[] endpointList) =>
            string.Join("/", endpointList);
    }
}
