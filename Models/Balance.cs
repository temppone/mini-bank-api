using System.ComponentModel.DataAnnotations;

namespace ApiTest.Model
{
    public class Balance
    {
        [Key]
        public Guid Identifier { get; set; }
        public Guid AccountIdentifier { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public void Withdraw(decimal withdrawAmount)
        {
            Amount -= withdrawAmount;
        }
        public void Deposit(decimal depositAmount)
        {
            Amount += depositAmount;
        }
    }
}