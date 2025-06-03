using System.ComponentModel.DataAnnotations;

namespace Yetenek_Pusulasi_Web_Platform.Models.ViewModels
{
    public class ScenarioAnswerViewModel
    {
        [Required]
        public int ScenarioId { get; set; }

        [Required(ErrorMessage = "Lütfen cevabınızı yazınız.")]
        [StringLength(2000, MinimumLength = 10, ErrorMessage = "Cevabınız en az 10, en fazla 2000 karakter olmalıdır.")]
        public string AnswerText { get; set; }

        // İleride senaryo başlığı gibi ek bilgileri de View'e taşımak için buraya ekleyebiliriz
        // public string ScenarioTitle { get; set; }
    }
}