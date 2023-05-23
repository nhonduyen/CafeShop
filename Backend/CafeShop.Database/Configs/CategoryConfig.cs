using CafeShop.Database.Common;
using CafeShop.Database.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CafeShop.Database.Configs {
    internal class CategoryConfig : MerchantEntityConfig<Category> {

        protected override void Config(EntityTypeBuilder<Category> builder) {
            builder.Property(o => o.Name).HasMaxLength(200).IsRequired();

            builder.HasIndex(o => new { o.MerchantId, o.Name }).IsUnique();

            builder.HasMany(o => o.Products).WithOne(o => o.Category).HasForeignKey(o => o.CategoryId);
        }
    }
}