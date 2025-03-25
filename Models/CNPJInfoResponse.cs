namespace ApiTest.Model
{
    public class CNPJInfoResponse
    {
        public string Abertura { get; set; }
        public string Situacao { get; set; }
        public string Tipo { get; set; }
        public string Nome { get; set; }
        public string Fantasia { get; set; }
        public string Porte { get; set; }
        public string NaturezaJuridica { get; set; }
        public List<Activity> AtividadePrincipal { get; set; }
        public List<Activity> AtividadesSecundarias { get; set; }
        public List<Person> Qsa { get; set; }
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Municipio { get; set; }
        public string Bairro { get; set; }
        public string Uf { get; set; }
        public string Cep { get; set; }
        public string Telefone { get; set; }
        public string DataSituacao { get; set; }
        public string Cnpj { get; set; }
        public string UltimaAtualizacao { get; set; }
        public string Status { get; set; }
        public string Complemento { get; set; }
        public string Email { get; set; }
        public string Efr { get; set; }
        public string MotivoSituacao { get; set; }
        public string SituacaoEspecial { get; set; }
        public string DataSituacaoEspecial { get; set; }
        public string CapitalSocial { get; set; }
        public Simples Simples { get; set; }
        public Simei Simei { get; set; }
        public Billing Billing { get; set; }
    }

    public class Activity
    {
        public string Code { get; set; }
        public string Text { get; set; }
    }

    public class Person
    {
        public string Nome { get; set; }
        public string Qual { get; set; }
    }

    public class Simples
    {
        public bool Optante { get; set; }
        public string DataOpcao { get; set; }
        public string DataExclusao { get; set; }
        public string UltimaAtualizacao { get; set; }
    }

    public class Simei
    {
        public bool Optante { get; set; }
        public string DataOpcao { get; set; }
        public string DataExclusao { get; set; }
        public string UltimaAtualizacao { get; set; }
    }

    public class Billing
    {
        public bool Free { get; set; }
        public bool Database { get; set; }
    }
}