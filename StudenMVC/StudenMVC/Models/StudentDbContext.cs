using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace StudenMVC.Models;

public partial class StudentDbContext : DbContext
{
    public StudentDbContext()
    {
    }

    public StudentDbContext(DbContextOptions<StudentDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=ACER\\MSSQLSERVER03;Initial Catalog=StudentDB;Persist Security Info=True;User ID=amazonweb;Password=12345;Encrypt=True;Trust Server Certificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.ClassId).HasName("PK__Classes__CB1927A09577ADA1");

            entity.Property(e => e.ClassId)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("ClassID");
            entity.Property(e => e.ClassName).HasMaxLength(70);
            entity.Property(e => e.Slot).HasMaxLength(50);
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StudentId).HasName("PK__Students__32C52A795BD5BFBD");

            entity.Property(e => e.StudentId)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("StudentID");
            entity.Property(e => e.Address).HasMaxLength(100);
            entity.Property(e => e.ClassId)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("ClassID");
            entity.Property(e => e.Fullname).HasMaxLength(50);
            entity.Property(e => e.Image).IsUnicode(false);

            entity.HasOne(d => d.Class).WithMany(p => p.Students)
                .HasForeignKey(d => d.ClassId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Students__ClassI__3A81B327");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
