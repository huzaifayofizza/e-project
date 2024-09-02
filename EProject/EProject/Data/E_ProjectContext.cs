using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using EProject.Models;

namespace EProject.Data
{
    public partial class E_ProjectContext : DbContext
    {
        public E_ProjectContext()
        {
        }

        public E_ProjectContext(DbContextOptions<E_ProjectContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Competition> Competitions { get; set; } = null!;
        public virtual DbSet<Exhibition> Exhibitions { get; set; } = null!;
        public virtual DbSet<Posting> Postings { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<UserRecord> UserRecords { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=.;Initial Catalog=E_Project;Persist Security Info=False;User ID=sa;Password=aptech;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Competition>(entity =>
            {
                entity.ToTable("Competition");

                entity.Property(e => e.CompEndDate)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CompStartDate)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CompetitionBanner)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CompetitionTitle)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Exhibition>(entity =>
            {
                entity.ToTable("Exhibition");

                entity.Property(e => e.ExhibitionImage)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.ExhibitionPrice)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.ExhibitionPosting)
                    .WithMany(p => p.Exhibitions)
                    .HasForeignKey(d => d.ExhibitionPostingId)
                    .HasConstraintName("FK__Exhibitio__Exhib__2B3F6F97");
            });

            modelBuilder.Entity<Posting>(entity =>
            {
                entity.HasKey(e => e.PostId)
                    .HasName("PK__Posting__AA126018A3022EF7");

                entity.ToTable("Posting");

                entity.Property(e => e.PostDate)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.PostImg)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.PostCompetition)
                    .WithMany(p => p.Postings)
                    .HasForeignKey(d => d.PostCompetitionId)
                    .HasConstraintName("FK__Posting__PostCom__2C3393D0");

                entity.HasOne(d => d.PostUser)
                    .WithMany(p => p.Postings)
                    .HasForeignKey(d => d.PostUserId)
                    .HasConstraintName("FK__Posting__PostUse__2D27B809");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.ToTable("ROLE");

                entity.Property(e => e.RoleName)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserRecord>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__UserReco__1788CC4C282005BD");

                entity.ToTable("UserRecord");

                entity.Property(e => e.UserEmail)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UserName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UserPassword)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.UserRole)
                    .WithMany(p => p.UserRecords)
                    .HasForeignKey(d => d.UserRoleId)
                    .HasConstraintName("FK__UserRecor__UserR__2E1BDC42");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
