using System.Collections.Generic;
using introducao_csharp.Entidades;

namespace introducao_csharp.Output
{
    public interface IExportar
    {
        string NomeArquivo { get; set; }

        void Exportar(List<IExportarDados> dados);
    }
}