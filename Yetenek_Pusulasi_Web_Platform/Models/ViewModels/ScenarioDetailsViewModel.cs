using Yetenek_Pusulasi_Web_Platform.Models.Entities;

namespace Yetenek_Pusulasi_Web_Platform.Models.ViewModels
{
    public class ScenarioDetailsViewModel
    {
        public Scenario Scenario { get; set; } = null!;
        public ScenarioAnswerViewModel AnswerViewModel { get; set; } = null!;
    }
} 