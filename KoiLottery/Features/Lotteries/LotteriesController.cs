using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KoiLottery.Features.Lotteries
{
    [Route("api/[controller]")]
    public partial class LotteriesController : Controller
    {
        private readonly IMediator _mediator;

        [HttpGet]
        public async Task<IActionResult> Get(Get.Query query)
            => Ok(await _mediator.Send(query));
    }
}
