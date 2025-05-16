using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Blood.Contract.Repositories.Entity;
using Blood.Repositories.Entity;
using Microsoft.AspNetCore.Identity;

namespace Blood.Repositories.Context
{
    public class DatabaseContext : IdentityDbContext<ApplicationUser, ApplicationRole, int,
        IdentityUserClaim<int>, ApplicationUserRole, IdentityUserLogin<int>,
        IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options) { }

        // DbSet
        public virtual DbSet<ApplicationUser> ApplicationUsers => Set<ApplicationUser>();
        public virtual DbSet<ApplicationRole> ApplicationRoles => Set<ApplicationRole>();
        public virtual DbSet<ApplicationUserRole> ApplicationUserRoles => Set<ApplicationUserRole>();

        public virtual DbSet<BloodCompatibility> BloodCompatibilities { get; set; }
        public virtual DbSet<BloodDonation> BloodDonations { get; set; }
        public virtual DbSet<BloodProcessing> BloodProcessings { get; set; }
        public virtual DbSet<BloodRequest> BloodRequests { get; set; }
        public virtual DbSet<BloodType> BloodTypes { get; set; }
        public virtual DbSet<DonationProcess> DonationProcesses { get; set; }
        public virtual DbSet<DonorProfile> DonorProfiles { get; set; }
        public virtual DbSet<EmergencyNotification> EmergencyNotifications { get; set; }
        public virtual DbSet<EmergencyResponder> EmergencyResponders { get; set; }
        public virtual DbSet<HealthCheck> HealthChecks { get; set; }
        public virtual DbSet<ProcessLog> ProcessLogs { get; set; }
        public virtual DbSet<RecipientProfile> RecipientProfiles { get; set; }

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

            modelBuilder.Entity<BloodCompatibility>(entity =>
            {
                entity.HasOne(bc => bc.DonorBloodType)
                    .WithMany(bt => bt.DonorCompatibilities)
                    .HasForeignKey(bc => bc.DonorBloodTypeId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(bc => bc.RecipientBloodType)
                    .WithMany(bt => bt.RecipientCompatibilities)
                    .HasForeignKey(bc => bc.RecipientBloodTypeId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<BloodDonation>()
                .HasOne(bd => bd.BloodInventory)
                .WithOne(bi => bi.Donation)
                .HasForeignKey<BloodInventory>(bi => bi.DonationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BloodDonation>()
                .HasOne(bd => bd.BloodProcessing)
                .WithOne(bp => bp.Donation)
                .HasForeignKey<BloodProcessing>(bp => bp.DonationId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<BloodRequest>(entity =>
            {
                entity.HasOne(r => r.Recipient)
                    .WithMany(p => p.BloodRequests)
                    .HasForeignKey(r => r.RecipientId)
                    .OnDelete(DeleteBehavior.Restrict); // NGĂN cascade path

                entity.HasOne(r => r.BloodType)
                    .WithMany(bt => bt.BloodRequests)
                    .HasForeignKey(r => r.BloodTypeId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(r => r.Staff)
                    .WithMany(bt => bt.BloodRequests) // nếu không cần Staff.BloodRequests
                    .HasForeignKey(r => r.StaffId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<RecipientProfile>(entity =>
            {
                entity.HasOne(p => p.User)
                    .WithOne(u => u.RecipientProfile)
                    .HasForeignKey<RecipientProfile>(p => p.UserId)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(p => p.BloodType)
                    .WithMany(bt => bt.RecipientProfiles)
                    .HasForeignKey(p => p.BloodTypeId)
                    .OnDelete(DeleteBehavior.Restrict);
            });


        }

    }
}
