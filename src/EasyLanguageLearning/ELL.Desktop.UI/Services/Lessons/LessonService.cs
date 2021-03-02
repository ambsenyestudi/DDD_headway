using ELL.Desktop.UI.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ELL.Desktop.UI.Services.Lessons
{
    public class LessonService : ILessonService
    {
        private const string urlTemplate = "https://localhost:5001/api/Lessons?courseId{0}";
        private readonly HttpClient client;
        public LessonService(HttpClient client)
        {
            this.client = client;
        }
        public async Task<List<Lesson>> GetLessons(Guid courseId)
        {
            var url = "https://localhost:5001/api/Lessons?courseId=90b94554-5e8e-4406-9e4b-c1b90fd4cb2c";//string.Format(urlTemplate, courseId);
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Lesson>>(json);
            }
            return new List<Lesson>();
        }
    }
}
