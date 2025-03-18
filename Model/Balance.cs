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
    }
}