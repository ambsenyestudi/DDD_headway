using EasyLanguageLearning.SeedingBackgroundProcess.Stores;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace EasyLanguageLearning.SeedingBackgroundProcess
{
    class Program
    {


        private static ELLService service = new ELLService(
            CreateGateway(),
            CreateLearningPathStore(), 
            CreateLanguageCatalogStore(),
            CreateVocabularyStore());


        public async static Task Main(string[] args)
        {
            Console.WriteLine("Seeding process ready");
            await service.WaitForReady();
            if(await service.IsSeedingNeeded())
            {
                Console.WriteLine("Seeding is needed");
                await service.SeedLearningPaths();
            }
            Console.WriteLine("Wanna quit?");
            Console.ReadLine();
        }
        private static VocabularyStore CreateVocabularyStore() =>
            new VocabularyStore();

        private static LearningPathStore CreateLearningPathStore() =>
            new LearningPathStore();
        private static LanguageCatalogStore CreateLanguageCatalogStore() =>
            new LanguageCatalogStore();
        private static ELLGateway CreateGateway() =>
            new ELLGateway(CreateClient());
        private static HttpClient CreateClient(string baseUrl = "https://localhost:5001/api/") =>
            new HttpClient { BaseAddress = new Uri(baseUrl) };
    }
}
