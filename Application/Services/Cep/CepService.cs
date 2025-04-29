using ApiClient.RefitInterfaces;
using Arguments.Entities.Cep;
using BaseLibrary.Arguments;
using BaseLibrary.Services;

namespace Application.Interfaces.Entities
{
    public class CepService(ICepRefit refit) : BaseService<ICepRefit>(refit), ICepService
    {
        public async Task<BaseApiResponse<OutputByPostalCode, string>> GetByPostalCode(string postalCode)
        {
            var response = await _refit!.GetByPostalCode(postalCode);
            return ReturnResponse<OutputByPostalCode, string>(response);
        }
    }
}
