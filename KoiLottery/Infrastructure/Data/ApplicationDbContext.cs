using KoiLottery.Features.Lotteries.Models;
using KoiLottery.Features.LotteryParticipants.Models;
using KoiLottery.Features.Wallets.Models;
using KoiLottery.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace KoiLottery.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Lottery> Lotteries { get; set; }
        public DbSet<LotteryPayment> LotteryPayments { get; set; }
        public DbSet<LotteryParticipant> LotteryParticipants { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
