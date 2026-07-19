using System;
using System.Collections.Generic;
using Crawler.Entities;
using Microsoft.EntityFrameworkCore;

namespace Crawler.Context;

public partial class FindomContext : DbContext
{
    public FindomContext()
    {
    }

    public FindomContext(DbContextOptions<FindomContext> options)
        : base(options)
    {
    }

    public virtual DbSet<RawLink> RawLinks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql(System.Environment.GetEnvironmentVariable("findom_psql_dotnet"));

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<RawLink>()
            .HasIndex(r => r.Url)
            .IsUnique();

        OnModelCreatingPartial(builder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
