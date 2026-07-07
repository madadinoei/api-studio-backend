using ApiStudio.Domain.Entities;
using ApiStudio.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ApiStudio.Persistence.Configurations;

internal class ApiRequestConfiguration : IEntityTypeConfiguration<ApiRequest>
{
    public void Configure(EntityTypeBuilder<ApiRequest> builder)
    {
        builder.ToTable("ApiRequests");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.Method)
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.HasOne(x => x.Collection)
            .WithMany()
            .HasForeignKey(x => x.CollectionId);


        builder.OwnsOne(x => x.Endpoint, endpoint =>
        {
            endpoint.Property(x => x.Value)
                .HasColumnName("Endpoint")
                .HasMaxLength(2000)
                .IsRequired();
        });

        builder.OwnsOne(x => x.Body, body =>
        {
            body.Property(x => x.Type)
                .HasConversion<string>()
                .HasMaxLength(20);

            body.Property(x => x.Content)
                .HasColumnType("nvarchar(max)");
        });

        builder.OwnsMany(x => x.Headers, header =>
        {
            header.ToTable("ApiRequestHeaders");

            header.WithOwner()
                .HasForeignKey("ApiRequestId");

            header.Property<int>("Id");

            header.HasKey("Id");

            header.Property(x => x.Key)
                .HasMaxLength(200);

            header.Property(x => x.Value)
                .HasMaxLength(2000);

            header.Property(x => x.Enabled);
        });


        builder.OwnsMany(x => x.QueryParameters, query =>
        {
            query.ToTable("ApiRequestQueryParameters");

            query.WithOwner()
                .HasForeignKey("ApiRequestId");

            query.Property<int>("Id");

            query.HasKey("Id");

            query.Property(x => x.Name);

            query.Property(x => x.Value);

            query.Property(x => x.Enabled);
        });


        builder.Navigation(x => x.Headers)
            .UsePropertyAccessMode(PropertyAccessMode.Field);

        builder.Navigation(x => x.QueryParameters)
            .UsePropertyAccessMode(PropertyAccessMode.Field);
    }
}