using AwesomeBlog.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AwesomeBlog.Infrastructure.Persistence.Configurations
{
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.ToTable(nameof(Post));

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
                .Property(entity => entity.Title)
                .HasColumnName("title")
                .IsRequired()
                .HasMaxLength(250);
            
            builder
                .HasIndex(entity => entity.Title);

            builder
                .Property(entity => entity.Content)
                .HasColumnName("content")
                .IsRequired()
                .HasColumnType("text");

            builder
                .HasOne(post => post.Category)
                .WithMany(category => category.Posts)
                .HasForeignKey(entity => entity.CategoryId);

            builder
                .Property(entity => entity.CategoryId)
                .HasColumnName("category_id")
                .HasMaxLength(36)
                .IsRequired();

            builder
                .Property(entity => entity.CreatedAt)
                .IsRequired()
                .HasColumnName("created_at");

            builder
                .Property(entity => entity.CreatedBy)
                .IsRequired()
                .HasColumnName("created_by");

            builder
                .Property(entity => entity.LastModifiedAt)
                .HasColumnName("last_modified_at");

            builder
                .Property(entity => entity.LastModifiedBy)
                .HasColumnName("last_modified_by");

            builder
                .Property(entity => entity.IsDeleted)
                .HasColumnName("is_deleted");

            builder.HasQueryFilter(entity => !entity.IsDeleted);
        }
    }
}
