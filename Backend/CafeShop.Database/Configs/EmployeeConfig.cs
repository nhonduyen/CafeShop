using CafeShop.Database.Common;
using CafeShop.Database.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CafeShop.Database.Configs {
    internal class EmployeeConfig : MerchantEntityConfig<Employee> {

        protected override void Config(EntityTypeBuilder<Employee> builder) {
            builder.Property(o => o.Username).HasMaxLength(50).IsRequired();
            builder.Property(o => o.Password).HasMaxLength(1000).IsRequired();
            builder.Property(o => o.Name).HasMaxLength(200).IsRequired();
        }
    }
}