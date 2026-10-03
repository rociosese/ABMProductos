using CSRPractica.Models.DTOs.Requests;
using CSRPractica.Models.DTOs.Responses;

namespace CSRPractica.Services.Interfaces
{
    public interface IProductService
    {
        List<ProductForReadDto> GetAllProducts();
        ProductForReadDto? GetProductById(int id);
        ProductForReadDto CreateProduct(ProductForCreateDto dto);
        void UpdateProduct(int id, ProductForUpdateDto dto);
        void DeleteProduct(int id);

        List<ProductForReadDto> SearchProductsByName(string name);

        ProductStatsDto GetStats();

        bool ProductNameExists(string name);
    }
}