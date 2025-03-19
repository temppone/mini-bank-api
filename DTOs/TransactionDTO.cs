namespace ApiTest.DTOs
{
    public class TransactionDTO
    {
        public decimal Amount { get; set; }
        public Guid DestinationAccountIdentifier { get; set; }
        public Guid SourceAccountIdentifier { get; set; }
    }
}