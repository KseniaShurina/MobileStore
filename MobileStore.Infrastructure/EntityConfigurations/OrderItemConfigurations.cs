using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MobileStore.Infrastructure.Entities;

namespace MobileStore.Infrastructure.EntityConfigurations
{
    internal class OrderItemConfigurations : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(i => i.Order)
                .WithMany(p => p.Items)
                .HasForeignKey(x => x.OrderId);
        }
    }
}
