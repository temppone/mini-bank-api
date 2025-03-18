namespace ApiTest.DTOs
{
    public class BalanceDTO
    {
        public decimal Amount { get; set; }
        public Guid AccountIdentifier { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}