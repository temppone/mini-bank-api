namespace ApiTest.Views
{
    public class TransactionView
    {
        public Guid Identifier { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public Guid DestinationAccountIdentifier { get; set; }
        public Guid SourceAccountIdentifier { get; set; }
    }
}