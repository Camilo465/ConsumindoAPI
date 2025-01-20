using ApiClient.RefitInterfaces;
using Arguments.Entities.Fipe;
using BaseLibrary.Arguments;
using BaseLibrary.Services;
using Newtonsoft.Json;

namespace Application.Interfaces.Entities
{
    public class FipeService(IFipeRefit refit) : BaseService<IFipeRefit>(refit), IFipeService
    {
        public async Task<BaseApiResponse<List<OutputByFipe>, string>> GetByFipe(string fipe)
        {
            var response = await _refit!.GetByFipe(fipe);
            return ReturnResponse<List<OutputByFipe>, string>(response);
        }

        public async Task<BaseApiResponse<List<GetAll>, string>> GetAll()
        {
            var response = await _refit!.GetAll();
            return ReturnResponse<List<GetAll>, string>(response);
        }

        public async Task<BaseApiResponse<List<OutputByVehicleType>, string>> GetByVehicleType(string vehicleType)
        {
            var response = await _refit!.GetByVehicleType(vehicleType);
            return ReturnResponse<List<OutputByVehicleType>, string>(response);
        }
    }
}
