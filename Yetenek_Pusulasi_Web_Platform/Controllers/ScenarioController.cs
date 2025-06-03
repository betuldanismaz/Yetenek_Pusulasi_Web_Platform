using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using Yetenek_Pusulasi_Web_Platform.Models.ViewModels;
using Yetenek_Pusulasi_Web_Platform.Services.Interfaces; // Servis arayüzümüz
// using Yetenek_Pusulasi_Web_Platform.Data; // Artık DbContext'e doğrudan erişim yok

namespace Yetenek_Pusulasi_Web_Platform.Controllers
{
    public class ScenarioController : Controller
    {
        private readonly IScenarioService _scenarioService; // DbContext yerine servis

        public ScenarioController(IScenarioService scenarioService) // Servisi enjekte et
        {
            _scenarioService = scenarioService;
        }

        // GET: Scenario
        public async Task<IActionResult> Index()
        {
            var scenarios = await _scenarioService.GetAllScenariosAsync();
            return View(scenarios);
        }

        // GET: Scenario/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var scenario = await _scenarioService.GetScenarioByIdAsync(id.Value);
            if (scenario == null)
            {
                return NotFound();
            }

            var viewModel = new ScenarioDetailsViewModel
            {
                Scenario = scenario,
                AnswerViewModel = new ScenarioAnswerViewModel { ScenarioId = id.Value }
            };

            return View(viewModel);
        }

        // POST: Scenario/SubmitAnswer
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> SubmitAnswer(ScenarioAnswerViewModel viewModel)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Challenge(); // Giriş yapmaya zorla
            }

            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Lütfen formdaki hataları düzeltin.";
                // Model geçerli değilse, kullanıcıyı senaryo detay sayfasına geri gönder.
                // Sayfanın ihtiyaç duyduğu Scenario modelini tekrar yüklememiz gerekiyor.
                var scenario = await _scenarioService.GetScenarioByIdAsync(viewModel.ScenarioId);
                if (scenario == null)
                {
                    return NotFound("İlgili senaryo bulunamadı.");
                }

                var detailsViewModel = new ScenarioDetailsViewModel
                {
                    Scenario = scenario,
                    AnswerViewModel = viewModel
                };

                return View("Details", detailsViewModel);
            }

            bool submissionSuccessful = await _scenarioService.SubmitStudentAnswerAsync(viewModel, userId);

            if (submissionSuccessful)
            {
                TempData["SuccessMessage"] = "Cevabınız başarıyla gönderildi!";
                return RedirectToAction("Details", new { id = viewModel.ScenarioId });
            }
            else
            {
                // Genellikle SubmitStudentAnswerAsync içinde hata yönetimi yapılır
                // ve buraya özel bir hata mesajı iletilir.
                TempData["ErrorMessage"] = "Cevabınız gönderilirken bir sorun oluştu veya senaryo bulunamadı.";
                var scenario = await _scenarioService.GetScenarioByIdAsync(viewModel.ScenarioId);
                if (scenario == null)
                {
                    return NotFound("İlgili senaryo bulunamadı.");
                }

                var detailsViewModel = new ScenarioDetailsViewModel
                {
                    Scenario = scenario,
                    AnswerViewModel = viewModel
                };

                return View("Details", detailsViewModel);
            }
        }
    }
}