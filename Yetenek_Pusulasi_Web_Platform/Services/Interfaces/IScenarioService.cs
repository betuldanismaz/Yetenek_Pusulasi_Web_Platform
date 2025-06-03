using Yetenek_Pusulasi_Web_Platform.Models.Entities;
using Yetenek_Pusulasi_Web_Platform.Models.ViewModels;

namespace Yetenek_Pusulasi_Web_Platform.Services.Interfaces
{
    public interface IScenarioService
    {
        Task<IEnumerable<Scenario>> GetAllScenariosAsync();
        Task<Scenario> GetScenarioByIdAsync(int id);
        Task<bool> SubmitStudentAnswerAsync(ScenarioAnswerViewModel viewModel, string userId);
    }
}