using System;
using Microsoft.EntityFrameworkCore;
using FarmManager.Shared.Models;

namespace FarmManager.Server.Repository;

public class FarmDbContext : DbContext
{
    protected readonly IConfiguration Configuration;

    public DbSet<Field> Fields { get; set; }

	public FarmDbContext(IConfiguration configuration)
	{
        Configuration = configuration;
	}

    protected override void OnConfiguring(DbContextOptionsBuilder options)
    {
        // connect to postgres with connection string from app settings
        options.UseNpgsql(Configuration["FarmsConnectionString"]);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Field>(entity =>
        {

        });
        modelBuilder.FinalizeModel();
        Console.WriteLine("Finalize Model");
    }
}

