using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MobileStore.Infrastructure.Entities;

namespace MobileStore.Infrastructure.EntityConfigurations
{
    internal class ProductContentConfigurations : IEntityTypeConfiguration<ProductContent>
    {
        public void Configure(EntityTypeBuilder<ProductContent> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(p => p.Product)
                .WithMany(x => x.Contents)
                .HasForeignKey(p => p.ContentId);
        }
    }
}
