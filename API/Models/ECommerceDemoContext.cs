using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace API.Models
{
    public partial class ECommerceDemoContext : DbContext
    {
        public ECommerceDemoContext(DbContextOptions<ECommerceDemoContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductAttribute> ProductAttributes { get; set; }
        public virtual DbSet<ProductAttributeLookup> ProductAttributeLookups { get; set; }
        public virtual DbSet<ProductCategory> ProductCategories { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.ProdDescription).IsUnicode(false);

                entity.Property(e => e.ProdName)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.HasOne(d => d.ProdCat)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.ProdCatId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product_ProductCategory");
            });

            modelBuilder.Entity<ProductAttribute>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("ProductAttribute");

                entity.Property(e => e.AttributeValue)
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.HasOne(d => d.Attribute)
                    .WithMany()
                    .HasForeignKey(d => d.AttributeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductAttribute_ProductAttributeLookup");

                entity.HasOne(d => d.Product)
                    .WithMany()
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductAttribute_Product");
            });

            modelBuilder.Entity<ProductAttributeLookup>(entity =>
            {
                entity.HasKey(e => e.AttributeId);

                entity.ToTable("ProductAttributeLookup");

                entity.Property(e => e.AttributeName)
                    .IsRequired()
                    .HasMaxLength(250)
                    .IsUnicode(false);

                entity.HasOne(d => d.ProdCat)
                    .WithMany(p => p.ProductAttributeLookups)
                    .HasForeignKey(d => d.ProdCatId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductAttributeLookup_ProductCategory");
            });

            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.HasKey(e => e.ProdCatId);

                entity.ToTable("ProductCategory");

                entity.Property(e => e.CategoryName)
                    .HasMaxLength(250)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
