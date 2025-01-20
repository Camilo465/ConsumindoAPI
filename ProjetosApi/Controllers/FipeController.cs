using Application.Interfaces;
using Arguments.Entities.Fipe;
using BaseLibrary.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ProjetosApi.Controllers
{
    [Route("api/[controller]")]    
    [ApiController]
    [Authorize]
    public class FipeController(IFipeService service) : BaseController<IFipeService>(service)
    {
        [HttpGet("GetAll")]
        public async Task<ActionResult<List<GetAll>>> GetAll()
        {
            return await ResponseAsync(await _service!.GetAll());
        }
        [HttpGet("GetByFipe/{fipe}")]
        public async Task<ActionResult<List<OutputByFipe>>> GetByFipe(string fipe)
        {
            return await ResponseAsync(await _service!.GetByFipe(fipe));
        }
        [HttpGet("GetByVehicleType/{vehicleType}")]
        public async Task<ActionResult<List<OutputByVehicleType>>> GetByVehicleType(string vehicleType)
        {
            return await ResponseAsync(await _service!.GetByVehicleType(vehicleType));
        }
    }
}
