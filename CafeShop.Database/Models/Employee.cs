using CafeShop.Database.Common;

namespace CafeShop.Database.Models {
    public class Employee : MerchantEntity {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;

        public string Name { get; set; } = null!;
        public bool IsActive { get; set; } = true;
        public bool IsOwner { get; set; }
    }
}