using Domain.Common;

namespace Domain.Entities;

public class SimulationEntity : BaseEntity<Guid>
{
    public string PhoneNumber { get; set; } = null!;
    public string SampleOTP { get; set; } = null!;
}

// @SimulationEntity
// - Id: Guid
// - PhoneNumber: string
// - SampleOTP: string