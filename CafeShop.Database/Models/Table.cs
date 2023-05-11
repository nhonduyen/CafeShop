using CafeShop.Database.Common;
using CafeShop.Database.Interfaces;

namespace CafeShop.Database.Models {
    public class Table : MerchantEntity, IUpdateEntity<Table>, IDeleteEntity {
        public string Name { get; set; } = null!;
        public bool IsActive { get; set; } = true;

        public virtual ICollection<Order>? Orders { get; set; }

        public void Delete(Guid userId) {
            this.IsDelete = true;
            this.ModifiedBy = userId;
            this.ModifiedDate = DateTimeOffset.UtcNow;
        }

        public void Update(Table other) {
            this.Name = other.Name;
            this.IsActive = other.IsActive;
            this.ModifiedBy = other.ModifiedBy;
            this.ModifiedDate = other.ModifiedDate;
        }
    }
}