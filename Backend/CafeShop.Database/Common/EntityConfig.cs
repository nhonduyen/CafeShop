using CafeShop.Database.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CafeShop.Database.Common {
    internal abstract class EntityConfig<TEntity> : IEntityTypeConfiguration<TEntity>
        where TEntity : class, IEntity {

        public void Configure(EntityTypeBuilder<TEntity> builder) {
            builder.ToTable(typeof(TEntity).Name);
            builder.HasKey(o => o.Id);

            Config(builder);
        }

        protected abstract void Config(EntityTypeBuilder<TEntity> builder);
    }
}