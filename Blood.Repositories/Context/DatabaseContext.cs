using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Blood.Contract.Repositories.Entity;
using Blood.Repositories.Entity;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Blood.Repositories.Context
{
    public class DatabaseContext : IdentityDbContext<ApplicationUser, ApplicationRole, int,
        IdentityUserClaim<int>, ApplicationUserRole, IdentityUserLogin<int>,
        IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        // DbSet
        private static readonly PasswordHasher<ApplicationUser> hasher = new();

        public virtual DbSet<ApplicationUser> ApplicationUsers => Set<ApplicationUser>();
        public virtual DbSet<ApplicationRole> ApplicationRoles => Set<ApplicationRole>();
        public virtual DbSet<ApplicationUserRole> ApplicationUserRoles => Set<ApplicationUserRole>();

        public DbSet<BloodGroup> BloodGroups { get; set; }
        public DbSet<DonorAvailability> DonorAvailabilities { get; set; }
        public DbSet<BloodRequest> BloodRequests { get; set; }
        public DbSet<BloodUnit> BloodUnits { get; set; }
        public DbSet<Donation> Donations { get; set; }
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<BloodCompatibility> BloodCompatibilities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.User)
                    .WithMany(u => u.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<BloodCompatibility>(bc =>
            {

                // Quan hệ với DonorBloodGroup
                bc.HasOne(b => b.DonorBloodGroup)
                  .WithMany(bg => bg.DonorCompatibilities) // Giả sử BloodGroup có navigation property
                  .HasForeignKey(b => b.DonorBloodGroupId)
                  .OnDelete(DeleteBehavior.Restrict);

                // Quan hệ với RecipientBloodGroup
                bc.HasOne(b => b.RecipientBloodGroup)
                  .WithMany(bg => bg.RecipientCompatibilities) // Giả sử BloodGroup có navigation property
                  .HasForeignKey(b => b.RecipientBloodGroupId)
                  .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Donation>()
                .HasOne(d => d.BloodRequest)
                .WithMany(r => r.Donations)
                .HasForeignKey(d => d.BloodRequestId)
                .OnDelete(DeleteBehavior.Restrict);

            // 👉 Seed BloodGroups
            modelBuilder.Entity<BloodGroup>().HasData(
                new BloodGroup { Id = 1, Name = "A+" },
                new BloodGroup { Id = 2, Name = "A-" },
                new BloodGroup { Id = 3, Name = "B+" },
                new BloodGroup { Id = 4, Name = "B-" },
                new BloodGroup { Id = 5, Name = "AB+" },
                new BloodGroup { Id = 6, Name = "AB-" },
                new BloodGroup { Id = 7, Name = "O+" },
                new BloodGroup { Id = 8, Name = "O-" }
            );

            // 👉 Seed BloodCompatibility (ví dụ một vài tương thích, bạn có thể thêm tiếp)
            modelBuilder.Entity<BloodCompatibility>().HasData(
                // Whole Blood and Red Blood Cells (RBCs) compatibility
                new BloodCompatibility { Id = 1, DonorBloodGroupId = 8, RecipientBloodGroupId = 1, BloodComponent = "WholeBlood", IsCompatible = true }, // O- → A+
                new BloodCompatibility { Id = 2, DonorBloodGroupId = 7, RecipientBloodGroupId = 1, BloodComponent = "WholeBlood", IsCompatible = true }, // O+ → A+
                new BloodCompatibility { Id = 3, DonorBloodGroupId = 1, RecipientBloodGroupId = 5, BloodComponent = "WholeBlood", IsCompatible = true }, // A+ → AB+
                new BloodCompatibility { Id = 4, DonorBloodGroupId = 2, RecipientBloodGroupId = 6, BloodComponent = "WholeBlood", IsCompatible = true }, // A- → AB-
                new BloodCompatibility { Id = 5, DonorBloodGroupId = 3, RecipientBloodGroupId = 5, BloodComponent = "WholeBlood", IsCompatible = true }, // B+ → AB+
                new BloodCompatibility { Id = 6, DonorBloodGroupId = 8, RecipientBloodGroupId = 8, BloodComponent = "WholeBlood", IsCompatible = true }, // O- → O-

                // Red Blood Cells (RBC) full compatibility
                new BloodCompatibility { Id = 7, DonorBloodGroupId = 8, RecipientBloodGroupId = 1, BloodComponent = "RedBloodCells", IsCompatible = true },
                new BloodCompatibility { Id = 8, DonorBloodGroupId = 8, RecipientBloodGroupId = 2, BloodComponent = "RedBloodCells", IsCompatible = true },
                new BloodCompatibility { Id = 9, DonorBloodGroupId = 8, RecipientBloodGroupId = 3, BloodComponent = "RedBloodCells", IsCompatible = true },
                new BloodCompatibility { Id = 10, DonorBloodGroupId = 8, RecipientBloodGroupId = 4, BloodComponent = "RedBloodCells", IsCompatible = true },
                new BloodCompatibility { Id = 11, DonorBloodGroupId = 8, RecipientBloodGroupId = 5, BloodComponent = "RedBloodCells", IsCompatible = true },
                new BloodCompatibility { Id = 12, DonorBloodGroupId = 8, RecipientBloodGroupId = 6, BloodComponent = "RedBloodCells", IsCompatible = true },
                new BloodCompatibility { Id = 13, DonorBloodGroupId = 8, RecipientBloodGroupId = 7, BloodComponent = "RedBloodCells", IsCompatible = true },
                new BloodCompatibility { Id = 14, DonorBloodGroupId = 8, RecipientBloodGroupId = 8, BloodComponent = "RedBloodCells", IsCompatible = true },

                new BloodCompatibility { Id = 15, DonorBloodGroupId = 7, RecipientBloodGroupId = 1, BloodComponent = "RedBloodCells", IsCompatible = true },
                new BloodCompatibility { Id = 16, DonorBloodGroupId = 7, RecipientBloodGroupId = 3, BloodComponent = "RedBloodCells", IsCompatible = true },
                new BloodCompatibility { Id = 17, DonorBloodGroupId = 7, RecipientBloodGroupId = 5, BloodComponent = "RedBloodCells", IsCompatible = true },
                new BloodCompatibility { Id = 18, DonorBloodGroupId = 7, RecipientBloodGroupId = 7, BloodComponent = "RedBloodCells", IsCompatible = true },

                new BloodCompatibility { Id = 19, DonorBloodGroupId = 1, RecipientBloodGroupId = 1, BloodComponent = "RedBloodCells", IsCompatible = true },
                new BloodCompatibility { Id = 20, DonorBloodGroupId = 1, RecipientBloodGroupId = 5, BloodComponent = "RedBloodCells", IsCompatible = true },

                new BloodCompatibility { Id = 21, DonorBloodGroupId = 2, RecipientBloodGroupId = 2, BloodComponent = "RedBloodCells", IsCompatible = true },
                new BloodCompatibility { Id = 22, DonorBloodGroupId = 2, RecipientBloodGroupId = 6, BloodComponent = "RedBloodCells", IsCompatible = true },

                new BloodCompatibility { Id = 23, DonorBloodGroupId = 3, RecipientBloodGroupId = 3, BloodComponent = "RedBloodCells", IsCompatible = true },
                new BloodCompatibility { Id = 24, DonorBloodGroupId = 3, RecipientBloodGroupId = 5, BloodComponent = "RedBloodCells", IsCompatible = true },

                new BloodCompatibility { Id = 25, DonorBloodGroupId = 4, RecipientBloodGroupId = 4, BloodComponent = "RedBloodCells", IsCompatible = true },
                new BloodCompatibility { Id = 26, DonorBloodGroupId = 4, RecipientBloodGroupId = 6, BloodComponent = "RedBloodCells", IsCompatible = true },

                // Plasma compatibility (reverse of RBC)
                new BloodCompatibility { Id = 27, DonorBloodGroupId = 5, RecipientBloodGroupId = 1, BloodComponent = "Plasma", IsCompatible = true },
                new BloodCompatibility { Id = 28, DonorBloodGroupId = 6, RecipientBloodGroupId = 2, BloodComponent = "Plasma", IsCompatible = true },
                new BloodCompatibility { Id = 29, DonorBloodGroupId = 5, RecipientBloodGroupId = 3, BloodComponent = "Plasma", IsCompatible = true },
                new BloodCompatibility { Id = 30, DonorBloodGroupId = 6, RecipientBloodGroupId = 4, BloodComponent = "Plasma", IsCompatible = true },
                new BloodCompatibility { Id = 31, DonorBloodGroupId = 1, RecipientBloodGroupId = 8, BloodComponent = "Plasma", IsCompatible = true },
                new BloodCompatibility { Id = 32, DonorBloodGroupId = 5, RecipientBloodGroupId = 7, BloodComponent = "Plasma", IsCompatible = true },
                new BloodCompatibility { Id = 33, DonorBloodGroupId = 7, RecipientBloodGroupId = 8, BloodComponent = "Plasma", IsCompatible = true },

                // Platelets - generally AB+ is universal donor for platelets
                new BloodCompatibility { Id = 34, DonorBloodGroupId = 5, RecipientBloodGroupId = 1, BloodComponent = "Platelets", IsCompatible = true },
                new BloodCompatibility { Id = 35, DonorBloodGroupId = 5, RecipientBloodGroupId = 3, BloodComponent = "Platelets", IsCompatible = true },
                new BloodCompatibility { Id = 36, DonorBloodGroupId = 5, RecipientBloodGroupId = 5, BloodComponent = "Platelets", IsCompatible = true },
                new BloodCompatibility { Id = 37, DonorBloodGroupId = 5, RecipientBloodGroupId = 7, BloodComponent = "Platelets", IsCompatible = true },

                // You can add more based on your application’s precision needs
                new BloodCompatibility { Id = 38, DonorBloodGroupId = 6, RecipientBloodGroupId = 2, BloodComponent = "Platelets", IsCompatible = true },
                new BloodCompatibility { Id = 39, DonorBloodGroupId = 6, RecipientBloodGroupId = 6, BloodComponent = "Platelets", IsCompatible = true },
                new BloodCompatibility { Id = 40, DonorBloodGroupId = 6, RecipientBloodGroupId = 4, BloodComponent = "Platelets", IsCompatible = true }
            );



            // Roles
            var adminRole = new ApplicationRole { Id = 1, Name = "Admin", NormalizedName = "ADMIN", Description = "Quản trị viên" };
            var doctorRole = new ApplicationRole { Id = 2, Name = "Doctor", NormalizedName = "DOCTOR", Description = "Bác sĩ" };
            var userRole = new ApplicationRole { Id = 3, Name = "User", NormalizedName = "USER", Description = "Người dùng" };

            modelBuilder.Entity<ApplicationRole>().HasData(adminRole, doctorRole, userRole);

            // Users
            var adminUser = new ApplicationUser
            {
                Id = 1,
                UserName = "admin",
                NormalizedUserName = "ADMIN",
                Email = "admin@example.com",
                NormalizedEmail = "ADMIN@EXAMPLE.COM",
                EmailConfirmed = true,
                FullName = "Quản trị viên",
                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                RefreshToken = null,
                RefreshTokenExpiryTime = DateTimeOffset.UtcNow.AddDays(1),
                Status = true
            };
            adminUser.PasswordHash = hasher.HashPassword(adminUser, "Admin@123");

            var doctorUser = new ApplicationUser
            {
                Id = 2,
                UserName = "doctor",
                NormalizedUserName = "DOCTOR",
                Email = "doctor@example.com",
                NormalizedEmail = "DOCTOR@EXAMPLE.COM",
                EmailConfirmed = true,
                FullName = "Bác sĩ",
                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                RefreshToken = null,
                RefreshTokenExpiryTime = DateTimeOffset.UtcNow.AddDays(1),
                Status = true
            };
            doctorUser.PasswordHash = hasher.HashPassword(doctorUser, "Doctor@123");

            var normalUser = new ApplicationUser
            {
                Id = 3,
                UserName = "user",
                NormalizedUserName = "USER",
                Email = "user@example.com",
                NormalizedEmail = "USER@EXAMPLE.COM",
                EmailConfirmed = true,
                FullName = "Người dùng",
                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                RefreshToken = null,
                RefreshTokenExpiryTime = DateTimeOffset.UtcNow.AddDays(1),
                Status = true
            };
            normalUser.PasswordHash = hasher.HashPassword(normalUser, "User@123");

            modelBuilder.Entity<ApplicationUser>().HasData(adminUser, doctorUser, normalUser);

            // UserRoles
            modelBuilder.Entity<ApplicationUserRole>().HasData(
                new ApplicationUserRole { UserId = 1, RoleId = 1 },
                new ApplicationUserRole { UserId = 2, RoleId = 2 },
                new ApplicationUserRole { UserId = 3, RoleId = 3 }
            );

            var user4 = new ApplicationUser
            {
                Id = 4,
                UserName = "john",
                NormalizedUserName = "JOHN",
                Email = "john@example.com",
                NormalizedEmail = "JOHN@EXAMPLE.COM",
                EmailConfirmed = true,
                FullName = "John Doe",
                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                RefreshToken = null,
                RefreshTokenExpiryTime = DateTimeOffset.UtcNow.AddDays(1),
                Status = true
            };

            var user5 = new ApplicationUser
            {
                Id = 5,
                UserName = "jane",
                NormalizedUserName = "JANE",
                Email = "jane@example.com",
                NormalizedEmail = "JANE@EXAMPLE.COM",
                EmailConfirmed = true,
                FullName = "Jane Smith",
                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                RefreshToken = null,
                RefreshTokenExpiryTime = DateTimeOffset.UtcNow.AddDays(1),
                Status = true
            };

            var user6 = new ApplicationUser
            {
                Id = 6,
                UserName = "alice",
                NormalizedUserName = "ALICE",
                Email = "alice@example.com",
                NormalizedEmail = "ALICE@EXAMPLE.COM",
                EmailConfirmed = true,
                FullName = "Alice Nguyen",
                SecurityStamp = Guid.NewGuid().ToString(),
                ConcurrencyStamp = Guid.NewGuid().ToString(),
                RefreshToken = null,
                RefreshTokenExpiryTime = DateTimeOffset.UtcNow.AddDays(1),
                Status = true
            };

            // Đặt mật khẩu
            user4.PasswordHash = hasher.HashPassword(user4, "John@123");
            user5.PasswordHash = hasher.HashPassword(user5, "Jane@123");
            user6.PasswordHash = hasher.HashPassword(user6, "Alice@123");

            modelBuilder.Entity<ApplicationUser>().HasData(user4, user5, user6);

            modelBuilder.Entity<ApplicationUserRole>().HasData(
                new ApplicationUserRole { UserId = 4, RoleId = 3 }, // Normal user
                new ApplicationUserRole { UserId = 5, RoleId = 3 }, // Normal user
                new ApplicationUserRole { UserId = 6, RoleId = 3 }  // Normal user
            );

            modelBuilder.Entity<BlogPost>().HasData(
                new BlogPost
                {
                    Id = 1,
                    Title = "Tầm quan trọng của hiến máu",
                    Content = "Hiến máu là một hành động cao cả, giúp cứu sống nhiều người bệnh cần truyền máu.",
                    Author = "Admin",
                    ImageUrl = "https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Fworld%20blood%20donor%20day%20social%20media%20template.png?alt=media&token=bc11e9bd-1eac-415b-8c70-20c17fcd340a",
                },
                new BlogPost
                {
                    Id = 2,
                    Title = "Những điều cần biết khi đi hiến máu",
                    Content = "Trước khi đi hiến máu, bạn cần ăn nhẹ, ngủ đủ và không uống rượu bia.",
                    Author = "Bác sĩ Nguyễn Văn A",
                    ImageUrl = "https://firebasestorage.googleapis.com/v0/b/hairsalonamazing-14369.appspot.com/o/images%2Fworld%20blood%20donor%20day%20social%20media%20template.png?alt=media&token=bc11e9bd-1eac-415b-8c70-20c17fcd340a",
                }
            );

            modelBuilder.Entity<BloodUnit>().HasData(
                new BloodUnit
                {
                    Id = 1,
                    BloodGroupId = 1, // A+
                    BloodComponent = "WholeBlood",
                    Quantity = 10,
                    ExpiryDate = DateTime.Now.AddMonths(4),
                },
                new BloodUnit
                {
                    Id = 2,
                    BloodGroupId = 2, // B+
                    BloodComponent = "Plasma",
                    Quantity = 5,
                    ExpiryDate = DateTime.Now.AddMonths(4),
                },
                new BloodUnit
                {
                    Id = 3,
                    BloodGroupId = 3, // AB+
                    BloodComponent = "RedBloodCells",
                    Quantity = 8,
                    ExpiryDate = DateTime.Now.AddMonths(4),
                }
            );



        }

    }
}
