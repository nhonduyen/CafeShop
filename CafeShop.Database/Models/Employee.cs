using CafeShop.Database.Common;
using CafeShop.Database.Interfaces;

namespace CafeShop.Database.Models {
    public class Employee : MerchantEntity, IUpdateEntity<Employee>, IDeleteEntity {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;

        public string Name { get; set; } = null!;
        public bool IsActive { get; set; } = true;
        public bool IsOwner { get; set; }

        public void Delete(Guid userId) {
            this.IsDelete = true;
            this.ModifiedBy = userId;
            this.ModifiedDate = DateTimeOffset.UtcNow;
        }

        public void Update(Employee other) {
            this.Name = other.Name;
            this.IsActive = other.IsActive;
            this.ModifiedBy = other.ModifiedBy;
            this.ModifiedDate = other.ModifiedDate;
        }
    }
}