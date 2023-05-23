using CafeShop.Database.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CafeShop.Database.Common {
    internal abstract class MerchantEntityConfig<TEntity> : IEntityTypeConfiguration<TEntity>
        where TEntity : class, IMerchantEntity {

        public void Configure(EntityTypeBuilder<TEntity> builder) {
            builder.ToTable(typeof(TEntity).Name);
            builder.HasKey(o => o.Id);

            builder.HasIndex(o => o.MerchantId);
            builder.HasIndex(o => new { o.MerchantId, o.IsDelete });

            Config(builder);
        }

        protected abstract void Config(EntityTypeBuilder<TEntity> builder);
    }
}