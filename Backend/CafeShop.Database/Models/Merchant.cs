using CafeShop.Database.Common;

namespace CafeShop.Database.Models {
    public class Merchant : Entity {
        public string Code { get; set; } = null!;
        public string Name { get; set; } = null!;

        public bool IsActive { get; set; }
        public DateTimeOffset ExpiredDate { get; set; } = DateTimeOffset.UtcNow;
    }
}