using Arguments.Entities.Fipe;
using Refit;

namespace ApiClient.RefitInterfaces
{
    public interface IFipeRefit
    {
        [Get("/api/fipe/marcas/v1/{vehicleType}")]
        Task<ApiResponse<string>> GetByVehicleType(string vehicleType);
        [Get("/api/fipe/preco/v1/{fipe}")]
        Task<ApiResponse<string>> GetByFipe(string fipe);
        [Get("/api/fipe/tabelas/v1")]
        Task<ApiResponse<string>> GetAll();
    }
}
