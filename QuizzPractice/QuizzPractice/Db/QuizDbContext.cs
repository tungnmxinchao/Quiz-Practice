using Microsoft.EntityFrameworkCore;
using QuizzPractice.Db.Models;

namespace QuizzPractice.Db
{
    public class QuizDbContext : DbContext
    {
        public QuizDbContext(DbContextOptions<QuizDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<Quiz> Quizzes { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Option> Options { get; set; }
        public DbSet<Result> Results { get; set; }
        public DbSet<Answer> Answers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
         
            modelBuilder.Entity<User>()
                .Property(u => u.Role)
                .HasConversion<string>()
                .HasMaxLength(10)
                .HasDefaultValue("student");
            modelBuilder.Entity<User>()
                .Property(u => u.Status)
                .HasConversion<string>()
                .HasMaxLength(10)
                .HasDefaultValue("active");

       
            modelBuilder.Entity<Subject>()
                .Property(s => s.Status)
                .HasConversion<string>()
                .HasMaxLength(10)
                .HasDefaultValue("active");

      
            modelBuilder.Entity<Quiz>()
                .Property(q => q.Status)
                .HasConversion<string>()
                .HasMaxLength(10)
                .HasDefaultValue("active");

         
            modelBuilder.Entity<Question>()
                .Property(q => q.QuestionType)
                .HasConversion<string>()
                .HasMaxLength(20);
            modelBuilder.Entity<Question>()
                .Property(q => q.Level)
                .HasConversion<string>()
                .HasMaxLength(10);
            modelBuilder.Entity<Question>()
                .Property(q => q.Status)
                .HasConversion<string>()
                .HasMaxLength(10)
                .HasDefaultValue("active");

     
            modelBuilder.Entity<Option>()
                .Property(o => o.Status)
                .HasConversion<string>()
                .HasMaxLength(10)
                .HasDefaultValue("active");

            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict; 
            }
        }


    }
}
