namespace CafeShop.Models.Dto {
    public class TableDto {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}