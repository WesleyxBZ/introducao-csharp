using System.Collections.Generic;
using System.Xml;
using introducao_csharp.Entidades;

namespace introducao_csharp.Output
{
    public class ExportarXml : IExportar
    {
        public string NomeArquivo { get; set; }

        public ExportarXml(string nomeArquivo)
        {
            NomeArquivo = nomeArquivo + ".xml";
        }

        public void Exportar(List<IExportarDados> dados)
        {
            var doc = new XmlDocument();

            var xmlDado = doc.CreateElement("dados");
            doc.AppendChild(xmlDado);

            foreach (var dado in dados)
            {
                xmlDado.AppendChild(dado.ExportarXml(doc));
            }

            doc.Save(NomeArquivo);
        }

    }
}