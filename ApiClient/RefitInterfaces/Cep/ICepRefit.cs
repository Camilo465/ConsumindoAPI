using Refit;


namespace ApiClient.RefitInterfaces
{
    public interface ICepRefit
    {
        [Get("/api/cep/v1/{postalCode}")]
        Task<ApiResponse<string>> GetByPostalCode(string postalCode);
    }
}
