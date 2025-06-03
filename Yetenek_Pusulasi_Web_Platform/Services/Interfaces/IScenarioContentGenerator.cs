namespace Yetenek_Pusulasi_Web_Platform.Services.Interfaces
{
    public interface IScenarioContentGenerator
    {
        string GenerateContent(dynamic? parameters = null);
        string GetDefaultDescription();
    }
}
