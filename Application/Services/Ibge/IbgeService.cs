using ApiClient.RefitInterfaces;

using Arguments.Entities.Fipe;
using Arguments.Entities.Ibge;
using BaseLibrary.Arguments;
using BaseLibrary.Services;
using Newtonsoft.Json;

namespace Application.Interfaces.Entities
{
    public class IbgeService(IIbgeRefit refit) : BaseService<IIbgeRefit>(refit), IIbgeService
    {
        public async Task<BaseApiResponse<List<OutPutGetAll>, string>> GetAll()
        {
            var response = await _refit!.GetAll();
            return ReturnResponse<List<OutPutGetAll>, string>(response);
        }

        public async Task<BaseApiResponse<List<OutputGetByUf>, string>> GetByUf(string uf)
        {
            var response = await _refit!.GetByUf(uf);
            return ReturnResponse<List<OutputGetByUf>, string>(response);
        }

        public async Task<BaseApiResponse<OutputGetByCodeIbge, string>> GetByCode(string code)
        {
            var response = await _refit!.GetByCode(code);
            return ReturnResponse<OutputGetByCodeIbge, string>(response);
        }
    }
}
