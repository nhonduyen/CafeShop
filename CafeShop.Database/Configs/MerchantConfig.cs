using CafeShop.Database.Common;
using CafeShop.Database.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CafeShop.Database.Configs {
    internal class MerchantConfig : EntityConfig<Merchant> {

        protected override void Config(EntityTypeBuilder<Merchant> builder) {
            builder.Property(o => o.Code).HasMaxLength(50).IsRequired();
            builder.Property(o => o.Name).HasMaxLength(255).IsRequired();

            builder.HasIndex(o => o.Code).IsUnique();
        }
    }
}