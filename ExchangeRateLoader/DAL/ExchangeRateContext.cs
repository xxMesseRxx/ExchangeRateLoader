namespace ExchangeRateLoader.DAL;

using System;
using System.Collections.Generic;
using ExchangeRateLoader.Model;
using Microsoft.EntityFrameworkCore;

public partial class ExchangeRateContext : DbContext
{
    public DbSet<UahRate> UahRates { get; set; } = null!;

    public ExchangeRateContext(DbContextOptions<ExchangeRateContext> options)
        : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<UahRate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("uah_rate_pkey");

            entity.ToTable("uah_rate");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Currency)
                .HasMaxLength(3)
                .HasColumnName("currency");
            entity.Property(e => e.Date).HasColumnName("date");
            entity.Property(e => e.Rate)
                .HasPrecision(12, 7)
                .HasColumnName("rate");
        });
    }
}
