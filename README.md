# HealthInsuranceService

--CREATE DATABASE HealthInsurance

--USE HealthInsurance


----------USER-----------------------------------

CREATE TABLE UserDetail (UserDetailID BIGINT IDENTITY(1,1) Constraint PK_UserDetail Primary Key, IsIssuer BIT DEFAULT 0, UserName VARCHAR(250) NOT NULL, Password VARCHAR(250) NOT NULL, Email VARCHAR(250), [Address] VARCHAR(1000), PhoneNumber BIGINT NOT NULL, IsSmoker BIT DEFAULT 0)

----------ISSUER---------------------------------

CREATE TABLE InsurancePlan (InsurancePlanID BIGINT IDENTITY(1,1) Constraint PK_InsurancePlan Primary Key, InsuranceName Varchar(1000) NOT NULL, SumAssured DECIMAL(18,6) NOT NULL, PremiumPercentage DECIMAL(18,6), AdditionalPercentage DECIMAL(18,6) NOT NULL, CreatedOn DateTime NOT NULL, CreatedBy BIGINT NOT NULL, ModifiedOn DateTime NOT NULL, ModifiedBy BIGINT NOT NULL)

----------MASTER DATA-----------------------------

CREATE TABLE PaymentCycle (PaymentCycleID INT IDENTITY(1,1) Constraint PK_PaymentCycle Primary Key, CycleDescription VARCHAR(250) NOT NULL)

----------ACQUIRER--------------------------------

CREATE TABLE AcquirerPlan (AcquirerPlanID BIGINT IDENTITY(1,1) Constraint PK_AcquirerPlan Primary Key, UserDetailID BIGINT NOT NULL Constraint FK_AcquirerPlan_UserDetail Foreign Key References UserDetail(UserDetailID), InsurancePlanID BIGINT NOT NULL Constraint FK_AcuirerPlan_InsurancePlan Foreign Key References InsurancePlan(InsurancePlanID), PaymentCycleID INT NOT NULL Constraint F_AcquirerPlan_PaymentCycle Foreign Key References PaymentCycle(PaymentCycleID), CreatedOn DATETIME NOT NULL) 

CREATE TABLE PaymentSchedule (PaymentScheduleID BIGINT IDENTITY(1,1) Constraint PK_PaymentSchedule Primary Key, UserDetailID BIGINT NOT NULL Constraint FK_PaymentSchedule_UserDetail Foreign Key References UserDetail(UserDetailID), AcquirerPlanID BIGINT Constraint FK_PaymentSchedule_AcquirerPlan Foreign Key References AcquirerPlan(AcquirerPlanID), PaymentCycleID INT NOT NULL Constraint FK_PaymentSchedule_PaymentCycle Foreign Key References PaymentCycle(PaymentCycleID), PremiumDate DATETIME, PremiumAmount DECIMAL(18,6), IsPaid BIT, PaymentDate DATETIME)