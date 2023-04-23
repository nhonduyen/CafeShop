using CafeShop.Database.Interfaces;

namespace CafeShop.Database.Common {
    public class MerchantEntity : Entity, IMerchantEntity {
        public Guid MerchantId { get; set; }
        public bool IsDelete { get; set; }
    }
}