using KoiLottery.Infrastructure.Models;

namespace KoiLottery.Features.Wallets.Models
{
    public record Wallet(
        string Address
    ) : BaseEntity;
}
