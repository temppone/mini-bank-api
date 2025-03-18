namespace ApiTest.DTOs
{
    public class AccountDTO
    {
        public required string Name { get; set; }
        public required string Cnpj { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}