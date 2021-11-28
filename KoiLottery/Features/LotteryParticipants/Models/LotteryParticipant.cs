using KoiLottery.Features.Wallets.Models;
using KoiLottery.Infrastructure.Models;
using System;

namespace KoiLottery.Features.LotteryParticipants.Models
{
    public record LotteryParticipant(
        Guid LotteryId,
        Guid WalletId
    ) : BaseEntity
    {
        public Wallet Wallet { get; init; }
    }
}
