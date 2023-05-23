using CafeShop.CommonServices;
using CafeShop.Database.Models;

namespace CafeShop.Models.Dto {
    public class ProductDto {
        public Guid Id { get; set; }
        public ImageDto? Image { get; set; }
        public CategoryDto Category { get; set; } = null!;
        public string Name { get; set; } = string.Empty;
        public int Price { get; set; }
        public bool IsActive { get; set; }

        public ProductDto LoadImage(Image? image, string? url) {
            this.Image = ImageDto.FromEntity(image, url);
            return this;
        }
    }
}