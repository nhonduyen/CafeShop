using CafeShop.Database.Common;

namespace CafeShop.Database.Models {
    public class Table : MerchantEntity {
        public string Name { get; set; } = null!;
        public bool IsActive { get; set; } = true;

        public virtual ICollection<Order>? Orders { get; set; }
    }
}