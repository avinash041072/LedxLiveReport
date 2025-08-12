using System;
namespace LedxLiveReport.Models
{
    public partial class VwUserActivityLiveToday
    {
        public int Id { get; set; }

        public int PostId { get; set; }

        public DateTime? VisitDate { get; set; }

        public string? UserIp { get; set; }

        public string? Permalink { get; set; }

        public string? CampaignId { get; set; }

        public string? Device { get; set; }

        public string? DeviceId { get; set; }

        public string? Status { get; set; }

        public string? PostType { get; set; }

        public int TimeSpent { get; set; }

        public string? ProductType { get; set; }

        public string? PostTitle { get; set; }

        public bool? IsTestUser { get; set; }

        public DateTime? RegistrationDate { get; set; }

        public string? Gender { get; set; }

        public string? State { get; set; }

        public DateTime? LastActiveDate { get; set; }

        public bool? IsPaid { get; set; }

        public string? UtmId { get; set; }

        public string? UtmMedium { get; set; }

        public string? UtmCampaign { get; set; }

        public string? UtmSource { get; set; }

        public string? DisplayName { get; set; }

        public string? City { get; set; }

        public string? MobileNo { get; set; }

        public int? UserId { get; set; }

        public string? Category { get; set; }

        public string? SubCategory { get; set; }

        public string? _5ccategory { get; set; }

        public string? UserEmail { get; set; }

        public string? PursuingYear { get; set; }

        public string? UtmOwner { get; set; }
    }
}

