using Microsoft.EntityFrameworkCore;
using VacancyManagement.Domain;

namespace VacancyManagement.Infrastructure
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext() { }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<Vacancy> Vacancies { get; set; }
        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<AnswerOption> AnswerOptions { get; set; }
        public DbSet<CandidateAnswers> CandidateAnswers { get; set; }
        public DbSet<FileEntity> FileEntity { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Vacancy>().HasKey(v => v.Id);
            modelBuilder.Entity<Candidate>().HasKey(c => c.Id);

            // Define the one-to-many relationship between Question and AnswerOption
            modelBuilder.Entity<Question>()
                .HasMany(q => q.Options)
                .WithOne(a => a.Question)
                .HasForeignKey(a => a.QuestionId)
                .OnDelete(DeleteBehavior.Cascade);

            // Define the CorrectOptionId as a foreign key if necessary
            modelBuilder.Entity<Question>()
                .HasOne<AnswerOption>()
                .WithMany() // If CorrectOption doesn't have a back-navigation property
                .HasForeignKey(q => q.CorrectOptionId)
                .OnDelete(DeleteBehavior.Restrict);

            // CandidateAnswers relationship with Candidate
            modelBuilder.Entity<CandidateAnswers>()
               .HasOne(ca => ca.Candidate)
               .WithMany(c => c.CandidateAnswers) // Explicit navigation back to Candidate
               .HasForeignKey(ca => ca.CandidateId)
               .OnDelete(DeleteBehavior.Restrict);

            // CandidateAnswers relationship with Question
            modelBuilder.Entity<CandidateAnswers>()
                .HasOne(ca => ca.Question)
                .WithMany(q => q.CandidateAnswers)  // Explicit navigation back to Question
                .HasForeignKey(ca => ca.QuestionId)
                .OnDelete(DeleteBehavior.Restrict);

            // CandidateAnswers relationship with AnswerOption
            modelBuilder.Entity<CandidateAnswers>()
                .HasOne(ca => ca.AnswerOption)
                .WithMany(ao => ao.CandidateAnswers) // Explicit navigation back to AnswerOption
                .HasForeignKey(ca => ca.AnswerOptionId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }
    }
}
