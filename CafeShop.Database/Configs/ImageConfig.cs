using CafeShop.Database.Common;
using CafeShop.Database.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CafeShop.Database.Configs {
    internal class ImageConfig : MerchantEntityConfig<Image> {

        protected override void Config(EntityTypeBuilder<Image> builder) {
            builder.Property(o => o.Name).HasMaxLength(255).IsRequired();
            builder.Property(o => o.Path).HasMaxLength(4000);

            builder.HasIndex(o => new { o.MerchantId, o.ItemType });
            builder.HasIndex(o => new { o.MerchantId, o.ItemType, o.ItemId });
        }
    }
}