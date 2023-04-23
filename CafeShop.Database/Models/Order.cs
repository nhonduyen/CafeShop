using CafeShop.Database.Common;
using CafeShop.Database.Enums;

namespace CafeShop.Database.Models {
    public class Order : MerchantEntity {
        public EOrder Type { get; set; } = EOrder.DineIn;
        public string OrderNo { get; set; } = null!;
        public long Date { get; set; }

        public Guid? TableId { get; set; }
        public string TableName { get; set; } = string.Empty;

        public long SubTotal { get; set; }
        public long Discount { get; set; }
        public long TotalBill { get; set; }

        public Guid CreatedBy { get; set; }
        public DateTimeOffset CreatedDate { get; set; } = DateTimeOffset.UtcNow;

        public virtual Table? Table { get; set; }
        public virtual ICollection<OrderDetail>? OrderDetails { get; set; }
    }
}