namespace introducao_csharp.Telas
{
    public interface ITelaBase
    {
        void Executar();

        void Exportar();

        void Remover();

        void Adicionar();

        void Listar();

        void Detalhes(int index);
    }
}