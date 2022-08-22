using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SocialMedia03.DAL.Models
{
    public partial class smdbContext : DbContext
    {
        public smdbContext()
        {
        }

        public smdbContext(DbContextOptions<smdbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Comment> Comments { get; set; } = null!;
        public virtual DbSet<Notif> Notifs { get; set; } = null!;
        public virtual DbSet<Post> Posts { get; set; } = null!;
        public virtual DbSet<React> Reacts { get; set; } = null!;
        public virtual DbSet<Report> Reports { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=CONGSANG\\SANG;Initial Catalog=smdb;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("Comment");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Content)
                    .HasMaxLength(255)
                    .HasColumnName("content")
                    .IsFixedLength();

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.ParentId).HasColumnName("parent_id");

                entity.Property(e => e.PostId).HasColumnName("post_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Parent)
                    .WithMany(p => p.InverseParent)
                    .HasForeignKey(d => d.ParentId)
                    .HasConstraintName("FK_Comment_Reply");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.PostId)
                    .HasConstraintName("FK_Comment_Post");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Comment_User");
            });

            modelBuilder.Entity<Notif>(entity =>
            {
                entity.ToTable("Notif");

                entity.HasIndex(e => new { e.UserId, e.PostId, e.Type, e.CommentId }, "Unique_Key_Notif");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CommentId).HasColumnName("comment_id");

                entity.Property(e => e.IsRead)
                    .HasColumnName("is_read")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.PostId).HasColumnName("post_id");

                entity.Property(e => e.Type)
                    .HasMaxLength(20)
                    .HasColumnName("type")
                    .IsFixedLength();

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Comment)
                    .WithMany(p => p.Notifs)
                    .HasForeignKey(d => d.CommentId)
                    .HasConstraintName("FK_Notif_Comment");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Notifs)
                    .HasForeignKey(d => d.PostId)
                    .HasConstraintName("FK_Notif_Post");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Notifs)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Notif_User");
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.ToTable("Post");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Content)
                    .HasMaxLength(255)
                    .HasColumnName("content")
                    .IsFixedLength();

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.Hashtag)
                    .HasMaxLength(127)
                    .HasColumnName("hashtag")
                    .IsFixedLength();

                entity.Property(e => e.Image)
                    .HasMaxLength(150)
                    .HasColumnName("image")
                    .IsFixedLength();

                entity.Property(e => e.UserId).HasColumnName("user_id");
            });

            modelBuilder.Entity<React>(entity =>
            {
                entity.ToTable("React");

                entity.HasIndex(e => new { e.PostId, e.UserId, e.CommentId }, "Unique_Key_React")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CommentId).HasColumnName("comment_id");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.PostId).HasColumnName("post_id");

                entity.Property(e => e.Type)
                    .HasColumnName("type")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Comment)
                    .WithMany(p => p.Reacts)
                    .HasForeignKey(d => d.CommentId)
                    .HasConstraintName("FK_React_Comment");

                entity.HasOne(d => d.Post)
                    .WithMany(p => p.Reacts)
                    .HasForeignKey(d => d.PostId)
                    .HasConstraintName("FK_React_Post");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Reacts)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_React_User");
            });

            modelBuilder.Entity<Report>(entity =>
            {
                entity.ToTable("Report");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.Details)
                    .HasMaxLength(255)
                    .HasColumnName("details")
                    .IsFixedLength();

                entity.Property(e => e.IsSolve)
                    .HasColumnName("is_solve")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Reason)
                    .HasMaxLength(20)
                    .HasColumnName("reason")
                    .IsFixedLength();

                entity.Property(e => e.TargetPostId).HasColumnName("target_post_id");

                entity.Property(e => e.TargetUserId).HasColumnName("target_user_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.TargetPost)
                    .WithMany(p => p.Reports)
                    .HasForeignKey(d => d.TargetPostId)
                    .HasConstraintName("FK_Report_Post");

                entity.HasOne(d => d.TargetUser)
                    .WithMany(p => p.ReportTargetUsers)
                    .HasForeignKey(d => d.TargetUserId)
                    .HasConstraintName("FK_Report_Target_User");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ReportUsers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Report_User");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Address)
                    .HasMaxLength(50)
                    .HasColumnName("address")
                    .IsFixedLength();

                entity.Property(e => e.Avatar)
                    .HasMaxLength(255)
                    .HasColumnName("avatar")
                    .IsFixedLength();

                entity.Property(e => e.Birthday)
                    .HasColumnType("date")
                    .HasColumnName("birthday");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("email")
                    .IsFixedLength();

                entity.Property(e => e.Enable)
                    .IsRequired()
                    .HasColumnName("enable")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Firstname)
                    .HasMaxLength(50)
                    .HasColumnName("firstname")
                    .IsFixedLength();

                entity.Property(e => e.Hometown)
                    .HasMaxLength(50)
                    .HasColumnName("hometown")
                    .IsFixedLength();

                entity.Property(e => e.Lastname)
                    .HasMaxLength(50)
                    .HasColumnName("lastname")
                    .IsFixedLength();

                entity.Property(e => e.Password)
                    .HasMaxLength(100)
                    .HasColumnName("password")
                    .IsFixedLength();

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .HasColumnName("phone")
                    .IsFixedLength();

                entity.Property(e => e.UserRole)
                    .HasMaxLength(20)
                    .HasColumnName("user_role")
                    .HasDefaultValueSql("(N'ROLE_USER')")
                    .IsFixedLength();

                entity.Property(e => e.Uuid)
                    .HasMaxLength(100)
                    .HasColumnName("UUID")
                    .IsFixedLength();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
