using CafeShop.Database.Common;
using CafeShop.Database.Interfaces;

namespace CafeShop.Database.Models {
    public class Product : MerchantEntity, IUpdateEntity<Product>, IDeleteEntity {
        public string Name { get; set; } = null!;
        public int Price { get; set; }
        public bool IsActive { get; set; }

        public Guid CategoryId { get; set; }

        public virtual Category? Category { get; set; }
        public virtual ICollection<OrderDetail>? OrderDetails { get; set; }

        public void Delete(Guid userId) {
            this.IsDelete = true;
            this.ModifiedBy = userId;
            this.ModifiedDate = DateTimeOffset.UtcNow;
        }

        public void Update(Product other) {
            this.Name = other.Name;
            this.Price = other.Price;
            this.IsActive = other.IsActive;
            this.CategoryId = other.CategoryId;
            this.ModifiedBy = other.ModifiedBy;
            this.ModifiedDate = other.ModifiedDate;
        }
    }
}