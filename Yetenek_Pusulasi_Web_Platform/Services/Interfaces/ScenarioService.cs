using Microsoft.EntityFrameworkCore;
using Yetenek_Pusulasi_Web_Platform.Data;
using Yetenek_Pusulasi_Web_Platform.Models.Entities;
using Yetenek_Pusulasi_Web_Platform.Models.ViewModels;

namespace Yetenek_Pusulasi_Web_Platform.Services.Interfaces
{
    public class ScenarioService : IScenarioService
    {
        private readonly ApplicationDbContext _context;

        public ScenarioService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Scenario>> GetAllScenariosAsync()
        {
            return await _context.Scenarios.ToListAsync();
        }

        public async Task<Scenario> GetScenarioByIdAsync(int id)
        {
            return await _context.Scenarios.FindAsync(id);
        }

        public async Task<bool> SubmitStudentAnswerAsync(ScenarioAnswerViewModel viewModel, string userId)
        {
            var scenario = await _context.Scenarios.FindAsync(viewModel.ScenarioId);
            if (scenario == null)
                return false;

            var answer = new StudentAnswer
            {
                ScenarioId = viewModel.ScenarioId,
                StudentId = userId,
                AnswerText = viewModel.AnswerText,
                SubmittedAt = DateTime.UtcNow
            };

            _context.StudentAnswers.Add(answer);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}