using AutoMapper;
using Entities.DTOs;
using Entities.Models;
using Entities.RequestParameters;
using Services.Contracts;
using Store.Repositories.Contracts;

namespace Services;
public class ProductManager : IProductService
{
    private readonly IRepositoryManager _repositoryManager;
    private readonly IMapper _mapper;
    public ProductManager(IRepositoryManager repositoryManager, IMapper mapper)
    {
        _repositoryManager = repositoryManager;
        _mapper = mapper;
    }

    public void CreateProduct(ProductDtoForInsertion productDto)
    {
        //Product product = new Product()
        //{
        //    ProductName = productDto.ProductName,
        //    Price = productDto.Price,
        //    CategoryId=productDto.CategoryId
        //};
        Product product = _mapper.Map<Product>(productDto);
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

    public IEnumerable<Product> GetAllProductsWithDetails(ProductRequestParameters p)
    {
        return _repositoryManager.Product.GetAllProductsWithDetails(p);
    }

    public Product? GetOneProduct(int id, bool trackChanges)
    {
        var product = _repositoryManager.Product.GetOneProduct(id, trackChanges);
        if (product == null)
            throw new Exception("Product not found!");
        return product;
    }

    public ProductDtoForUpdate GetOneProductForUpdate(int id, bool trackchanges)
    {
        var product = GetOneProduct(id, false);
        var productDto= _mapper.Map<ProductDtoForUpdate>(product);
        return productDto;
    }

    public IEnumerable<Product> GetShowCaseProducts(bool trackChanges)
    {
        var products = _repositoryManager.Product.GetShowCaseProducts(trackChanges);
        return products;
    }

    public void UpdateOneProduct(ProductDtoForUpdate productDto)
    {
        //var entity = _repositoryManager.Product.GetOneProduct(productDto.ProductId, true);
        //entity.ProductName= productDto.ProductName;
        //entity.Price= productDto.Price;
        //entity.CategoryId= productDto.CategoryId;
        var entity= _mapper.Map<Product>(productDto);
        _repositoryManager.Product.UpdateOneProduct(entity);
        _repositoryManager.Save();
        
    }
}
