﻿using Kysect.BotFramework.ApiProviders.Discord;
using Kysect.BotFramework.ApiProviders.Telegram;
using Kysect.BotFramework.Core.Contexts;
using Kysect.BotFramework.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Kysect.BotFramework.Data
{
    public class BotFrameworkDbContext : DbContext
    {
        public DbSet<DialogContextEntity> DialogContexts { get; set; }
        public DbSet<DiscordSenderInfoEntity> DiscordSenderInfos { get; set; }
        public DbSet<TelegramSenderInfoEntity> TelegramSenderInfos { get; set; }
        
        public BotFrameworkDbContext(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<DialogContextEntity>().HasKey(d => d.SenderInfoId);
            modelBuilder.Entity<DiscordSenderInfoEntity>().HasKey(d => d.Id);
            modelBuilder.Entity<TelegramSenderInfoEntity>().HasKey(t => t.Id);
        }
    }
}