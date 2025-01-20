
using Arguments.Entities.Cep;
using BaseLibrary.Arguments;

namespace Application.Interfaces
{
    public interface ICepService
    {
        Task<BaseApiResponse<OutputByPostalCode, string>> GetByPostalCode(string postalCode);
    }
}
