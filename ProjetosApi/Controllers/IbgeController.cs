using Application.Interfaces;
using Arguments.Entities.Ibge;
using BaseLibrary.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ProjetosApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class IbgeController(IIbgeService service) : BaseController<IIbgeService>(service)
    {
        [HttpGet("GetAll")]
        public async Task<ActionResult<List<OutPutGetAll>>> GetAll()
        {
            return await ResponseAsync(await _service!.GetAll());
        }
        [HttpGet("GetByCode/{code}")]
        public async Task<ActionResult<List<OutputGetByCodeIbge>>> GetByCode(string code)
        {
            return await ResponseAsync(await _service!.GetByCode(code));
        }
        [HttpGet("GetByUf/{uf}")]
        public async Task<ActionResult<OutputGetByUf>> GetByUf(string uf)
        {
            return await ResponseAsync(await _service!.GetByUf(uf));
        }
    }
}
