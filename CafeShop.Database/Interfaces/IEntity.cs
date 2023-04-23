namespace CafeShop.Database.Interfaces {

    public interface IEntity {
        public Guid Id { get; set; }
        public Guid ModifiedBy { get; set; }
        public DateTimeOffset ModifiedDate { get; set; }
    }
}