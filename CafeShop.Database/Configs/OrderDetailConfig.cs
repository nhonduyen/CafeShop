using CafeShop.Database.Common;
using CafeShop.Database.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CafeShop.Database.Configs {
    internal class OrderDetailConfig : MerchantEntityConfig<OrderDetail> {

        protected override void Config(EntityTypeBuilder<OrderDetail> builder) {
            builder.Property(o => o.ProductCode).HasMaxLength(50);
            builder.Property(o => o.ProductName).HasMaxLength(200);

            builder.HasIndex(o => new { o.MerchantId, o.OrderId });
            builder.HasIndex(o => new { o.MerchantId, o.ProductId });

            builder.HasOne(o => o.Order).WithMany(o => o.OrderDetails).HasForeignKey(o => o.OrderId);
            builder.HasOne(o => o.Product).WithMany(o => o.OrderDetails).HasForeignKey(o => o.ProductId);
        }
    }
}