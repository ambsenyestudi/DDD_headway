using ELL.Desktop.UI.Models;
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
        private const string url = "https://localhost:5001/api/Courses?motherLanguageIso=en&learningLanguageIso=fr";
        private readonly HttpClient client;

        public CourseService(HttpClient client)
        {
            this.client = client;
        }
        public async Task<List<Course>> GetCourses(Guid id)
        {
            //todo make call with path id
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
