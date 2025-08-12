using System;
using System.Collections.Generic;

namespace LedxLiveReport.Models;

public partial class VwUserCart
{
    public int Id { get; set; }

    public int? CartId { get; set; }

    public string? CartKey { get; set; }

    public string? ItemType { get; set; }

    public int? ProductId { get; set; }

    public string? ProductName { get; set; }

    public int? ProductQty { get; set; }

    public double? ProductPrice { get; set; }

    public string? Status { get; set; }

    public DateTime? CreatedDate { get; set; }

    public DateTime? RegistrationDate { get; set; }

    public int? UserId { get; set; }

    public string? DisplayName { get; set; }

    public string? UserEmail { get; set; }

    public string? MobileNo { get; set; }

    public string? Gender { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public DateTime? LastActiveDate { get; set; }

    public bool? IsTestUser { get; set; }

    public bool? IsPaid { get; set; }

    public string? UserType { get; set; }

    public string? PursuingYear { get; set; }

    public string? Unsubscribed { get; set; }
}
