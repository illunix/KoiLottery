using KoiLottery.Infrastructure.Models;
using System;

namespace KoiLottery.Features.Lotteries.Models
{
    public record LotteryPayment(
        Guid LotteryId,
        Guid WalletId
    ) : BaseEntity
    {
        public Lottery Lottery { get; init; }
    }
}
