using Entities.Models;
using Services.Contracts;
using Store.Repositories.Contracts;

namespace Services;
public class ProductManager : IProductService
{
    private readonly IRepositoryManager _repositoryManager;

    public ProductManager(IRepositoryManager repositoryManager)
    {
        _repositoryManager = repositoryManager;
    }

    public void CreateProduct(Product product)
    {
        _repositoryManager.Product.Create(product);
        _repositoryManager.Save();
    }

    public void DeleteOneProduct(int id)
    {
        Product product= GetOneProduct(id,false);
        if (product != null)
        {
            _repositoryManager.Product.DeleteOneProduct(product);
            _repositoryManager.Save();

        }
    }

    public IEnumerable<Product> GetAllProducts(bool trackChanges)
    {
        return _repositoryManager.Product.GetAllProducts(trackChanges);
    }

    public Product? GetOneProduct(int id, bool trackChanges)
    {
        var product = _repositoryManager.Product.GetOneProduct(id, trackChanges);
        if (product == null)
            throw new Exception("Product not found!");
        return product;
    }

    public void UpdateOneProduct(Product product)
    {
        var entity = _repositoryManager.Product.GetOneProduct(product.ProductId, true);
        entity.ProductName= product.ProductName;
        entity.Price= product.Price;
        _repositoryManager.Save();
        
    }
}
