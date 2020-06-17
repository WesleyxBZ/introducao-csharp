using System;

namespace introducao_csharp.Entidades
{
    public partial class Cliente : IExportarDados
    {
        public string Nome { get; private set; }

        public int Idade { get; private set; }

        public EnumSexo Sexo { get; private set; }

        public string NumeroCarteiraReservista { get; private set; }

        public string NumeroCarteiraMotorista { get; private set; }

        public Endereco Endereco { get; private set; }

        public string Cpf { get; private set; }

        public Cliente() { }

        public Cliente(string cpf, string nome, int idade, EnumSexo sexo)
        {
            Cpf = cpf;
            Nome = nome;
            Idade = idade;
            Sexo = sexo;
        }

        public Cliente(string cpf, string nome, int idade, EnumSexo sexo, Endereco endereco) : this(cpf, nome, idade, sexo)
        {
            Endereco = endereco;
        }

        public Cliente(string nome,
                        string cpf,
                        int idade,
                        EnumSexo sexo,
                        string numeroCarteiraReservista,
                        string numeroCarteiraMotorista,
                        Endereco endereco) : this(cpf, nome, idade, sexo)
        {
            NumeroCarteiraMotorista = numeroCarteiraMotorista;
            NumeroCarteiraReservista = numeroCarteiraReservista;
            Endereco = endereco;
        }

        public void InformarEndereco(Endereco endereco)
        {
            Endereco = endereco;
        }

        public void InformarDados(string nome, int idade, EnumSexo sexo)
        {
            Nome = nome;
            Idade = idade;
            Sexo = sexo;
        }

        public bool InformarCartieraMotorista(string numero)
        {
            if (!EhMaiorDeIdade())
                return false;

            NumeroCarteiraMotorista = numero;
            return true;
        }

        public bool InformarCartieraReservista(string numero)
        {
            if (!EhMaiorDeIdade() || Sexo == EnumSexo.Feminino)
                return false;

            NumeroCarteiraReservista = numero;
            return true;
        }

        public string RetornarCpfNome()
        {
            return $"{Cpf} - {Nome}";
        }

        public int RetornaAnoNascimento()
        {
            return DateTime.Now.Year - Idade;
        }

        public bool EhMaiorDeIdade()
        {
            return Idade >= 18;
        }
        public string RetornaDados()
        {
            var dados = $"Nome: {Nome} \nIdade: {Idade} \nSexo: {Sexo} \nCpf: {Cpf}\n";

            if (EhMaiorDeIdade() && Sexo == EnumSexo.Masculino)
                dados += $"Nº carteira reservista: {NumeroCarteiraReservista}\n";

            if (EhMaiorDeIdade() && NumeroCarteiraMotorista != null)
                dados += $"Nº carteira motorista: {NumeroCarteiraMotorista}\n";

            if (Endereco != null)
                dados += Endereco.RetornaDados();

            return dados;
        }

    }
}