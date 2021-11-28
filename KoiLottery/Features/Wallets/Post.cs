using FluentValidation;
using GenerateMediator;
using KoiLottery.Features.Wallets.Models;
using KoiLottery.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Nethereum.Util;
using System.Linq;
using System.Threading.Tasks;

namespace KoiLottery.Features.Wallets
{
    [GenerateMediator]
    public static partial class Post
    {
        public sealed partial record Command(
            string Address
        )
        {
            public static void AddValidation(AbstractValidator<Command> v)
            {
                v.RuleFor(x => x.Address)
                    .NotEmpty().WithMessage("Please enter eth wallet address.")
                    .Custom((address, context) =>
                    {
                        if (!address.IsValidEthereumAddressHexFormat())
                        {
                            context.AddFailure("Invalid eth address.");
                        }
                    });
            }
        }

        public static async Task CommandHandler(
            Command command,
            ApplicationDbContext context
        )
        {
            var walletExist = await context.Wallets
                .Where(q => q.Address == command.Address)
                .AnyAsync();
            if (walletExist)
            {
                return;
            }

            var wallet = new Wallet(command.Address);

            context.Add(wallet);

            await context.SaveChangesAsync();
        }
    }
}
