using System;
using System.Collections.Generic;

namespace LedxLiveReport.Models;

	public partial class VwPaidUsersLive
{
    public DateTime? OrderDate { get; set; }

    public string? CouponCode { get; set; }

    public string? OrderType { get; set; }

    public string? OrderStatus { get; set; }

    public int? UserId { get; set; }

    public string? DisplayName { get; set; }

    public string? BuyerEmail { get; set; }

    public string? City { get; set; }

    public string? State { get; set; }

    public double? ProductGrossRevenue { get; set; }

    public double? ProductNetRevenue { get; set; }

    public double? DiscountAmount { get; set; }

    public double? SaleAmount { get; set; }

    public DateTime? PaymentDate { get; set; }

    public string? UtmId { get; set; }

    public string? OrderItemName { get; set; }

    public int OrderId { get; set; }

    public string? BuyerMobileNo { get; set; }

    public string? MobileNo { get; set; }

    public bool? IsRepeatUser { get; set; }

    public bool? IsTestUser { get; set; }

    public string? PriceRange { get; set; }

    public string? Gender { get; set; }

    public string? ScoreRange { get; set; }

    public string? Airrange { get; set; }

    public string? YearOfAttempt { get; set; }

    public string? Domicile { get; set; }

    public string? UserType { get; set; }

    public string? UtmOwner { get; set; }

    public string? UtmCategory { get; set; }

    public string? UtmCampaign { get; set; }

    public string? Utmdepartment { get; set; }

    public double? AllIndiaRank { get; set; }

    public string? ProductType { get; set; }

    public double? OrderQty { get; set; }

    public double? NetTotalAmount { get; set; }

    public string? PaymentMethod { get; set; }

    public string? PursuingYear { get; set; }

    public int Id { get; set; }

    public DateTime? CreatedDate { get; set; }

    public string? Unsubscribed { get; set; }
}



