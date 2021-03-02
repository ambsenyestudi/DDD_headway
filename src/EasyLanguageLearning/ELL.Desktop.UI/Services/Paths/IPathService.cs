using ELL.Desktop.UI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ELL.Desktop.UI.Services.Paths
{
    public interface IPathService
    {
        Task<List<LearningPath>> GetPaths();
    }
}
