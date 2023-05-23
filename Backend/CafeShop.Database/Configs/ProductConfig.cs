using CafeShop.Database.Common;
using CafeShop.Database.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CafeShop.Database.Configs {
    internal class ProductConfig : MerchantEntityConfig<Product> {

        protected override void Config(EntityTypeBuilder<Product> builder) {
            builder.Property(o => o.Name).HasMaxLength(200).IsRequired();

            builder.HasOne(o => o.Category).WithMany(o => o.Products).HasForeignKey(o => o.CategoryId);
            builder.HasMany(o => o.OrderDetails).WithOne(o => o.Product).HasForeignKey(o => o.ProductId);
        }
    }
}