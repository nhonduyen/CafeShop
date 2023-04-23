using CafeShop.Database.Interfaces;

namespace CafeShop.Database.Common {
    public class Entity : IEntity {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid ModifiedBy { get; set; }
        public DateTimeOffset ModifiedDate { get; set; } = DateTimeOffset.UtcNow;
    }
}