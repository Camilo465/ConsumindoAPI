using Arguments.Entities.Ibge;
using Refit;

namespace ApiClient.RefitInterfaces
{
    public interface IIbgeRefit
    {
        [Get("/api/ibge/uf/v1")]
        Task<ApiResponse<string>> GetAll();
        [Get("/api/ibge/uf/v1/{code}")]
        Task<ApiResponse<string>> GetByCode(string code);
        [Get("/api/ibge/municipios/v1/{uf}?providers=dados-abertos-br,gov,wikipedia")]
        Task<ApiResponse<string>> GetByUf(string uf);
    }
}
