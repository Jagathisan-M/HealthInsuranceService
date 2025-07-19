using System;
using System.Collections.Generic;

namespace HealthInsuranceService.HealthInsuranceDBContext;

public partial class InsurancePlan
{
    public long InsurancePlanId { get; set; }

    public string InsuranceName { get; set; } = null!;

    public decimal SumAssured { get; set; }

    public decimal? PremiumPercentage { get; set; }

    public decimal AdditionalPercentage { get; set; }

    public DateTime CreatedOn { get; set; }

    public long CreatedBy { get; set; }

    public DateTime ModifiedOn { get; set; }

    public long ModifiedBy { get; set; }

    public virtual ICollection<AcquirerPlan> AcquirerPlans { get; set; } = new List<AcquirerPlan>();
}
