using Microsoft.EntityFrameworkCore;
using PRN232_Blood_Donation_Support_System.Models;
using System;

namespace PRN232_Blood_Donation_Support_System.Data
{
    public class BloodDonationDbContext : DbContext
    {
        public BloodDonationDbContext(DbContextOptions<BloodDonationDbContext> options)
            : base(options)
        {
        }

        // DbSet properties
        public DbSet<User> Users { get; set; }
        public DbSet<BloodType> BloodTypes { get; set; }
        public DbSet<DonorProfile> DonorProfiles { get; set; }
        public DbSet<RecipientProfile> RecipientProfiles { get; set; }
        public DbSet<BloodCompatibility> BloodCompatibilities { get; set; }
        public DbSet<BloodRequest> BloodRequests { get; set; }
        public DbSet<BloodDonation> BloodDonations { get; set; }
        public DbSet<BloodInventory> BloodInventories { get; set; }
        public DbSet<DonationRequestMatching> DonationRequestMatchings { get; set; }
        public DbSet<DonationProcess> DonationProcesses { get; set; }
        public DbSet<ProcessLog> ProcessLogs { get; set; }
        public DbSet<HealthCheck> HealthChecks { get; set; }
        public DbSet<BloodProcessing> BloodProcessings { get; set; }
        public DbSet<EmergencyNotification> EmergencyNotifications { get; set; }
        public DbSet<EmergencyResponder> EmergencyResponders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure entity relationships, constraints, and seed data

            // 1. User configurations
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Username).IsUnique();
                entity.HasIndex(e => e.Email).IsUnique();

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");
                entity.Property(e => e.IsVerified).HasDefaultValue(false);

                // User to DonorProfile relationship (1-to-1)
                entity.HasOne(e => e.DonorProfile)
                      .WithOne(e => e.User)
                      .HasForeignKey<DonorProfile>(e => e.UserId)
                      .OnDelete(DeleteBehavior.Restrict);

                // User to RecipientProfile relationship (1-to-1)
                entity.HasOne(e => e.RecipientProfile)
                      .WithOne(e => e.User)
                      .HasForeignKey<RecipientProfile>(e => e.UserId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // 2. BloodType configurations
            modelBuilder.Entity<BloodType>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");

                // Relationships
                entity.HasMany(e => e.DonorProfiles)
                      .WithOne(e => e.BloodType)
                      .HasForeignKey(e => e.BloodTypeId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.RecipientProfiles)
                      .WithOne(e => e.BloodType)
                      .HasForeignKey(e => e.BloodTypeId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.DonorCompatibilities)
                      .WithOne(e => e.DonorBloodType)
                      .HasForeignKey(e => e.DonorBloodTypeId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(e => e.RecipientCompatibilities)
                      .WithOne(e => e.RecipientBloodType)
                      .HasForeignKey(e => e.RecipientBloodTypeId)
                      .OnDelete(DeleteBehavior.Restrict);

                // Seed data for blood types
                entity.HasData(
                    new BloodType { Id = 1, Name = "A+", Description = "A positive blood type", CreatedAt = DateTime.Now },
                    new BloodType { Id = 2, Name = "A-", Description = "A negative blood type", CreatedAt = DateTime.Now },
                    new BloodType { Id = 3, Name = "B+", Description = "B positive blood type", CreatedAt = DateTime.Now },
                    new BloodType { Id = 4, Name = "B-", Description = "B negative blood type", CreatedAt = DateTime.Now },
                    new BloodType { Id = 5, Name = "AB+", Description = "AB positive blood type", CreatedAt = DateTime.Now },
                    new BloodType { Id = 6, Name = "AB-", Description = "AB negative blood type", CreatedAt = DateTime.Now },
                    new BloodType { Id = 7, Name = "O+", Description = "O positive blood type", CreatedAt = DateTime.Now },
                    new BloodType { Id = 8, Name = "O-", Description = "O negative blood type", CreatedAt = DateTime.Now }
                );
            });

