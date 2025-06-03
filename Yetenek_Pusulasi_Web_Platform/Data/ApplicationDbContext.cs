using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Yetenek_Pusulasi_Web_Platform.Models.Entities; // Yeni eklediğimiz namespace

namespace Yetenek_Pusulasi_Web_Platform.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        // Yeni DbSet'lerimizi ekliyoruz
        public DbSet<Scenario> Scenarios { get; set; } = null!;
        public DbSet<StudentAnswer> StudentAnswers { get; set; } = null!;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder); // Identity için gerekli konfigürasyonları korur

            // Burada entity'lerimiz için özel konfigürasyonlar ekleyebiliriz (isteğe bağlı)
            // Örneğin, Scenario ve StudentAnswer arasındaki ilişkiyi açıkça belirtmek:

            // Scenario ile StudentAnswers arasındaki bire-çok ilişki zaten
            // navigation property'ler ve ForeignKey attribute'ları ile kuruldu,
            // ancak daha karmaşık senaryolarda Fluent API kullanılabilir.
            // Şimdilik bu kadarı yeterli.

            builder.Entity<StudentAnswer>()
                .HasOne(sa => sa.Scenario)
                .WithMany(s => s.StudentAnswers)
                .HasForeignKey(sa => sa.ScenarioId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.Entity<StudentAnswer>()
                .HasOne(sa => sa.Student)
                .WithMany()
                .HasForeignKey(sa => sa.StudentId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}