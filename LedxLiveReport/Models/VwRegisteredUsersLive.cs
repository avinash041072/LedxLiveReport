using System;
using System.Collections.Generic;

namespace LedxLiveReport.Models;

public partial class VwRegisteredUsersLive
{
    public DateTime? RegistrationDate { get; set; }

    public int? UserId { get; set; }

    public string? UserEmail { get; set; }

    public string? MobileNo { get; set; }

    public string? Gender { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public DateTime? LastActiveDate { get; set; }

    public string? UtmId { get; set; }

    public string? UtmMedium { get; set; }

    public string? UtmCampaign { get; set; }

    public string? UtmSource { get; set; }

    public string? UtmCategory { get; set; }

    public string? Utmdepartment { get; set; }

    public bool? IsTestUser { get; set; }

    public bool? IsPaid { get; set; }

    public bool? CertificateSent { get; set; }

    public string? UserType { get; set; }

    public DateTime? Dob { get; set; }

    public string? Organization { get; set; }

    public string? College { get; set; }

    public string? YearOfAttempt { get; set; }

    public double? AllIndiaRank { get; set; }

    public string? ScoreRange { get; set; }

    public string? Airrange { get; set; }

    public string? Domicile { get; set; }

    public string? UtmOwner { get; set; }

    public string? DisplayName { get; set; }

    public string? PursuingYear { get; set; }

    public string? Address { get; set; }

    public string? PostCode { get; set; }

    public string? Country { get; set; }

    public int Id { get; set; }

    public string? UserSource { get; set; }

    public string? Unsubscribed { get; set; }

    public DateTime? CreatedDate { get; set; }
}
