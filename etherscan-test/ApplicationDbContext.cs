using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace etherscan_test
{
	public class ApplicationDbContext: DbContext
	{
        // private IConfigurationRoot _config;
        // public ApplicationDbContext()
        // {
        //     var builder = new ConfigurationBuilder().AddUserSecrets("b2db93b0-dfcd-4e49-8158-1f80e8064ba7");

        //     _config = builder.Build();
        // }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {
        }

		public DbSet<Block> Blocks { get; set; }
		public DbSet<Transaction> Transactions { get; set; }

        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     optionsBuilder.UseMySQL(_config.GetConnectionString("Default"));
        // }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Block>(entity =>
            {
                entity.ToTable("Block");
                entity.Property(e => e.BlockId).HasColumnType("int(20)");
                entity.Property(e => e.BlockNumber).HasColumnType("int(20)");
                entity.Property(e => e.Hash).HasColumnType("varchar(66)");
                entity.Property(e => e.ParentHash).HasColumnType("varchar(66)");
                entity.Property(e => e.Miner).HasColumnType("varchar(42)");
                entity.Property(e => e.BlockReward).HasColumnType("decimal(50,0)");
                entity.Property(e => e.GasLimit).HasColumnType("decimal(50,0)");
                entity.Property(e => e.GasUsed).HasColumnType("decimal(50,0)");

            });

            modelBuilder.Entity<Transaction>(entity =>
            {
                entity.ToTable("Transaction");
                entity.Property(e => e.TransactionId).HasColumnType("int(20)");
                entity.Property(e => e.BlockId).HasColumnType("int(20)");
                entity.Property(e => e.Hash).HasColumnType("varchar(66)");
                entity.Property(e => e.From).HasColumnType("varchar(42)");
                entity.Property(e => e.To).HasColumnType("varchar(42)");
                entity.Property(e => e.Value).HasColumnType("decimal(50,0)");
                entity.Property(e => e.Gas).HasColumnType("decimal(50,0)");
                entity.Property(e => e.GasPrice).HasColumnType("decimal(50,0)");
                entity.Property(e => e.TransactionIndex).HasColumnType("int(20)");
            });
        }
    }
}

