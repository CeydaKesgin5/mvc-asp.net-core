using Entities.Models;

namespace Repositories.Contracts{
    public interface IProductRepository: IRepositoryBase<Product>{
        IQueryable<Product> GetAllProducts(bool trackChanges);
        IQueryable<Product> GetShowCaseProducts(bool trackChanges);

        public Product? GetOneProduct(int id, bool trackChanges);
        void CreateProduct(Product product);
        void DeleteOneProduct(Product product);
        void UpdateOneProduct(Product entity);




    }
}