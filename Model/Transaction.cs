using System.ComponentModel.DataAnnotations;

namespace ApiTest.Model
{
    public class Transaction
    {
        [Key]
        public Guid Identifier { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; }
        public Guid DestinationAccountIdentifier { get; set; }
        public Guid SourceAccountIdentifier { get; set; }
    }

}