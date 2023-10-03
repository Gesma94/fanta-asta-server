namespace FantaAstaServer.Models.APIs;

public class CreateBatchRequestDto
{
    public int AuctionId { get; set; }
    public int FootballerId { get; set; }
    public int InitialCost { get; set; }
    public int CurrentCost { get; set; }
}