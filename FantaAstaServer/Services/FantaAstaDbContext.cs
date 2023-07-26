﻿// Copyright (c) 2023 - Gesma94
// This code is licensed under CC BY-NC-ND 4.0 license (see LICENSE for details)

using FantaAstaServer.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace FantaAstaServer.Services
{
    public class FantaAstaDbContext : DbContext
    {
        public FantaAstaDbContext(DbContextOptions<FantaAstaDbContext> dbContextOptions) : base(dbContextOptions) { }

        
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<AuctionEntity> Auctions { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>(x =>
            {
                x.ToTable("Users");
                x.HasKey(x => x.Id);

                x.Property(x => x.Id).HasColumnName("id");
                x.Property(x => x.Email).HasColumnName("email").IsRequired();
                x.Property(x => x.Username).HasColumnName("username").IsRequired();
                x.Property(x => x.Password).HasColumnName("password").IsRequired();
                x.Property(x => x.CraetionDate).HasColumnName("creation_date").IsRequired();
                x.Property(x => x.DateOfBirth).HasColumnName("date_of_birth");
                x.Property(x => x.City).HasColumnName("city");
                x.Property(x => x.FavouriteTeam).HasColumnName("favourite_team");
            });

            modelBuilder.Entity<UserAuctionEntity>(x =>
            {
                x.ToTable("Users_Auctions");
                x.HasKey(x => new { x.UserId, x.AuctionId});
                x.HasIndex(x => new { x.UserId, x.AuctionId });

                x.Property(x => x.UserId).HasColumnName("user_id").IsRequired();
                x.Property(x => x.AuctionId).HasColumnName("auction_id").IsRequired();
                x.Property(x => x.IsAdmin).HasColumnName("is_admin").IsRequired();
            });

            modelBuilder.Entity<FootballerEntity>(x =>
            {
                x.ToTable("Footballers");
                x.HasKey(x => x.Id);

                x.HasIndex(x => new { x.FirstName, x.LastName }).IsUnique();

                x.Property(x => x.Id).HasColumnName("id");
                x.Property(x => x.FirstName).HasColumnName("first_name").IsRequired();
                x.Property(x => x.LastName).HasColumnName("last_name").IsRequired();
                x.Property(x => x.Price).HasColumnName("price").IsRequired();
                x.Property(x => x.Role).HasColumnName("role").IsRequired();
                x.Property(x => x.AuctionId).HasColumnName("auction_id").IsRequired();
                x.Property(x => x.UserOwnerId).HasColumnName("user_owner_id");
            });

            modelBuilder.Entity<BatchEntity>(x =>
            {
                x.ToTable("Batches");
                x.HasKey(x => x.Id);

                x.Property(x => x.Id).HasColumnName("id").IsRequired();
                x.Property(x => x.AuctionId).HasColumnName("auction_id").IsRequired();
                x.Property(x => x.FootballerId).HasColumnName("footballer_id").IsRequired();
                x.Property(x => x.CurrentCost).HasColumnName("current_cost").IsRequired();
                x.Property(x => x.CurrentOwnerUserId).HasColumnName("current_owner_user_id").IsRequired();
                x.Property(x => x.InitialCost).HasColumnName("initial_cost").IsRequired();
                x.Property(x => x.Status).HasColumnName("status").IsRequired();
                x.Property(x => x.FoldedUsersId).HasColumnName("folded_users_id").IsRequired();
                x.Property(x => x.NextCallerUserId).HasColumnName("next_caller_user_id").IsRequired();
                x.Property(x => x.WinnerUserId).HasColumnName("winner_user_id");
            });

            modelBuilder.Entity<OfferEntity>(x =>
            {
                x.ToTable("Offers");
                x.HasKey(x => x.Id);

                x.Property(x => x.Id).HasColumnName("id").IsRequired();
                x.Property(x => x.BatchId).HasColumnName("batch_id").IsRequired();
                x.Property(x => x.UserId).HasColumnName("user_id").IsRequired();
                x.Property(x => x.PreviousAmount).HasColumnName("previous_amount");
                x.Property(x => x.OfferedAmount).HasColumnName("offered_amount").IsRequired();
                x.Property(x => x.CreationDate).HasColumnName("creation_date").IsRequired();
            });

            modelBuilder.Entity<AuctionEntity>(x => {
                x.ToTable("Auctions");
                x.HasKey(x => x.Id);
                x.HasIndex(x => x.Name).IsUnique();

                x.Property(x => x.Id).HasColumnName("id").IsRequired();
                x.Property(x => x.CreationDate).HasColumnName("creation_date").IsRequired();
                x.Property(x => x.Status).HasColumnName("status").IsRequired();
                x.Property(x => x.Name).HasColumnName("name").IsRequired();
                x.Property(x => x.UserAmount).HasColumnName("user_amount").IsRequired();
                x.Property(x => x.InitialCredit).HasColumnName("initial_credit").IsRequired();
                x.Property(x => x.Mode).HasColumnName("mode").IsRequired();
                x.Property(x => x.UsersOrder).HasColumnName("users_order");
                x.Property(x => x.RealTimeTimeoutCallMs).HasColumnName("realtime_timeout_call_ms");
                x.Property(x => x.GoalkeaperMinAmount).HasColumnName("goalkeaper_min_amount").IsRequired();
                x.Property(x => x.GoalkeaperMaxAmount).HasColumnName("goalkeaper_max_amount").IsRequired();
                x.Property(x => x.DefenderMinAmount).HasColumnName("defender_min_amount").IsRequired();
                x.Property(x => x.DefenderMaxAmount).HasColumnName("defender_max_amount").IsRequired();
                x.Property(x => x.MidfielderMinAmount).HasColumnName("midfielder_min_amount").IsRequired();
                x.Property(x => x.MidfielderMaxAmount).HasColumnName("midfielder_max_amount").IsRequired();
                x.Property(x => x.StrikerMinAmount).HasColumnName("striker_min_amount").IsRequired();
                x.Property(x => x.StrikerMaxAmount).HasColumnName("striker_max_amount").IsRequired();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}