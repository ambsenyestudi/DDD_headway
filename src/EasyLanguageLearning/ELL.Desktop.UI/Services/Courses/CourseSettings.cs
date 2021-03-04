using System.Collections.Generic;

namespace ELL.Desktop.UI.Services.Courses
{
    public class CourseSettings
    {
        public string BaseUrl { get; set; }
        public string GetMethod { get; set; }
        public Dictionary<string,string> ParameterDefaults { get; set; }
    }
}
