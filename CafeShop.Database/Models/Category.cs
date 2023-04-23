using CafeShop.Database.Common;

namespace CafeShop.Database.Models {
    public class Category : MerchantEntity {
        public string Name { get; set; } = null!;
        public bool IsActive { get; set; } = true;

        public virtual ICollection<Product>? Products { get; set; }
    }
}