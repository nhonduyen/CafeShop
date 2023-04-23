using CafeShop.Database.Common;
using CafeShop.Database.Enums;

namespace CafeShop.Database.Models {
    public class Image : MerchantEntity {
        public EImage ItemType { get; set; }
        public Guid ItemId { get; set; }
        public string Name { get; set; } = null!;
        public string Path { get; set; } = string.Empty;
    }
}