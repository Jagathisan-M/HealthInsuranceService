using System;
using System.Collections.Generic;

namespace HealthInsuranceService.HealthInsuranceDBContext;

public partial class PaymentCycle
{
    public int PaymentCycleId { get; set; }

    public string CycleDescription { get; set; } = null!;

    public virtual ICollection<AcquirerPlan> AcquirerPlans { get; set; } = new List<AcquirerPlan>();

    public virtual ICollection<PaymentSchedule> PaymentSchedules { get; set; } = new List<PaymentSchedule>();
}
