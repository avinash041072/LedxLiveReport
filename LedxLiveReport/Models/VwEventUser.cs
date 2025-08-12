using System;
namespace LedxLiveReport.Models
{
    public partial class VwEventUser
    {
        public int Id { get; set; }

        public int? EventTrakerId { get; set; }

        public string? ProductType { get; set; }

        public int? EventId { get; set; }

        public string? Utmid { get; set; }

        public string? EventTitle { get; set; }

        public string? EventUrl { get; set; }

        public string? EventType { get; set; }

        public string? UserType { get; set; }

        public int? UserId { get; set; }

        public string Status { get; set; } = null!;

        public int? BitrixId { get; set; }

        public DateTime CreatedDate { get; set; }

        public int? OldEventTrakerId { get; set; }

        public string? EventStatus { get; set; }

        public string? DisplayName { get; set; }

        public string? UserName { get; set; }

        public DateTime? RegistrationDate { get; set; }

        public string? MobileNo { get; set; }

        public string? UserEmail { get; set; }

        public bool? IsTestUser { get; set; }
    }
}

