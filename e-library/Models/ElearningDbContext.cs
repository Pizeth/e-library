using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace e_library.Models;

public partial class ElearningDbContext : DbContext
{
    public ElearningDbContext()
    {
    }

    public ElearningDbContext(DbContextOptions<ElearningDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<BookChapter> BookChapters { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<ChapterDetail> ChapterDetails { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<CourseDetail> CourseDetails { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Professor> Professors { get; set; }

    public virtual DbSet<Quiz> Quizs { get; set; }

    public virtual DbSet<QuizAnswer> QuizAnswers { get; set; }

    public virtual DbSet<QuizQuestion> QuizQuestions { get; set; }

    public virtual DbSet<QuizTaker> QuizTakers { get; set; }

    public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

    public virtual DbSet<Response> Responses { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Type> Types { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ElearningDB");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Author>(entity =>
        {
            entity.ToTable("Author");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Avatar).HasMaxLength(500);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.PenName).HasMaxLength(50);
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.ToTable("Book");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AuthorId).HasColumnName("AuthorID");
            entity.Property(e => e.BookTitle)
                .HasMaxLength(250)
                .HasColumnName("Book_Title");
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.Cover)
                .HasMaxLength(500)
                .IsFixedLength();
            entity.Property(e => e.Description).HasMaxLength(250);
            entity.Property(e => e.GenreId).HasColumnName("GenreID");
            entity.Property(e => e.PublishedDate).HasColumnType("date");

            entity.HasOne(d => d.Author).WithMany(p => p.Books)
                .HasForeignKey(d => d.AuthorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Book_Author");

            entity.HasOne(d => d.Category).WithMany(p => p.Books)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("FK_Book_Category");

            entity.HasOne(d => d.Genre).WithMany(p => p.Books)
                .HasForeignKey(d => d.GenreId)
                .HasConstraintName("FK_Book_Genre");
        });

        modelBuilder.Entity<BookChapter>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Book_Chapter");

            entity.ToTable("BookChapter");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.BookId).HasColumnName("BookID");
            entity.Property(e => e.ChapterTitle).HasMaxLength(50);
            entity.Property(e => e.Cover).HasMaxLength(500);

            entity.HasOne(d => d.Book).WithMany(p => p.BookChapters)
                .HasForeignKey(d => d.BookId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Book_Chapter_Book_Chapter");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.ToTable("Category");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CategoryName).HasMaxLength(50);
            entity.Property(e => e.Cover).HasMaxLength(500);
        });

        modelBuilder.Entity<ChapterDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Chapter_Detail");

