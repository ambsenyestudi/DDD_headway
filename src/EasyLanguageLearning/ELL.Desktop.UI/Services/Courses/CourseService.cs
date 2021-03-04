using ELL.Desktop.UI.Models;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ELL.Desktop.UI.Services.Courses
{
    public class CourseService : ICourseService
    {

        //private readonly string url= "https://localhost:5001/api/Courses?motherLanguageIso=en&learningLanguageIso=fr";
        private readonly HttpClient client;
        private readonly CourseSettings settings;

        public CourseService(HttpClient client, IOptions<CourseSettings> options)
        {
            this.client = client;
            settings = options.Value;
        }
        //todo work on not default params
        private string BuildUrl(CourseSettings settings)
        {
            var urlBuilder = new StringBuilder(settings.BaseUrl)
                 .Append($"/{settings.GetMethod}");
            foreach (var item in settings.ParameterDefaults)
            {
                var urlPart = item.Key == settings.ParameterDefaults.First().Key
                    ? "?" : "&";
                urlBuilder.Append($"{urlPart}{item.Key}={item.Value}");
            }
            return urlBuilder.ToString();
                
        }
        public async Task<List<Course>> GetCourses(Guid id)
        {
            //todo make call with path id
            var url = BuildUrl(settings);
            var response = await client.GetAsync(url);
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<List<Course>>(json);
            }
            return new List<Course>();
        }
    }
}
