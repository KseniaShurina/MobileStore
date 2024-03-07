using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using MobileStore.Infrastructure.Entities;

namespace MobileStore.Infrastructure.EntityConfigurations
{
    internal class ProductContentConfigurations : IEntityTypeConfiguration<ProductContent>
    {
        public void Configure(EntityTypeBuilder<ProductContent> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(p => p.Product)
                .WithMany(p => p.Contents)
                .HasForeignKey(i => i.ProductId);
        }
    }
}