            // 3. DonorProfile configurations
            modelBuilder.Entity<DonorProfile>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");
                entity.Property(e => e.DonationCount).HasDefaultValue(0);
                entity.Property(e => e.IsAvailable).HasDefaultValue(true);
                entity.Property(e => e.IsEmergencyAvailable).HasDefaultValue(false);

                // Add helpful index for finding donors
                entity.HasIndex(e => new { e.BloodTypeId, e.IsAvailable, e.IsEmergencyAvailable });
            });

            // 4. RecipientProfile configurations
            modelBuilder.Entity<RecipientProfile>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");
                entity.Property(e => e.Emergency).HasDefaultValue(false);
            });

            // 5. BloodCompatibility configurations
            modelBuilder.Entity<BloodCompatibility>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");

                // Create unique index
                entity.HasIndex(e => new { e.DonorBloodTypeId, e.RecipientBloodTypeId, e.TransfusionType })
                      .IsUnique();

                // Seed data for common compatibility rules (whole blood)
                // Only adding a subset as example - full compatibility matrix would be longer
                entity.HasData(
                    // O can donate to everyone (whole blood)
                    new BloodCompatibility { Id = 1, DonorBloodTypeId = 7, RecipientBloodTypeId = 1, TransfusionType = "whole_blood", CompatibilityLevel = "compatible", CreatedAt = DateTime.Now }, // O+ to A+
                    new BloodCompatibility { Id = 2, DonorBloodTypeId = 7, RecipientBloodTypeId = 3, TransfusionType = "whole_blood", CompatibilityLevel = "compatible", CreatedAt = DateTime.Now }, // O+ to B+
                    new BloodCompatibility { Id = 3, DonorBloodTypeId = 7, RecipientBloodTypeId = 5, TransfusionType = "whole_blood", CompatibilityLevel = "compatible", CreatedAt = DateTime.Now }, // O+ to AB+
                    new BloodCompatibility { Id = 4, DonorBloodTypeId = 7, RecipientBloodTypeId = 7, TransfusionType = "whole_blood", CompatibilityLevel = "ideal", CreatedAt = DateTime.Now },      // O+ to O+

                    new BloodCompatibility { Id = 5, DonorBloodTypeId = 8, RecipientBloodTypeId = 1, TransfusionType = "whole_blood", CompatibilityLevel = "compatible", CreatedAt = DateTime.Now }, // O- to A+
                    new BloodCompatibility { Id = 6, DonorBloodTypeId = 8, RecipientBloodTypeId = 2, TransfusionType = "whole_blood", CompatibilityLevel = "compatible", CreatedAt = DateTime.Now }, // O- to A-
                    new BloodCompatibility { Id = 7, DonorBloodTypeId = 8, RecipientBloodTypeId = 3, TransfusionType = "whole_blood", CompatibilityLevel = "compatible", CreatedAt = DateTime.Now }, // O- to B+
                    new BloodCompatibility { Id = 8, DonorBloodTypeId = 8, RecipientBloodTypeId = 4, TransfusionType = "whole_blood", CompatibilityLevel = "compatible", CreatedAt = DateTime.Now }, // O- to B-
                    new BloodCompatibility { Id = 9, DonorBloodTypeId = 8, RecipientBloodTypeId = 5, TransfusionType = "whole_blood", CompatibilityLevel = "compatible", CreatedAt = DateTime.Now }, // O- to AB+
                    new BloodCompatibility { Id = 10, DonorBloodTypeId = 8, RecipientBloodTypeId = 6, TransfusionType = "whole_blood", CompatibilityLevel = "compatible", CreatedAt = DateTime.Now }, // O- to AB-
                    new BloodCompatibility { Id = 11, DonorBloodTypeId = 8, RecipientBloodTypeId = 7, TransfusionType = "whole_blood", CompatibilityLevel = "compatible", CreatedAt = DateTime.Now }, // O- to O+
                    new BloodCompatibility { Id = 12, DonorBloodTypeId = 8, RecipientBloodTypeId = 8, TransfusionType = "whole_blood", CompatibilityLevel = "ideal", CreatedAt = DateTime.Now }       // O- to O-
                );
            });

            // 6. BloodRequest configurations
            modelBuilder.Entity<BloodRequest>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");
                entity.Property(e => e.Status).HasDefaultValue("pending");
                entity.Property(e => e.Urgency).HasDefaultValue("normal");
                entity.Property(e => e.FulfilledUnits).HasDefaultValue(0);

                // Create helpful indexes
                entity.HasIndex(e => new { e.RecipientId, e.Status });
                entity.HasIndex(e => new { e.BloodTypeId, e.TransfusionType, e.Urgency });
                entity.HasIndex(e => e.RequiredBy);
            });

            // 7. BloodDonation configurations
            modelBuilder.Entity<BloodDonation>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");
                entity.Property(e => e.Status).HasDefaultValue("scheduled");
                entity.Property(e => e.InventoryAdded).HasDefaultValue(false);

                // Relationship with HealthCheck
                entity.HasOne(e => e.HealthCheck)
                      .WithOne(e => e.BloodDonation)
                      .HasForeignKey<BloodDonation>(e => e.HealthCheckId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // 8. BloodInventory configurations
            modelBuilder.Entity<BloodInventory>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");
                entity.Property(e => e.Status).HasDefaultValue("available");
                entity.Property(e => e.Units).HasDefaultValue(1);
                entity.Property(e => e.Tested).HasDefaultValue(false);

                // Create helpful indexes
                entity.HasIndex(e => new { e.BloodTypeId, e.TransfusionType, e.Status });
                entity.HasIndex(e => e.ExpiryDate);
                entity.HasIndex(e => e.DonationId);
            });

            // 9. DonationRequestMatching configurations
            modelBuilder.Entity<DonationRequestMatching>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");
                entity.Property(e => e.Status).HasDefaultValue("matched");

                // Create unique index
                entity.HasIndex(e => new { e.RequestId, e.DonorId })
                      .IsUnique();
            });

            // 10. DonationProcess configurations
            modelBuilder.Entity<DonationProcess>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");
                entity.Property(e => e.CurrentStage).HasDefaultValue("request_received");

                // Create helpful index
                entity.HasIndex(e => new { e.RequestId, e.CurrentStage });
            });

            // 11. ProcessLog configurations
            modelBuilder.Entity<ProcessLog>(entity =>
            {
                entity.Property(e => e.Timestamp).HasDefaultValueSql("GETDATE()");

                // Create helpful index
                entity.HasIndex(e => new { e.ProcessId, e.Stage });
            });

            // 12. HealthCheck configurations
            modelBuilder.Entity<HealthCheck>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");

                // Add the missing ProcessId foreign key relationships if needed
                // This assumes we've added ProcessId to the HealthCheck class
                entity.HasOne(e => e.Process)
                      .WithMany(e => e.HealthChecks)
                      .HasForeignKey(e => e.ProcessId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // 13. BloodProcessing configurations
            modelBuilder.Entity<BloodProcessing>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");
                entity.Property(e => e.SeparatedComponents).HasDefaultValue(false);
                entity.Property(e => e.AddedToInventory).HasDefaultValue(false);
            });

            // 14. EmergencyNotification configurations
            modelBuilder.Entity<EmergencyNotification>(entity =>
            {
                entity.Property(e => e.SendAt).HasDefaultValueSql("GETDATE()");
                entity.Property(e => e.Status).HasDefaultValue("sent");
                entity.Property(e => e.Response).HasDefaultValue("no_response");

                // Create helpful index
                entity.HasIndex(e => new { e.RequestId, e.DonorId });
            });

            // 15. EmergencyResponder configurations
            modelBuilder.Entity<EmergencyResponder>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("GETDATE()");

                // Create helpful index
                entity.HasIndex(e => new { e.RequestId, e.DonorId });

                // Add missing Foreign Key relationships
                entity.HasOne(e => e.Request)
                      .WithMany(e => e.EmergencyResponders)
                      .HasForeignKey(e => e.RequestId)
                      .OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(e => e.Donor)
                      .WithMany(e => e.EmergencyResponders)
                      .HasForeignKey(e => e.DonorId)
                      .OnDelete(DeleteBehavior.Restrict);
            });

            // Set up delete behavior for all relationships to Restrict by default
            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                                                    .SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelBuilder);
        }
    }
}