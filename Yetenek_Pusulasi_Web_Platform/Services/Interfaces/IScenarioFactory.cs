using Yetenek_Pusulasi_Web_Platform.Models.Entities;

namespace Yetenek_Pusulasi_Web_Platform.Services.Interfaces
{
    public interface IScenarioFactory
    {
        Scenario CreateScenario(ScenarioType type, string title, dynamic? contentParameters = null);
    }
}