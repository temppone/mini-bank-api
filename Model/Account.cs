using System.ComponentModel.DataAnnotations;

namespace ApiTest.Model
{
    public class Account
    {
        [Key]
        public Guid Identifier { get; set; }
        public required string Cnpj { get; set; }
        public required string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}