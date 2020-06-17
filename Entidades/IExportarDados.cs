using System.Xml;

namespace introducao_csharp.Entidades
{
    public interface IExportarDados
    {
        string ExportarCsv();

        XmlElement ExportarXml(XmlDocument doc);
    }
}