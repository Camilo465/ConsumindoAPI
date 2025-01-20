
using Arguments.Entities.Ibge;
using BaseLibrary.Arguments;

namespace Application.Interfaces
{
    public interface IIbgeService
    {
        Task<BaseApiResponse<List<OutPutGetAll>, string>> GetAll();
        Task<BaseApiResponse<OutputGetByCodeIbge, string>> GetByCode(string code);
        Task<BaseApiResponse<List<OutputGetByUf>, string>> GetByUf(string uf);
    }
}
