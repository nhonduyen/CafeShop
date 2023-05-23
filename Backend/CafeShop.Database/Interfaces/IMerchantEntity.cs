namespace CafeShop.Database.Interfaces {

    public interface IMerchantEntity : IEntity {
        public Guid MerchantId { get; set; }
        public bool IsDelete { get; set; }
    }
}