            entity.ToTable("ChapterDetail");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Body).HasMaxLength(500);
            entity.Property(e => e.ChapterId).HasColumnName("ChapterID");
            entity.Property(e => e.Image).HasMaxLength(500);

            entity.HasOne(d => d.Chapter).WithMany(p => p.ChapterDetails)
                .HasForeignKey(d => d.ChapterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Chapter_Detail_Chapter_Detail");
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.ToTable("Course");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CategoryId).HasColumnName("CategoryID");
            entity.Property(e => e.CourseName).HasMaxLength(250);
            entity.Property(e => e.Cover).HasMaxLength(500);
            entity.Property(e => e.ProfessorId).HasColumnName("ProfessorID");
            entity.Property(e => e.TypeId).HasColumnName("TypeID");

            entity.HasOne(d => d.Category).WithMany(p => p.Courses)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Course_Category");

            entity.HasOne(d => d.Professor).WithMany(p => p.Courses)
                .HasForeignKey(d => d.ProfessorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Course_Professor");

            entity.HasOne(d => d.Type).WithMany(p => p.Courses)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Course_Type");
        });

        modelBuilder.Entity<CourseDetail>(entity =>
        {
            entity.ToTable("CourseDetail");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Course).WithMany(p => p.CourseDetails)
                .HasForeignKey(d => d.CourseId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CourseDetail_Course");

            entity.HasOne(d => d.User).WithMany(p => p.CourseDetails)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_CourseDetail_User");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.ToTable("Genre");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Cover).HasMaxLength(500);
            entity.Property(e => e.GenreName).HasMaxLength(50);
        });

        modelBuilder.Entity<Professor>(entity =>
        {
            entity.ToTable("Professor");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Institute).HasMaxLength(50);
            entity.Property(e => e.ProfessorName).HasMaxLength(50);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.Professors)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Professor_User");
        });

        modelBuilder.Entity<Quiz>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Question");

            entity.ToTable("Quiz");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.Summary).HasMaxLength(250);

            entity.HasOne(d => d.Course).WithMany(p => p.Quizs)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("FK_Question_Course");
        });

        modelBuilder.Entity<QuizAnswer>(entity =>
        {
            entity.ToTable("QuizAnswer");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Content).HasMaxLength(250);
            entity.Property(e => e.QuizQuestionId).HasColumnName("QuizQuestionID");

            entity.HasOne(d => d.QuizQuestion).WithMany(p => p.QuizAnswers)
                .HasForeignKey(d => d.QuizQuestionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QuizAnswer_QuizQuestion");
        });

        modelBuilder.Entity<QuizQuestion>(entity =>
        {
            entity.ToTable("QuizQuestion");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Question).HasMaxLength(250);
            entity.Property(e => e.QuizId).HasColumnName("QuizID");

            entity.HasOne(d => d.Quiz).WithMany(p => p.QuizQuestions)
                .HasForeignKey(d => d.QuizId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QuizQuestion_Quiz");
        });

        modelBuilder.Entity<QuizTaker>(entity =>
        {
            entity.ToTable("QuizTaker");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.QuizId).HasColumnName("QuizID");
            entity.Property(e => e.Remark).HasMaxLength(50);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Quiz).WithMany(p => p.QuizTakers)
                .HasForeignKey(d => d.QuizId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QuizTaker_Quiz");

            entity.HasOne(d => d.User).WithMany(p => p.QuizTakers)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_QuizTaker_User");
        });

        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.ToTable("RefreshToken");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ExpiryDate).HasColumnType("date");
            entity.Property(e => e.Token).HasMaxLength(250);
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.User).WithMany(p => p.RefreshTokens)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_RefreshToken_User");
        });

        modelBuilder.Entity<Response>(entity =>
        {
            entity.ToTable("Response");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AnswerContent).HasMaxLength(120);
            entity.Property(e => e.QuizAnswerId).HasColumnName("QuizAnswerID");
            entity.Property(e => e.QuizTakerId).HasColumnName("QuizTakerID");

            entity.HasOne(d => d.QuizAnswer).WithMany(p => p.Responses)
                .HasForeignKey(d => d.QuizAnswerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Response_QuizAnswer");

            entity.HasOne(d => d.QuizTaker).WithMany(p => p.Responses)
                .HasForeignKey(d => d.QuizTakerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Response_QuizTaker");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Role");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.RoleName).HasMaxLength(50);
        });

        modelBuilder.Entity<Type>(entity =>
        {
            entity.ToTable("Type");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Type1)
                .HasMaxLength(50)
                .HasColumnName("Type");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("User");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Address).HasMaxLength(250);
            entity.Property(e => e.Avatar).HasMaxLength(500);
            entity.Property(e => e.Dob)
                .HasColumnType("date")
                .HasColumnName("DOB");
            entity.Property(e => e.Email).HasMaxLength(50);
            entity.Property(e => e.FirstName).HasMaxLength(50);
            entity.Property(e => e.LastName).HasMaxLength(50);
            entity.Property(e => e.Password).HasMaxLength(250);
            entity.Property(e => e.Phone).HasMaxLength(12);
            entity.Property(e => e.RegDate).HasColumnType("date");
            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.Salt).HasMaxLength(250);
            entity.Property(e => e.Username).HasMaxLength(50);

            entity.HasOne(d => d.Role).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_User_Role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
