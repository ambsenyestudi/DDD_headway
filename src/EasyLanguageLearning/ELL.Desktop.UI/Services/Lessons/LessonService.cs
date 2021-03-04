using ELL.Desktop.UI.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ELL.Desktop.UI.Services.Lessons
{
    public class LessonService : ILessonService
    {
        private readonly HttpClient client;
        private readonly LessonSettings settings;

        public LessonService(HttpClient client, IOptions<LessonSettings> options)
        {
            this.client = client;
            settings = options.Value;            

        }
        public async Task<List<Lesson>> GetLessons(Guid courseId)
        {
            //https://localhost:5001/api/Lessons?courseId=90b94554-5e8e-4406-9e4b-c1b90fd4cb2c
            var url = BuildUrl(settings, courseId);//"https://localhost:5001/api/Lessons?courseId=90b94554-5e8e-4406-9e4b-c1b90fd4cb2c";//string.Format(urlTemplate, courseId);
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Lesson>>(json);
            }
            return new List<Lesson>();
        }
        private string BuildUrl(LessonSettings settings, Guid courseId) =>
            new StringBuilder(settings.BaseUrl)
                .Append($"/{settings.GetMethod}")
                .Append(ComposeParam(settings.ParameterTemplate, courseId))
                .ToString();

        private string ComposeParam(string template, Guid id, bool isOtherThanFirst = false) =>
            isOtherThanFirst ? ":" : "?" +
                string.Format(template, id.ToString());
    }
}
