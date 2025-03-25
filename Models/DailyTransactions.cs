using System.ComponentModel.DataAnnotations;

namespace ApiTest.Model
{
    public class DailyTransactions
    {
        [Key]
        public Guid Identifier { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public DateTime Date { get; set; }
        public required List<Transaction> Transactions { get; set; }
    }
}