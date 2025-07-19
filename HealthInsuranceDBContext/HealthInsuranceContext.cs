using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HealthInsuranceService.HealthInsuranceDBContext;

public partial class HealthInsuranceContext : DbContext
{
    public HealthInsuranceContext()
    {
    }

    public HealthInsuranceContext(DbContextOptions<HealthInsuranceContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AcquirerPlan> AcquirerPlans { get; set; }

    public virtual DbSet<InsurancePlan> InsurancePlans { get; set; }

    public virtual DbSet<PaymentCycle> PaymentCycles { get; set; }

    public virtual DbSet<PaymentSchedule> PaymentSchedules { get; set; }

    public virtual DbSet<UserDetail> UserDetails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=localhost;Database=HealthInsurance;persist security info=True; Integrated Security=SSPI;Encrypt=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AcquirerPlan>(entity =>
        {
            entity.ToTable("AcquirerPlan");

            entity.Property(e => e.AcquirerPlanId).HasColumnName("AcquirerPlanID");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.InsurancePlanId).HasColumnName("InsurancePlanID");
            entity.Property(e => e.PaymentCycleId).HasColumnName("PaymentCycleID");
            entity.Property(e => e.UserDetailId).HasColumnName("UserDetailID");

            entity.HasOne(d => d.InsurancePlan).WithMany(p => p.AcquirerPlans)
                .HasForeignKey(d => d.InsurancePlanId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AcuirerPlan_InsurancePlan");

            entity.HasOne(d => d.PaymentCycle).WithMany(p => p.AcquirerPlans)
                .HasForeignKey(d => d.PaymentCycleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("F_AcquirerPlan_PaymentCycle");

            entity.HasOne(d => d.UserDetail).WithMany(p => p.AcquirerPlans)
                .HasForeignKey(d => d.UserDetailId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AcquirerPlan_UserDetail");
        });

        modelBuilder.Entity<InsurancePlan>(entity =>
        {
            entity.ToTable("InsurancePlan");

            entity.Property(e => e.InsurancePlanId).HasColumnName("InsurancePlanID");
            entity.Property(e => e.AdditionalPercentage).HasColumnType("decimal(18, 6)");
            entity.Property(e => e.CreatedOn).HasColumnType("datetime");
            entity.Property(e => e.InsuranceName)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedOn).HasColumnType("datetime");
            entity.Property(e => e.PremiumPercentage).HasColumnType("decimal(18, 6)");
            entity.Property(e => e.SumAssured).HasColumnType("decimal(18, 6)");
        });

        modelBuilder.Entity<PaymentCycle>(entity =>
        {
            entity.ToTable("PaymentCycle");

            entity.Property(e => e.PaymentCycleId).HasColumnName("PaymentCycleID");
            entity.Property(e => e.CycleDescription)
                .HasMaxLength(250)
                .IsUnicode(false);
        });

        modelBuilder.Entity<PaymentSchedule>(entity =>
        {
            entity.ToTable("PaymentSchedule");

            entity.Property(e => e.PaymentScheduleId).HasColumnName("PaymentScheduleID");
            entity.Property(e => e.AcquirerPlanId).HasColumnName("AcquirerPlanID");
            entity.Property(e => e.PaymentCycleId).HasColumnName("PaymentCycleID");
            entity.Property(e => e.PaymentDate).HasColumnType("datetime");
            entity.Property(e => e.PremiumAmount).HasColumnType("decimal(18, 6)");
            entity.Property(e => e.PremiumDate).HasColumnType("datetime");
            entity.Property(e => e.UserDetailId).HasColumnName("UserDetailID");

            entity.HasOne(d => d.AcquirerPlan).WithMany(p => p.PaymentSchedules)
                .HasForeignKey(d => d.AcquirerPlanId)
                .HasConstraintName("FK_PaymentSchedule_AcquirerPlan");

            entity.HasOne(d => d.PaymentCycle).WithMany(p => p.PaymentSchedules)
                .HasForeignKey(d => d.PaymentCycleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PaymentSchedule_PaymentCycle");

            entity.HasOne(d => d.UserDetail).WithMany(p => p.PaymentSchedules)
                .HasForeignKey(d => d.UserDetailId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PaymentSchedule_UserDetail");
        });

        modelBuilder.Entity<UserDetail>(entity =>
        {
            entity.ToTable("UserDetail");

            entity.Property(e => e.UserDetailId).HasColumnName("UserDetailID");
            entity.Property(e => e.Address)
                .HasMaxLength(1000)
                .IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.IsIssuer).HasDefaultValue(false);
            entity.Property(e => e.IsSmoker).HasDefaultValue(false);
            entity.Property(e => e.Password)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(250)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
