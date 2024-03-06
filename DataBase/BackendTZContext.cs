using Core.Models;
using DataBase.Models;
using Microsoft.EntityFrameworkCore;

namespace DataBase;

public partial class BackendTZContext : DbContext
{
    public BackendTZContext(DbContextOptions<BackendTZContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Statistic> Statistics { get; set; }

    public virtual DbSet<Experiment> Experiments { get; set; }

    public virtual DbSet<Key> Keys { get; set; }

    public virtual DbSet<Value> Values { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Experiment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Experime__3214EC073EBDA9F8");

            entity.ToTable("Experiment");

            entity.HasOne(d => d.Key).WithMany(p => p.Experiments)
                .HasForeignKey(d => d.KeyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Experimen__KeyId__3F466844");

            entity.HasOne(d => d.Value).WithMany(p => p.Experiments)
                .HasForeignKey(d => d.ValueId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Experimen__Value__403A8C7D");
        });

        modelBuilder.Entity<Key>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Key__3214EC0706D48E42");

            entity.ToTable("Key");

            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Value>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Value__3214EC07563F7054");

            entity.ToTable("Value");

            entity.Property(e => e.Name).HasMaxLength(50);

            entity.HasOne(d => d.Key).WithMany(p => p.Values)
                .HasForeignKey(d => d.KeyId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Value__KeyId__3B75D760");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
