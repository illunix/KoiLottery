using FluentValidation;
using GenerateMediator;
using KoiLottery.Features.LotteryParticipants.Models;
using KoiLottery.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace KoiLottery.Features.LotteryParticipants
{
    [GenerateMediator]
    public static partial class Post
    {
        public sealed partial record Command(
            Guid LotteryId,
            Guid WalletId
        )
        {
            public static void AddValidation(AbstractValidator<Command> v)
            {
                v.RuleFor(x => x.LotteryId)
                    .NotEmpty().WithMessage("Please enter lottery id.");

                v.RuleFor(x => x.WalletId)
                    .NotEmpty().WithMessage("Please enter wallet id.");
            }
        }

        public record CommandResult(
            bool lotteryExist = true,
            bool walletExist = true
        );

        public static async Task<CommandResult> CommandHandler(
            Command command,
            ApplicationDbContext context
        )
        {
            var lotteryExist = await context.Lotteries
                .Where(q => q.Id == command.LotteryId)
                .AnyAsync();
            if (!lotteryExist)
            {
                return new(false);
            }

            var walletExist = await context.Wallets
                .Where(q => q.Id == command.WalletId)
                .AnyAsync();
            if (walletExist)
            {
                return new(true, false);
            }

            var lotteryParticipant = new LotteryParticipant(
                command.LotteryId,
                command.WalletId
            );

            context.Add(lotteryParticipant);

            await context.SaveChangesAsync();

            return new();
        }
    }
}
