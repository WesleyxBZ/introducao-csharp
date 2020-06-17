using System.Collections.Generic;
using System.IO;
using introducao_csharp.Entidades;

namespace introducao_csharp.Output
{
    public class ExportarCsv : IExportar
    {

        public string NomeArquivo { get; set; }

        public ExportarCsv(string nomeArquivo)
        {
            NomeArquivo = nomeArquivo + ".csv";
        }

        public void Exportar(List<IExportarDados> dados)
        {
            using (var file = new StreamWriter(NomeArquivo))
            {
                foreach (var dado in dados)
                {
                    file.WriteLine(dado.ExportarCsv());
                }
            }
        }

    }
}