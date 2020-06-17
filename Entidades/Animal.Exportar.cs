using System.Xml;

namespace introducao_csharp.Entidades
{
    public partial class Animal
    {
        public string ExportarCsv()
        {
            return $"{Nome};{Idade}";
        }

        public XmlElement ExportarXml(XmlDocument doc)
        {
            var xmlCliente = doc.CreateElement("cliente");

            var xmlNome = doc.CreateElement("nome");
            xmlNome.InnerText = Nome;
            xmlCliente.AppendChild(xmlNome);

            var xmlIdade = doc.CreateElement("idade");
            xmlIdade.InnerText = Idade.ToString();
            xmlCliente.AppendChild(xmlIdade);

            return xmlCliente;
        }
    }
}