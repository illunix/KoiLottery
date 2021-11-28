using GenerateMediator;
using KoiLottery.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KoiLottery.Features.Lotteries
{
    [GenerateMediator]
    public static partial class Get
    {
        public sealed partial record Query;

        public record Lottery(
            Guid Id,
            string Name,
            decimal ParticipationCost,
            int Duration,
            DateTime CreatedAt,
            int ParticipantsCount,
            decimal Pool
        );

        public static async Task<IReadOnlyList<Lottery>> QueryHandler(
            ApplicationDbContext context
        )
        {
            var lotteries = await context.Lotteries
                .Include(q => q.LotteryPayments)
                .Include(q => q.LotteryParticipants)
                .OrderByDescending(q => q.LotteryParticipants.Count())
                .Select(q => new Lottery(
                    q.Id,
                    q.Name,
                    q.ParticipationCost,
                    q.Duration,
                    q.CreatedAt,
                    q.LotteryParticipants.Count(),
                    q.LotteryPayments.Count() * q.LotteryParticipants.Count()
                 ))
                .ToListAsync();

            return lotteries;
        }
    }
}
