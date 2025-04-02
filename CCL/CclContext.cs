using System;
using System.Collections.Generic;
using CCL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CCL;

public partial class CclContext : DbContext
{
    public CclContext()
    {
    }

    public CclContext(DbContextOptions<CclContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Producto> Productos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql("Host=localhost;Database=CCL;Username=postgres;Password=1234");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Producto>(entity =>
        {
            entity.HasKey(e => e.Idproducto).HasName("producto_pkey");

            entity.ToTable("producto");

            entity.Property(e => e.Idproducto).HasColumnName("idproducto");
            entity.Property(e => e.Cantidadproducto).HasColumnName("cantidadproducto");
            entity.Property(e => e.Nombreproducto)
                .HasMaxLength(100)
                .HasColumnName("nombreproducto");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
