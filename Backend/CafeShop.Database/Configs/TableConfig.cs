using CafeShop.Database.Common;
using CafeShop.Database.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CafeShop.Database.Configs {
    internal class TableConfig : MerchantEntityConfig<Table> {

        protected override void Config(EntityTypeBuilder<Table> builder) {
            builder.Property(o => o.Name).HasMaxLength(200).IsRequired();

            builder.HasIndex(o => new { o.MerchantId, o.Name }).IsUnique();

            builder.HasMany(o => o.Orders).WithOne(o => o.Table).HasForeignKey(o => o.TableId);
        }
    }
}