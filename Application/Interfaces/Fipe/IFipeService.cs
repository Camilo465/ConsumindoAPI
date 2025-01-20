
using Arguments.Entities.Fipe;
using BaseLibrary.Arguments;

namespace Application.Interfaces
{
    public interface IFipeService
    {
        Task<BaseApiResponse<List<OutputByVehicleType>,string>> GetByVehicleType(string vehicleType);
        Task<BaseApiResponse<List<OutputByFipe>,string>> GetByFipe(string fipe);
        Task<BaseApiResponse<List<GetAll>,string>> GetAll();
    }
}
