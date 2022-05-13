using Application.SectorOptions.Queries;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class SectorOptionsController : BaseApiController
    {
        [HttpGet]
        public async Task<IActionResult> GetSectorOptions()
        {
            return HandleResult(await Mediator.Send(new List.Query()));
        }
    }
}
