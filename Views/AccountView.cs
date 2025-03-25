namespace ApiTest.Views
{
    public class AccountView
    {
        public required string Name { get; set; }
        public required string Cnpj { get; set; }
        public required Guid Identifier { get; set; }
    }
}