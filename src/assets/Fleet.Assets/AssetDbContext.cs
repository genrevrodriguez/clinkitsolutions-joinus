using Fleet.Assets.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Fleet.Assets
{
    public class AssetDbContext : DbContext
    {
        public DbSet<Asset> Assets { get; set; }
        public DbSet<AssetCategory> AssetCategories { get; set; }
        public DbSet<AssetFleet> AssetFleets { get; set; }
        public DbSet<Models.Fleet> Fleets { get; set; }
        public DbSet<AssetLogItem> AssetLogItems { get; set; }

        public AssetDbContext(DbContextOptions<AssetDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<AssetCategory>(asset =>
            {
                asset.Property(v => v.Id).ValueGeneratedOnAdd();
                asset.Property(v => v.Name).IsRequired(true);

                asset.HasData(new AssetCategory
                {
                    Id = 1,
                    Name = "Vehicle"
                });
            });

            builder.Entity<Asset>(asset =>
            {
                asset.Property(v => v.Id).ValueGeneratedOnAdd();
                asset.Property(v => v.Name).IsRequired(true);
            });
            
            builder.Entity<Models.Fleet>(fleet =>
            {
                fleet.Property(f => f.Id).ValueGeneratedOnAdd();
            });

            builder.Entity<AssetFleet>(assetFleet =>
            {
                assetFleet.HasKey(vf => new { vf.AssetId, vf.FleetId });
                assetFleet.HasOne(vf => vf.Asset).WithMany(v => v.AssetFleets).HasForeignKey(vf => vf.AssetId);
                assetFleet.HasOne(vf => vf.Fleet).WithMany(f => f.AssetFleets).HasForeignKey(vf => vf.FleetId);
            });

            builder.Entity<AssetLogItem>(log =>
            {
                log.Property(l => l.Id).ValueGeneratedOnAdd();
                log.OwnsOne(l => l.Location);
            });
        }
    }
}
