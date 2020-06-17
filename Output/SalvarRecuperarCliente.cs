using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using introducao_csharp.Entidades;

namespace introducao_csharp.Output
{

    public class SalvarRecuperarCliente
    {

        private const string caminho = "bd-clientes.xml";

        public void Salvar(List<Cliente> clientes)
        {
            var doc = new XmlDocument();

            var xmlDado = doc.CreateElement("dados");
            doc.AppendChild(xmlDado);

            foreach (var cliente in clientes)
            {
                var xmlCliente = doc.CreateElement("cliente");

                var xmlNome = doc.CreateElement("nome");
                xmlNome.InnerText = cliente.Nome;
                xmlCliente.AppendChild(xmlNome);

                var xmlIdade = doc.CreateElement("idade");
                xmlIdade.InnerText = cliente.Idade.ToString();
                xmlCliente.AppendChild(xmlIdade);

                var xmlSexo = doc.CreateElement("sexo");
                xmlSexo.InnerText = cliente.Sexo.ToString();
                xmlCliente.AppendChild(xmlSexo);

                var xmlNcartRes = doc.CreateElement("numeroCarteiraReservista");
                xmlNcartRes.InnerText =
                    string.IsNullOrWhiteSpace(cliente.NumeroCarteiraReservista)
                    ? " "
                    : cliente.NumeroCarteiraReservista;
                xmlCliente.AppendChild(xmlNcartRes);

                var xmlNcartMot = doc.CreateElement("numeroCarteiraMotorista");
                xmlNcartMot.InnerText =
                    string.IsNullOrWhiteSpace(cliente.NumeroCarteiraMotorista)
                    ? " "
                    : cliente.NumeroCarteiraMotorista;
                xmlCliente.AppendChild(xmlNcartMot);

                var xmlCpf = doc.CreateElement("cpf");
                xmlCpf.InnerText = cliente.Cpf;
                xmlCliente.AppendChild(xmlCpf);

                var xmlEndereco = doc.CreateElement("endereco");

                var xmlRua = doc.CreateElement("rua");
                xmlRua.InnerText = cliente.Endereco == null ? " " : cliente.Endereco.Rua;
                xmlEndereco.AppendChild(xmlRua);

                var xmlNumero = doc.CreateElement("numero");
                xmlNumero.InnerText = cliente.Endereco == null ? " " : cliente.Endereco.Numero;
                xmlEndereco.AppendChild(xmlNumero);

                var xmlCep = doc.CreateElement("cep");
                xmlCep.InnerText = cliente.Endereco == null ? " " : cliente.Endereco.Cep;
                xmlEndereco.AppendChild(xmlCep);

                var xmlCidade = doc.CreateElement("cidade");
                xmlCidade.InnerText = cliente.Endereco == null ? " " : cliente.Endereco.Cidade;
                xmlEndereco.AppendChild(xmlCidade);

                var xmlEstado = doc.CreateElement("estado");
                xmlEstado.InnerText = cliente.Endereco == null ? " " : cliente.Endereco.Estado;
                xmlEndereco.AppendChild(xmlEstado);

                xmlCliente.AppendChild(xmlEndereco);

                xmlDado.AppendChild(xmlCliente);
            }

            doc.Save(caminho);
        }

        public List<Cliente> PegarTodos()
        {
            var clientes = new List<Cliente>();

            try
            {
                var doc = XDocument.Load(caminho);

                var xmlClientes = from c in doc.Descendants("cliente")
                                  select new
                                  {
                                      Nome = c.Element("nome").Value,
                                      Idade = c.Element("idade").Value,
                                      Sexo = c.Element("sexo").Value,
                                      NumeroCarteiraReservista = c.Element("numeroCarteiraReservista").Value,
                                      NumeroCarteiraMotorista = c.Element("numeroCarteiraMotorista").Value,
                                      Cpf = c.Element("cpf").Value,
                                      Rua = c.Element("endereco").Element("rua").Value,
                                      Numero = c.Element("endereco").Element("numero").Value,
                                      Cep = c.Element("endereco").Element("cep").Value,
                                      Cidade = c.Element("endereco").Element("cidade").Value,
                                      Estado = c.Element("endereco").Element("estado").Value
                                  };

                foreach (var c in xmlClientes)
                {
                    var endereco = new Endereco(
                        c.Rua,
                        c.Numero,
                        c.Cep,
                        c.Cidade,
                        c.Estado
                    );

                    var cliente = new Cliente(
                        c.Nome,
                        c.Cpf,
                        int.Parse(c.Idade),
                        c.Sexo == "Masculino" ? EnumSexo.Masculino : EnumSexo.Feminino,
                        c.NumeroCarteiraReservista,
                        c.NumeroCarteiraMotorista,
                        endereco
                    );

                    clientes.Add(cliente);
                }

                return clientes;
            }
            catch
            {
                var doc = new XmlDocument();
                doc.AppendChild(doc.CreateElement("dados"));
                doc.Save(caminho);
            }

            return clientes;
        }

    }
}