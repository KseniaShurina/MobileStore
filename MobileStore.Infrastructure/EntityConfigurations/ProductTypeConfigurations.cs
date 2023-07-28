using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MobileStore.Infrastructure.Entities;

namespace MobileStore.Infrastructure.EntityConfigurations
{
    internal class ProductTypeConfigurations : IEntityTypeConfiguration<ProductType>
    {
        public void Configure(EntityTypeBuilder<ProductType> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
