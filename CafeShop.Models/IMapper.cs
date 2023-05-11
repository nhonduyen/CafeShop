using CafeShop.Common.Models;
using CafeShop.Database.Interfaces;
using System.Diagnostics.CodeAnalysis;

namespace CafeShop.Models {

    public interface IMapper<TDto, TEntity>
        where TDto : class, new()
        where TEntity : class, IEntity {

        [return: NotNull]
        TDto FromEntity(TEntity entity);

        [return: NotNull]
        TEntity ToEntity(BaseModelReq<TDto> req);

        [return: NotNull]
        public List<TDto> FromEntities(List<TEntity> entities) {
            return entities.Select(entity => FromEntity(entity)).ToList();
        }
    }
}