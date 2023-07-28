using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MobileStore.Infrastructure.Entities;

namespace MobileStore.Infrastructure.EntityConfigurations 
{
    internal class CartItemConfigurations : IEntityTypeConfiguration<CartItem>
    {
        public void Configure(EntityTypeBuilder<CartItem> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
