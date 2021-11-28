using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KoiLottery.Features.Wallets
{
    [Route("api/[controller]")]
    public partial class WalletsController : Controller
    {
        private readonly IMediator _mediator;

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Post.Command command)
            => Ok(await _mediator.Send(command));
    }
}
