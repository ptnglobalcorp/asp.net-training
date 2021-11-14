using AwesomeBlog.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AwesomeBlog.Infrastructure.Persistence.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable(nameof(Category));

            builder
                .Property(entity => entity.ClusteredKey)
                .HasColumnName("clustered_key")
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder
                .HasIndex(entity => entity.ClusteredKey)
                .IsClustered();

            builder
                .Property(entity => entity.Id)
                .HasColumnName("id")
                .HasMaxLength(36)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder
                .HasKey(entity => entity.Id)
                .IsClustered(false);

            builder
                .Property(entity => entity.Name)
                .HasColumnName("name")
                .IsRequired()
                .HasMaxLength(100);

            builder
                .HasIndex(entity => entity.Name);

            builder
                .Property(entity => entity.Description)
                .HasColumnName("description")
                .HasMaxLength(250);

            builder
                .Property(entity => entity.CreatedAt)
                .IsRequired()
                .HasColumnName("created_at");

            builder
                .Property(entity => entity.LastModifiedAt)
                .HasColumnName("last_modified_at");

            builder
                .Property(entity => entity.IsDeleted)
                .HasColumnName("is_deleted");

            builder.HasQueryFilter(entity => !entity.IsDeleted);
        }
    }
}
