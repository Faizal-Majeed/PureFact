using Microsoft.EntityFrameworkCore;
using UserRegistrationAPI.Entities;

namespace UserRegistrationAPI.Helpers
{
    public partial class UserDBContext : DbContext
    {
        public UserDBContext(DbContextOptions<UserDBContext> options) 
        : base(options) { }

        public DbSet<Users> Users { get; set; }

        public virtual DbSet<Documents> Documents { get; set; }

          protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Documents>(entity =>
            {
                entity.HasKey(e => e.DocumentId)
                    .HasName("PK__Document__1ABEEF0FDE4AC1BB");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Documents)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Documents_Users");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.UserId)
                    .HasName("PK__Users__1788CC4C9D313FDB");

                entity.Property(e => e.DateOfBirth).HasColumnType("datetime");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(250);

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Lastname)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                 entity.Property(e => e.PasswordHash)
                    .IsRequired();

                entity.Property(e => e.PasswordSalt)
                    .IsRequired();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}