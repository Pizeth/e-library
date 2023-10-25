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

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Question> Questions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

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
        });

        modelBuilder.Entity<BookChapter>(entity =>
        {
            entity.ToTable("Book_Chapter");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.BookId).HasColumnName("BookID");
            entity.Property(e => e.ChapterTitle).HasMaxLength(50);
            entity.Property(e => e.Cover).HasMaxLength(500);
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
            entity.ToTable("Chapter_Detail");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Body).HasMaxLength(500);
            entity.Property(e => e.ChapterId).HasColumnName("ChapterID");
            entity.Property(e => e.Image).HasMaxLength(500);
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.ToTable("Course");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.CourseName).HasMaxLength(250);
            entity.Property(e => e.Cover).HasMaxLength(500);
            entity.Property(e => e.ProfessorId)
                .HasMaxLength(50)
                .HasColumnName("ProfessorID");
            entity.Property(e => e.TypeId).HasColumnName("TypeID");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.ToTable("Genre");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Cover).HasMaxLength(500);
            entity.Property(e => e.Genre1)
                .HasMaxLength(50)
                .HasColumnName("Genre");
        });

        modelBuilder.Entity<Question>(entity =>
        {
            entity.ToTable("Question");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Answer).HasMaxLength(50);
            entity.Property(e => e.CourseId).HasColumnName("CourseID");
            entity.Property(e => e.Question1)
                .HasMaxLength(50)
                .HasColumnName("Question");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Role");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Role1)
                .HasMaxLength(50)
                .HasColumnName("Role");
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
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
