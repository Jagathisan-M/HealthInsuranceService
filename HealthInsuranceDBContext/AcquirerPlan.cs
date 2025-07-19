using System;
using System.Collections.Generic;

namespace HealthInsuranceService.HealthInsuranceDBContext;

public partial class AcquirerPlan
{
    public long AcquirerPlanId { get; set; }

    public long UserDetailId { get; set; }

    public long InsurancePlanId { get; set; }

    public int PaymentCycleId { get; set; }

    public DateTime CreatedOn { get; set; }

    public virtual InsurancePlan InsurancePlan { get; set; } = null!;

    public virtual PaymentCycle PaymentCycle { get; set; } = null!;

    public virtual ICollection<PaymentSchedule> PaymentSchedules { get; set; } = new List<PaymentSchedule>();

    public virtual UserDetail UserDetail { get; set; } = null!;
}
