using CafeShop.Database.Common;

namespace CafeShop.Database.Models {
    public class OrderDetail : MerchantEntity {
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; } = string.Empty;
        public int Price { get; set; }
        public int Quantity { get; set; }

        public long SubTotal { get; set; }

        public virtual Order? Order { get; set; }
        public virtual Product? Product { get; set; }
    }
}