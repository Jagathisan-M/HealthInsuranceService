using System;
using System.Collections.Generic;

namespace HealthInsuranceAPI.HealthInsuranceDBContext;

public partial class UserDetail
{
    public long UserDetailId { get; set; }

    public bool? IsIssuer { get; set; }

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string? Email { get; set; }

    public string? Address { get; set; }

    public long PhoneNumber { get; set; }

    public bool? IsSmoker { get; set; }

    public virtual ICollection<AcquirerPlan> AcquirerPlans { get; set; } = new List<AcquirerPlan>();

    public virtual ICollection<PaymentSchedule> PaymentSchedules { get; set; } = new List<PaymentSchedule>();
}
