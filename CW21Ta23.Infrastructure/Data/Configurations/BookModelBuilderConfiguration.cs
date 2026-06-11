using CW21Ta23.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CW21Ta23.Infrastructure.Data.Configurations;

public class BookModelBuilderConfiguration : BaseModelBuilderConfiguration<Book>
{
    protected override void ApplyEntityConfiguration(EntityTypeBuilder<Book> modelBuilder)
    {
        modelBuilder.Property(u => u.Title)
            .HasColumnType("nvarchar(100)")
            .IsRequired();
        
        modelBuilder.Property(u=>u.Stock)
            .HasDefaultValue(0)
            .IsRequired();
        modelBuilder.Property(u => u.Price)
            .HasColumnType("decimal(12,2)");
        
        
        modelBuilder.Property(u => u.Price)
            .HasDefaultValue(0);
            
        
        
        modelBuilder.Property(u => u.PublishYear)
            .IsRequired();

        modelBuilder.Property(u => u.CreatedAt)
            .HasDefaultValueSql("GETUTCDATE()");
            
        
        modelBuilder.HasIndex(u => u.AuthorId)
            ;
        
        modelBuilder.HasIndex(u => u.CategoryId)
            ;

        modelBuilder.HasOne(b => b.Author)
            .WithMany(a => a.Books)
            .HasForeignKey(b => b.AuthorId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.HasOne(b => b.Category)
            .WithMany(c => c.Books)
            .HasForeignKey(b => b.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);
        
        
        modelBuilder.HasOne(b => b.Publisher)
            .WithMany(p => p.Books)
            .HasForeignKey(b => b.PublisherId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder
            .HasMany(b => b.Tags)
            .WithMany(t => t.Books)
            .UsingEntity<Dictionary<string, object>>(
                "BookTags",
                j => j
                    .HasOne<Tag>()
                    .WithMany()
                    .HasForeignKey("TagsId"),
                j => j
                    .HasOne<Book>()
                    .WithMany()
                    .HasForeignKey("BooksId"),
                j =>
                {
                    j.HasKey("BooksId", "TagsId");   // ✅ این خط خیلی مهمه
                    j.ToTable("BookTags");
                });
        
        modelBuilder.HasData(SeedData.SeedData.Books);
    }
}