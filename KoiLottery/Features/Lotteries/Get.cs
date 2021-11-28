using GenerateMediator;
using KoiLottery.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
            decimal ParticipationCostInETH,
            decimal ParticipationCostInUSD,
            int Duration,
            DateTime CreatedAt,
            int ParticipantsCount,
            decimal PoolInETH,
            decimal PoolInUSD
        );

        public record Root
        {
            public Result result { get; init; }
        }

        public record Result
        {
            public decimal ethusd { get; init; }
        }

        public static async Task<IReadOnlyList<Lottery>> QueryHandler(
            ApplicationDbContext context,
            IHttpClientFactory clientFactory
        )
        {
            var client = clientFactory.CreateClient();

            var response = await client.SendAsync(new(
                HttpMethod.Get,
                "https://api.etherscan.io/api?module=stats&action=ethprice&apikey=U4ZKZ5KSHIWKYG5KJ6VVWK8A5ZBBE6JYQP"
            ));

            var etherscan = new Root();

            if (response.IsSuccessStatusCode)
            {
                var responseBody = await response.Content.ReadAsStringAsync();
                etherscan = JsonConvert.DeserializeObject<Root>(responseBody);
            }

            var etherPriceUSD = etherscan.result.ethusd;

            var lotteries = await context.Lotteries
                .Include(q => q.LotteryPayments)
                .Include(q => q.LotteryParticipants)
                .OrderByDescending(q => q.LotteryParticipants.Count())
                .Select(q => new Lottery(
                    q.Id,
                    q.Name,
                    q.ParticipationCost,
                    etherPriceUSD * q.ParticipationCost,
                    q.Duration,
                    q.CreatedAt,
                    q.LotteryParticipants.Count(),
                    q.LotteryPayments.Count() * q.LotteryParticipants.Count(),
                    etherPriceUSD * q.LotteryPayments.Count() * q.LotteryParticipants.Count()
                 ))
                .ToListAsync();

            return lotteries;
        }
    }
}
