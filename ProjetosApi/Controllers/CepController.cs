using Application.Interfaces;
using Arguments.Entities.Cep;
using BaseLibrary.Arguments;
using BaseLibrary.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace ProjetosApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Microsoft.AspNetCore.Authorization.Authorize]
public class CepController(ICepService service) : BaseController<ICepService>(service)
{
    [HttpGet("GetByPostalCode/{postalCode}")]
    public async Task<ActionResult<BaseApiResponse<OutputByPostalCode, string>>> GetByPostalCode(string postalCode)
    {
        return await ResponseAsync(await _service!.GetByPostalCode(postalCode));
    }
}
