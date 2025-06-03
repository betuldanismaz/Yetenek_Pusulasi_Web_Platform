using Yetenek_Pusulasi_Web_Platform.Services.Interfaces;

namespace Yetenek_Pusulasi_Web_Platform.Services.Interfaces.Factories
{
    public class ProblemSolvingContentGenerator : IScenarioContentGenerator
    {
        public string GetDefaultDescription() => "Bu senaryo, problem çözme yeteneklerinizi ölçmeyi hedefler.";

        public string GenerateContent(dynamic parameters = null)
        {
            // Burada daha karmaşık, parametrelere göre içerik üretilebilir.
            // Örnek: parameters.difficultyLevel, parameters.topic
            return "Karşınızda çözülmesi gereken karmaşık bir problem var. Adım adım nasıl bir çözüm yolu izlersiniz?";
        }
    }
}