using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace KoiLottery.Features.LotteryParticipants
{
    public partial class LotteryParticipantsController : Controller
    {
        private readonly IMediator _mediator;

        [HttpGet]
        public async Task<IActionResult> Get(Get.Query query)
            => Ok(await _mediator.Send(query));

        [HttpPost]
        public async Task<IActionResult> Post(Post.Command command)
        {
            var commandResult = await _mediator.Send(command);
            if (!commandResult.lotteryExist)
            {
                ModelState.AddModelError(
                    "lotteryId",
                    "Lottery does not exist."
                );

                return BadRequest(ModelState);
            }
            else if (!commandResult.walletExist)
            {
                ModelState.AddModelError(
                    "lotteryId",
                    "Wallet does not exist."
                );

                return BadRequest(ModelState);
            }

            return Ok();
        }
    }
}
