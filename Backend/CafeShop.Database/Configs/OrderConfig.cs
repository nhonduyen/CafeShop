using CafeShop.Database.Common;
using CafeShop.Database.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CafeShop.Database.Configs {
    internal class OrderConfig : MerchantEntityConfig<Order> {

        protected override void Config(EntityTypeBuilder<Order> builder) {
            builder.Property(o => o.OrderNo).HasMaxLength(50).IsRequired();

            builder.HasIndex(o => new { o.MerchantId, o.OrderNo }).IsUnique();
            builder.HasIndex(o => new { o.MerchantId, o.TableId });

            builder.HasOne(o => o.Table).WithMany(o => o.Orders).HasForeignKey(o => o.TableId);
            builder.HasMany(o => o.OrderDetails).WithOne(o => o.Order).HasForeignKey(o => o.OrderId);
        }
    }
}