using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace LedxLiveReport.Models;

public partial class ReportDataContext : DbContext
{
    public ReportDataContext()
    {
    }

    public ReportDataContext(DbContextOptions<ReportDataContext> options)
        : base(options)
    {
    }
    public virtual DbSet<VwUserActivityLiveToday> VwUserActivityLiveTodays { get; set; }
    public virtual DbSet<VwRegisteredUsersLive> VwRegisteredUsersLives { get; set; }
    public virtual DbSet<VwUserCart> VwUserCarts { get; set; }
    public virtual DbSet<RegisteredUser> RegisteredUsers { get; set; }
         public virtual DbSet<VwEventDetailsWithMooveAndPaidRegisteredUser> VwEventDetailsWithMooveAndPaidRegisteredUsers { get; set; }
    public virtual DbSet<VwEventUser> VwEventUsers { get; set; }
    public virtual DbSet<VwPaidUsersLive> VwPaidUsersLives { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("server=194.233.83.33,1433;database=LDX_ReportData;uid=ledxuser;pwd=LeDs#45XUseR6^DtB;TrustServerCertificate=true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<VwUserActivityLiveToday>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VW_UserActivityLiveToday");

            entity.Property(e => e.CampaignId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Category)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.City)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Device)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.DeviceId)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.DisplayName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.LastActiveDate).HasColumnType("datetime");
            entity.Property(e => e.MobileNo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Permalink)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.PostTitle)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.PostType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ProductType)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.PursuingYear)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.RegistrationDate).HasColumnType("date");
            entity.Property(e => e.State)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.SubCategory)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UserEmail)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UserIp)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UtmCampaign)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UtmId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UtmMedium)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UtmOwner)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UtmSource)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.VisitDate).HasColumnType("datetime");
            entity.Property(e => e._5ccategory)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("5CCategory");
        });
        modelBuilder.Entity<VwEventUser>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VW_EventUser");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DisplayName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.EventStatus)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.EventTitle)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.EventType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.EventUrl)
                .HasMaxLength(250)
                .IsUnicode(false)
                .HasColumnName("EventURL");
            entity.Property(e => e.MobileNo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.OldEventTrakerId).HasColumnName("Old_EventTrakerId");
            entity.Property(e => e.ProductType)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.RegistrationDate).HasColumnType("date");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UserEmail)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Utmid)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("UTMId");
        });
        modelBuilder.Entity<VwEventDetailsWithMooveAndPaidRegisteredUser>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VW_EventDetailsWithMooveAndPaidRegisteredUsers");

            entity.Property(e => e.BuyerEmail)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.City)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.DisplayName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.EventCampaign)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.EventOwner)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.EventStatus)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.EventTitle)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.EventUtmDepartment)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.EventUtmid)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.EventutmCategory)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.EventutmMedium)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.EventutmSource)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.MobileNo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.OldUserId).HasColumnName("Old_UserId");
            entity.Property(e => e.OrderItemName)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.OrderStatus)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PaymentDate).HasColumnType("datetime");
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ProductType)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.PursuingYear)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.RptCreatedDate).HasColumnType("date");
            entity.Property(e => e.RptPaidUserId).HasColumnName("RptPaidUserID");
            entity.Property(e => e.RptProductType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.State)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UserEmail)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UserType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UtmCampaign)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UtmCategory)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("UTmCategory");
            entity.Property(e => e.UtmId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UtmMedium)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UtmSource)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Utmdepartment)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("UTMDepartment");
        });
        modelBuilder.Entity<RegisteredUser>(entity =>
        {
            entity.Property(e => e.Address)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Airrange)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("AIRRange");
            entity.Property(e => e.AreadOfInterest)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.BackUpMobileNo)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.BitrixContactStatus)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.BitrixFields)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CertificateSent).HasDefaultValueSql("((0))");
            entity.Property(e => e.City)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.College)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Country)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DisplayName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Dob)
                .HasColumnType("date")
                .HasColumnName("DOB");
            entity.Property(e => e.Domicile)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ExternalSource)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.InternalSource)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.IsPaid).HasDefaultValueSql("((0))");
            entity.Property(e => e.IsTestUser).HasDefaultValueSql("((0))");
            entity.Property(e => e.LastActiveDate).HasColumnType("datetime");
            entity.Property(e => e.LeadSquareId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LedxUtmUpdate).HasColumnName("ledxUtmUpdate");
            entity.Property(e => e.MailModoCampaignId)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.MailModoCampaignName)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.MailModoStatus)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.MobileNo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.MobileNoForMatching)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.OldLastActiveDate).HasColumnType("datetime");
            entity.Property(e => e.OldUserId).HasColumnName("Old_UserId");
            entity.Property(e => e.Organization)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.PostCode)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Program)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.PursuingYear)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.RegistrationDate).HasColumnType("date");
            entity.Property(e => e.Remark)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ScoreRange)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Source)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.SourceInfo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.State)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.StatusCheck)
                .HasMaxLength(150)
                .IsUnicode(false)
                .HasColumnName("Status_Check");
            entity.Property(e => e.Stream)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.TempStatus)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Temp_status");
            entity.Property(e => e.Unsubscribed)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UserEmail)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UserLogin)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UserName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UserSource)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UserType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UtmCampaign)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UtmCategory)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("UTmCategory");
            entity.Property(e => e.UtmId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UtmMedium)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UtmOwner)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UtmSource)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Utmdepartment)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("UTMDepartment");
            entity.Property(e => e.YearOfAttempt)
                .HasMaxLength(50)
                .IsUnicode(false);
        });
        modelBuilder.Entity<VwRegisteredUsersLive>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VW_RegisteredUsersLive");

            entity.Property(e => e.Address)
                .HasMaxLength(500)
                .IsUnicode(false);
            entity.Property(e => e.Airrange)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("AIRRange");
            entity.Property(e => e.City)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.College)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.Country)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DisplayName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Dob)
                .HasColumnType("date")
                .HasColumnName("DOB");
            entity.Property(e => e.Domicile)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Id).ValueGeneratedOnAdd();
            entity.Property(e => e.LastActiveDate).HasColumnType("datetime");
            entity.Property(e => e.MobileNo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Organization)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.PostCode)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.PursuingYear)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.RegistrationDate).HasColumnType("date");
            entity.Property(e => e.ScoreRange)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.State)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Unsubscribed)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UserEmail)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UserSource)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UserType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UtmCampaign)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UtmCategory)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("UTmCategory");
            entity.Property(e => e.UtmId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UtmMedium)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UtmOwner)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UtmSource)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Utmdepartment)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("UTMDepartment");
            entity.Property(e => e.YearOfAttempt)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VwUserCart>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VW_UserCart");

            entity.Property(e => e.CartKey)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.City)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DisplayName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.ItemType)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.LastActiveDate).HasColumnType("datetime");
            entity.Property(e => e.MobileNo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.ProductName)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.PursuingYear)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.RegistrationDate).HasColumnType("date");
            entity.Property(e => e.State)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Unsubscribed)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UserEmail)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UserType)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<VwPaidUsersLive>(entity =>
        {
            entity
                .HasNoKey()
                .ToView("VW_PaidUsersLive");

            entity.Property(e => e.Airrange)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("AIRRange");
            entity.Property(e => e.BuyerEmail)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.BuyerMobileNo)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.City)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CouponCode)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DisplayName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Domicile)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Gender)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.MobileNo)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.OrderDate).HasColumnType("date");
            entity.Property(e => e.OrderItemName)
                .HasMaxLength(250)
                .IsUnicode(false);
            entity.Property(e => e.OrderStatus)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.OrderType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PaymentDate).HasColumnType("datetime");
            entity.Property(e => e.PaymentMethod)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PriceRange)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ProductType)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.PursuingYear)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ScoreRange)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.State)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Unsubscribed)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UserType)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UtmCampaign)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.UtmCategory)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("UTmCategory");
            entity.Property(e => e.UtmId)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.UtmOwner)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Utmdepartment)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("UTMDepartment");
            entity.Property(e => e.YearOfAttempt)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
