using Microsoft.AspNetCore.Identity; // IdentityUser için
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Yetenek_Pusulasi_Web_Platform.Models.Entities
{
    public class StudentAnswer
    {
        public int Id { get; set; }

        [Required]
        public string AnswerText { get; set; } // Öğrencinin verdiği yazılı cevap

        public DateTime SubmittedAt { get; set; } = DateTime.UtcNow;

        // Foreign Key for Scenario
        public int ScenarioId { get; set; }
        [ForeignKey("ScenarioId")]
        public virtual Scenario? Scenario { get; set; } // Navigation property

        // Foreign Key for Student (IdentityUser)
        [Required]
        public string StudentId { get; set; }
        [ForeignKey("StudentId")]
        public virtual IdentityUser? Student { get; set; } // Navigation property
    }
}