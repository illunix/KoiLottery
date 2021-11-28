using System;

namespace KoiLottery.Infrastructure.Models
{
    public record BaseEntity
    {
        public Guid Id { get; init; }
    }
}
