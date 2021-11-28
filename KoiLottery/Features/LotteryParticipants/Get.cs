using KoiLottery.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using GenerateMediator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace KoiLottery.Features.LotteryParticipants
{
    [GenerateMediator]
    public static partial class Get
    {
        public sealed partial record Query(Guid LotteryId);

        public record QueryResult(
            IReadOnlyList<LotteryParticipant> LotteryParticipants
        );

        public record LotteryParticipant(
            string Address
        );

        public static async Task<QueryResult> QueryHandler(
            Query query,
            ApplicationDbContext context
        )
        {
            var lotteryExist = await context.Lotteries
                .Where(q => q.Id == query.LotteryId)
                .AnyAsync();
            if (!lotteryExist)
            {
                return new(null);
            }

            var lotteryParticipants = await context.LotteryParticipants
                .Where(q => q.LotteryId == query.LotteryId)
                .Include(q => q.Wallet)
                .Select(q => new LotteryParticipant(q.Wallet.Address))
                .ToListAsync();

            return new(lotteryParticipants);
        }
    }
}
