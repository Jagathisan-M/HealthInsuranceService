using System;
using System.Collections.Generic;

namespace HealthInsuranceService.HealthInsuranceDBContext;

public partial class PaymentSchedule
{
    public long PaymentScheduleId { get; set; }

    public long UserDetailId { get; set; }

    public long? AcquirerPlanId { get; set; }

    public int PaymentCycleId { get; set; }

    public DateTime? PremiumDate { get; set; }

    public decimal? PremiumAmount { get; set; }

    public bool? IsPaid { get; set; }

    public DateTime? PaymentDate { get; set; }

    public virtual AcquirerPlan? AcquirerPlan { get; set; }

    public virtual PaymentCycle PaymentCycle { get; set; } = null!;

    public virtual UserDetail UserDetail { get; set; } = null!;
}
