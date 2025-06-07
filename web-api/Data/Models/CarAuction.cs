namespace WebApi.Data.Models;

public class CarAuction
{
    public Guid? Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public decimal StartingPrice { get; set; } = 0;
    public decimal CurrentPrice { get; set; } = 0;
    public bool IsActive => DateTime.UtcNow >= StartDate && DateTime.UtcNow <= EndDate;


    public CarAuctionModel ToModel()
    {
        return new CarAuctionModel
        {
            Id = Id,
            StartDate = StartDate,
            EndDate = EndDate,
            StartingPrice = StartingPrice,
            CurrentPrice = CurrentPrice
        };
    }
}