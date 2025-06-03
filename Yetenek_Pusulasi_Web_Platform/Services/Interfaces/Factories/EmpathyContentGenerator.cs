using Yetenek_Pusulasi_Web_Platform.Services.Interfaces;

namespace Yetenek_Pusulasi_Web_Platform.Services.Interfaces.Factories
{
    public class EmpathyContentGenerator : IScenarioContentGenerator
    {
        public string GetDefaultDescription() => "Bu senaryo, empati kurma becerilerinizi değerlendirmeyi amaçlar.";

        public string GenerateContent(dynamic parameters = null)
        {
            return "Yakın bir arkadaşınız önemli bir sınavda başarısız oldu ve çok üzgün. Ona nasıl yaklaşırsınız ve ne söylersiniz?";
        }
    }
}