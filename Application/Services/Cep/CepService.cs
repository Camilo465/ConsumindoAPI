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

            if (string.IsNullOrWhiteSpace(response.Content) || !IsJson(response.Content))
            {
                return ReturnError<OutputByPostalCode>("Error, Invalid Postal Code or unexpected response format");
            }

            return ReturnResponse<OutputByPostalCode, string>(response);
        }
        private bool IsJson(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return false;

            input = input.Trim();
            return (input.StartsWith("{") && input.EndsWith("}")) || (input.StartsWith("[") && input.EndsWith("]"));
        }
        private BaseApiResponse<TTypeResult, string> ReturnError<TTypeResult>(string errorMessage)
        {
            return new BaseApiResponse<TTypeResult, string>
            {
                Error = errorMessage
            };
        }
    }
}
