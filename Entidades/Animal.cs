namespace introducao_csharp.Entidades
{
    public partial class Animal : IExportarDados
    {
        public string Nome { get; private set; }

        public int Idade { get; private set; }

        public EnumSexo Sexo { get; private set; }

        public Animal(string nome, int idade, EnumSexo sexo)
        {
            Nome = nome;
            Idade = idade;
            Sexo = sexo;
        }

        public string RetornaDados()
        {
            return $"Nome: {Nome} \nIdade: {Idade} \nSexo: {Sexo}";
        }

    }

}