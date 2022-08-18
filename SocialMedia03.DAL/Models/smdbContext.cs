using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

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

        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=MYCOMPUTER;Initial Catalog=smdb;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Post>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.ToTable("Post");

                entity.Property(e => e.Content)
                    .HasMaxLength(255)
                    .HasColumnName("content")
                    .IsFixedLength(true);

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.Hashtag)
                    .HasMaxLength(127)
                    .HasColumnName("hashtag")
                    .IsFixedLength(true);

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Image)
                    .HasMaxLength(150)
                    .HasColumnName("image")
                    .IsFixedLength(true);

                entity.Property(e => e.UserId).HasColumnName("user_id");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("User");

                entity.Property(e => e.Address)
                    .HasMaxLength(50)
                    .HasColumnName("address")
                    .IsFixedLength(true);

                entity.Property(e => e.Avatar)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("avatar")
                    .IsFixedLength(true);

                entity.Property(e => e.Birthday)
                    .HasColumnType("date")
                    .HasColumnName("birthday");

                entity.Property(e => e.CreatedDate)
                    .HasColumnType("datetime")
                    .HasColumnName("created_date");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("email")
                    .IsFixedLength(true);

                entity.Property(e => e.Enable)
                    .IsRequired()
                    .HasColumnName("enable")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("firstname")
                    .IsFixedLength(true);

                entity.Property(e => e.Hometown)
                    .HasMaxLength(50)
                    .HasColumnName("hometown")
                    .IsFixedLength(true);

                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("id");

                entity.Property(e => e.Lastname)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("lastname")
                    .IsFixedLength(true);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("password")
                    .IsFixedLength(true);

                entity.Property(e => e.Phone)
                    .HasMaxLength(50)
                    .HasColumnName("phone")
                    .IsFixedLength(true);

                entity.Property(e => e.UserRole)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("user_role")
                    .HasDefaultValueSql("(N'ROLE_USER')")
                    .IsFixedLength(true);

                entity.Property(e => e.Uuid)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("UUID")
                    .IsFixedLength(true);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
