using KoiLottery.Features.LotteryParticipants.Models;
using KoiLottery.Infrastructure.Models;
using System;
using System.Collections.Generic;

namespace KoiLottery.Features.Lotteries.Models
{
    public record Lottery(
        string Name,
        decimal ParticipationCost,
        int Duration,
        bool Repeat,
        DateTime CreatedAt
    ) : BaseEntity
    {
        public IReadOnlyList<LotteryPayment> LotteryPayments { get; init; }
        public IReadOnlyList<LotteryParticipant> LotteryParticipants { get; init; }
    }
}
