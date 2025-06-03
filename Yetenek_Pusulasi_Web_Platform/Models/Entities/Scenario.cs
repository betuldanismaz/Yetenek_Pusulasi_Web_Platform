using System;
using System.ComponentModel.DataAnnotations;

namespace Yetenek_Pusulasi_Web_Platform.Models.Entities
{
    public enum ScenarioType
    {
        Generic,
        ProblemSolving,
        Empathy,
        AnalyticalThinking,
        CreativeWriting
    }

    public class Scenario
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; } = string.Empty;

        [StringLength(500)]
        public string? Description { get; set; }

        [Required]
        public string Content { get; set; } = string.Empty;

        public ScenarioType Type { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public virtual ICollection<StudentAnswer>? StudentAnswers { get; set; }
    }
}