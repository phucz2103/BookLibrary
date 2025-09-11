using BookLibrary.Domain;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BookLibrary.Data
{
    public class ApplicationDBContext : IdentityDbContext<User, Role, Guid>
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<BorrowOrder> BorrowOrders { get; set; }
        public DbSet<BorrowDetail> BorrowDetails { get; set; }
        public DbSet<Fine> Fines { get; set; }
        public DbSet<Author> Author { get; set; }
        public DbSet<Publishers> Publishers { get; set; }

        public DbSet<TEntity> SetEntity<TEntity>() where TEntity : class
        {
            return Set<TEntity>();
        }

        public async Task<int> CommitChangesAsync()
        {
            return await SaveChangesAsync();
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);
            // Cấu hình bảng User
            builder.Entity<User>(entity =>
            {
                entity.ToTable("Users");
                entity.Property(u => u.FullName).IsRequired().HasMaxLength(100);
                entity.Property(u => u.Email).IsRequired().HasMaxLength(100);
                entity.Property(u => u.UserName).IsRequired().HasMaxLength(256);
                entity.Property(u => u.PasswordHash).IsRequired();
            });
            // Cấu hình bảng Role
            builder.Entity<Role>(entity =>
            {
                entity.ToTable("Roles");
                entity.Property(r => r.Description).IsRequired().HasMaxLength(100);
            });
            // Cấu hình bảng UserRoles
            builder.Entity<IdentityUserRole<Guid>>(entity =>
            {
                entity.ToTable("UserRoles");
                entity.HasKey(ur => new { ur.UserId, ur.RoleId });
            });
            // Cấu hình bảng UserClaims
            builder.Entity<IdentityUserClaim<Guid>>(entity =>
            {
                entity.ToTable("UserClaims");
                entity.HasKey(uc => uc.Id);
            });
            // Cấu hình bảng RoleClaims
            builder.Entity<IdentityRoleClaim<Guid>>(entity =>
            {
                entity.ToTable("RoleClaims");
                entity.HasKey(rc => rc.Id);
            });
            // Cấu hình bảng UserLogins
            builder.Entity<IdentityUserLogin<Guid>>(entity =>
            {
                entity.ToTable("UserLogins");
                entity.HasKey(ul => new { ul.LoginProvider, ul.ProviderKey });
            });
            // Cấu hình bảng UserTokens
            builder.Entity<IdentityUserToken<Guid>>(entity =>
            {
                entity.ToTable("UserTokens");
                entity.HasKey(ut => new { ut.UserId, ut.LoginProvider, ut.Name });
            });

            builder.Entity<BorrowDetail>(entity =>
            {
                entity.HasKey(bd => new { bd.BorrowId, bd.BookId });

                entity.HasOne(bd => bd.Book)
                      .WithMany(b => b.BorrowDetails)
                      .HasForeignKey(bd => bd.BookId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(bd => bd.BorrowOrder)
                      .WithMany(bo => bo.BorrowDetails)
                      .HasForeignKey(bd => bd.BorrowId)
                      .OnDelete(DeleteBehavior.Cascade);

            });

            // Cấu hình bảng RefreshToken
            builder.Entity<RefreshToken>(entity =>
            {
                entity.ToTable("RefreshTokens");
                entity.HasKey(rt => rt.Id);
                entity.Property(rt => rt.Token).IsRequired();
                entity.Property(rt => rt.ExpiryDate).IsRequired();
                entity.HasOne(rt => rt.User)
                      .WithMany(u => u.RefreshTokens)
                      .HasForeignKey(rt => rt.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            // Cấu hình bảng Category
            builder.Entity<Category>(entity =>
            {
                entity.ToTable("Categories");
                entity.HasKey(c => c.CategoryId);
            });
            // Cấu hình bảng Book
            builder.Entity<Book>(entity =>
            {
                entity.ToTable("Books");
                entity.HasKey(b => b.BookId);
                entity.HasOne(b => b.Category)
                      .WithMany(c => c.Books)
                      .HasForeignKey(b => b.CategoryId)
                      .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(b => b.Author)
                      .WithMany(c => c.Books)
                      .HasForeignKey(a => a.AuthorId)
                      .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(b => b.Publisher)
                      .WithMany(c => c.Books)
                      .HasForeignKey(p => p.PublisherId)
                      .OnDelete(DeleteBehavior.SetNull);
            });
            // Cấu hình bảng BorrowOrder
            builder.Entity<BorrowOrder>(entity =>
            {
                entity.ToTable("BorrowOrders");
                entity.HasKey(bo => bo.BorrowOrderId);
                entity.HasOne(bo => bo.User)
                      .WithMany(u => u.borrowOrders)
                      .HasForeignKey(bo => bo.UserId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            builder.Entity<Fine>(entity =>
            {
                entity.ToTable("Fines");
                entity.HasKey(f => f.FineId);

                entity.HasOne(f => f.BorrowOrder)
                      .WithOne(bo => bo.Fine)
                      .HasForeignKey<Fine>(f => f.BorrowId)
                      .OnDelete(DeleteBehavior.Cascade);
            });
            builder.Entity<Author>(entity =>
            {
                entity.ToTable("Authors");
                entity.HasKey(a => a.AuthorId);
            });

            builder.Entity<Publishers>(entity =>
            {
                entity.ToTable("Publishers");
                entity.HasKey(p => p.PublisherId);
            });

        }
    }
}