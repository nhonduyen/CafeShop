using CafeShop.Database.Common;

namespace CafeShop.Database.Models {
    public class Product : MerchantEntity {
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;
        public int Price { get; set; }

        public Guid CategoryId { get; set; }

        public virtual Category? Category { get; set; }
        public virtual ICollection<OrderDetail>? OrderDetails { get; set; }
    }
}