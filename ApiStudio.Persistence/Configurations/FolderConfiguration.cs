using ApiStudio.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiStudio.Persistence.Configurations;

public sealed class FolderConfiguration
    : IEntityTypeConfiguration<Folder>
{
    public void Configure(EntityTypeBuilder<Folder> builder)
    {
        builder.ToTable("Folders");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.CollectionId)
            .IsRequired();

        builder.Property(x => x.ParentFolderId);

        builder.HasIndex(x => x.CollectionId);

        builder.HasIndex(x => x.ParentFolderId);

        builder.HasIndex(x => new
        {
            x.CollectionId,
            x.ParentFolderId,
            x.Name
        });

        builder.HasOne<Folder>()
            .WithMany()
            .HasForeignKey(x => x.ParentFolderId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}