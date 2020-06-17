namespace introducao_csharp.Entidades
{
    public class Endereco
    {
        public string Rua { get; set; }

        public string Numero { get; set; }

        public string Cep { get; set; }

        public string Cidade { get; set; }
        public string Estado { get; set; }

        public Endereco()
        {
        }

        public Endereco(string cep)
        {
            this.Cep = cep;
        }

        public Endereco(string rua, string numero, string cep, string cidade, string estado)
        {
            this.Rua = rua;
            this.Numero = numero;
            this.Cep = cep;
            this.Cidade = cidade;
            this.Estado = estado;
        }

        public string RetornaDados()
        {
            return $"Endereço: {Rua}, {Numero} - {Cidade}/{Estado} - {Cep}";
        }

    }
}