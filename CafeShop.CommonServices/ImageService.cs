using CafeShop.Common.Exceptions;
using CafeShop.Database;
using CafeShop.Database.Enums;
using CafeShop.Database.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

namespace CafeShop.CommonServices {

    public interface IImageService {

        Task<List<Image>> List(Guid merchantId, EImage itemType, Guid itemId, CancellationToken cancellationToken = default);

        Task<List<Image>> List(Guid merchantId, EImage itemType, List<Guid> itemIds, CancellationToken cancellationToken = default);

        Task Delete(Guid id, Image? entity, CancellationToken cancellationToken = default);

        Task Save(Guid merchantId, Guid userId, EImage itemType, Guid itemId, ImageDto model, Image? entity = null, CancellationToken cancellationToken = default);
    }

    public class ImageDto {

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;
        public string Path { get; set; } = string.Empty;

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public byte[]? Data { get; set; }

        [return: NotNullIfNotNull(nameof(entity))]
        public static ImageDto? FromEntity(Image? entity, string? url) {
            if (entity == null) return default;

            return new ImageDto {
                Id = entity.Id,
                Name = entity.Name,
                Path = $"{url}/{entity.Path}",
            };
        }
    }

    public class ImageService : IImageService {
        private readonly CafeShopContext db;

        public ImageService(IServiceProvider serviceProvider) {
            this.db = serviceProvider.GetRequiredService<CafeShopContext>();
        }

        public async Task<List<Image>> List(Guid merchantId, EImage itemType, Guid itemId, CancellationToken cancellationToken = default) {
            return await this.List(merchantId, itemType, new List<Guid> { itemId }, cancellationToken);
        }

        public async Task<List<Image>> List(Guid merchantId, EImage itemType, List<Guid> itemIds, CancellationToken cancellationToken = default) {
            return await this.db.Images.Where(o => o.MerchantId == merchantId && o.ItemType == itemType && itemIds.Contains(o.ItemId)).ToListAsync(cancellationToken);
        }

        public async Task Delete(Guid id, Image? entity, CancellationToken cancellationToken = default) {
            entity ??= await this.db.Images.FirstOrDefaultAsync(o => o.Id == id, cancellationToken);
            ManagedException.ThrowIfNull(entity, "No image found.");

            if (File.Exists(entity.Path)) File.Delete(entity.Path);

            this.db.Images.Remove(entity);
            await this.db.SaveChangesAsync(cancellationToken);
        }

        public async Task Save(Guid merchantId, Guid userId, EImage itemType, Guid itemId, ImageDto model, Image? entity = null, CancellationToken cancellationToken = default) {
            if (model.Data == null || model.Data.Length == 0)
                return;

            if (model.Id == Guid.Empty) {
                await Create(merchantId, userId, itemType, itemId, model, cancellationToken);
                return;
            }
            await this.Update(userId, model, entity, cancellationToken);
        }

        private async Task Create(Guid merchantId, Guid userId, EImage itemType, Guid itemId, ImageDto model, CancellationToken cancellationToken = default) {
            var entity = new Image() {
                Id = Guid.NewGuid(),
                MerchantId = merchantId,
                ItemType = itemType,
                ItemId = itemId,
                Name = model.Name,
                ModifiedBy = userId,
            };
            entity.Path = await UploadImage(entity, model.Data!, cancellationToken);

            await this.db.AddAsync(entity, cancellationToken);
            await this.db.SaveChangesAsync(cancellationToken);
        }

        private async Task Update(Guid userId, ImageDto model, Image? entity, CancellationToken cancellationToken = default) {
            entity ??= await this.db.Images.FirstOrDefaultAsync(o => o.Id == model.Id, cancellationToken);
            ManagedException.ThrowIfNull(entity, "No image found.");

            entity.Name = model.Name;
            entity.Path = await UploadImage(entity, model.Data!, cancellationToken);
            entity.ModifiedBy = userId;
            entity.ModifiedDate = DateTimeOffset.UtcNow;

            this.db.Images.Update(entity);
            await this.db.SaveChangesAsync(cancellationToken);
        }

        private static async Task<string> UploadImage(Image entity, byte[] data, CancellationToken cancellationToken = default) {
            var (directories, filename) = GetFilePath(entity);

            foreach (var directory in directories)
                if (!Directory.Exists(directory)) Directory.CreateDirectory(directory);

            await File.WriteAllBytesAsync(filename, data, cancellationToken);

            return filename;
        }

        public static (string[], string) GetFilePath(Image entity) {
            string fileType = "images";
            string imageType = entity.ItemType switch {
                EImage.Product => "product",
                _ => "other",
            };
            string extentions = Path.GetExtension(entity.Name);

            string[] directories = new string[] {
                $"{fileType}/{entity.MerchantId}",
                $"{fileType}/{entity.MerchantId}/{imageType}"
            };

            return (directories, $"{fileType}/{entity.MerchantId}/{imageType}/{entity.ItemId}_{entity.Id}{extentions}");
        }
    }
}