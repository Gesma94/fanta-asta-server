// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using FantaAsta.Domain.Common;
using FantaAsta.Domain.Models;
using FantaAsta.Infrastructure.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace FantaAsta.Infrastructure.DbContexts;

public class PostgreSqlContext : DbContext
{
    private readonly PostgreSqlOptions _postgreSqlOptions;

        public PostgreSqlContext(IOptions<PostgreSqlOptions> postgreSqlConfig, DbContextOptions<PostgreSqlContext> dbContextOptions)
            : base(dbContextOptions)
        { 
             _postgreSqlOptions = postgreSqlConfig?.Value ?? throw new ArgumentNullException(nameof(postgreSqlConfig));
        }

        
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<OfferEntity> Offers { get; set; }
        public DbSet<BatchEntity> Batches { get; set; }
        public DbSet<AuctionEntity> Auctions { get; set; }
        public DbSet<FootballerEntity> Footballers { get; set; }
        public DbSet<UserAuctionEntity> UserAuctions { get; set; }
        public DbSet<FootballerUserEntity> FootballerUsers { get; set; }
        public DbSet<UserRecoveryGuidEntity> UserRecoveryGuids { get; set; }


        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedTime = DateTimeOffset.UtcNow;
                        entry.Entity.LastModifiedTime = DateTimeOffset.UtcNow;
                        break;
                    case EntityState.Modified:
                        entry.Entity.LastModifiedTime = DateTimeOffset.UtcNow;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_postgreSqlOptions.GetConnectionString());
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PostgreSqlContext).Assembly);
        }    
}