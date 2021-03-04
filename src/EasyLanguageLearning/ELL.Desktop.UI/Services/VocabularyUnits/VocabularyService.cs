using ELL.Desktop.UI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace ELL.Desktop.UI.Services.VocabularyUnits
{
    public class VocabularyService : IVocabularyService
    {
        private const string urlTemplate = "https://localhost:5001/api/LessonContent?lessonId{0}";
        private readonly HttpClient client;
        public VocabularyService(HttpClient client)
        {
            this.client = client;
        }
        public async Task<List<Vocabulary>> GetVocabulary(Guid lessonId)
        {
            var url = "https://localhost:5001/api/LessonContent?lessonId=fasñdlfkasdjñflkasd"; ;//string.Format(urlTemplate, lessonId);
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Vocabulary>>(json);
            }
            return new List<Vocabulary>();
        }
    }
}
