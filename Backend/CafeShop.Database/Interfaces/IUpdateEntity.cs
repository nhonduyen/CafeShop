namespace CafeShop.Database.Interfaces {

    public interface IUpdateEntity<TEntity>
        where TEntity : class, IEntity {

        void Update(TEntity other);
    }
}