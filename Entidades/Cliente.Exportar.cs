using System.Xml;

namespace introducao_csharp.Entidades
{
    public partial class Cliente
    {

        public string ExportarCsv()
        {
            return $"{Cpf};{Nome}";
        }

        public XmlElement ExportarXml(XmlDocument doc)
        {
            var xmlCliente = doc.CreateElement("cliente");

            var xmlCpf = doc.CreateElement("cpf");
            xmlCpf.InnerText = Cpf;
            xmlCliente.AppendChild(xmlCpf);

            var xmlNome = doc.CreateElement("nome");
            xmlNome.InnerText = Nome;
            xmlCliente.AppendChild(xmlNome);

            return xmlCliente;
        }
    }
}