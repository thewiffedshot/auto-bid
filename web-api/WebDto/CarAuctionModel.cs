using System.ComponentModel.DataAnnotations;

public class CarAuctionModel
{
    public Guid? Id { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    [Required]
    public decimal StartingPrice { get; set; } = 0;
    public decimal CurrentPrice { get; set; } = 0;
    public bool IsActive => DateTime.UtcNow >= StartDate && DateTime.UtcNow <= EndDate;
}