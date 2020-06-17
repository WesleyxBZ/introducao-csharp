using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;
using introducao_csharp.Entidades;

namespace introducao_csharp.Output
{
    public class SalvarRecuperarAnimal
    {
        private const string caminho = "bd-animais.xml";

        public void Salvar(List<Animal> animais)
        {
            var doc = new XmlDocument();

            var xmlDado = doc.CreateElement("dados");
            doc.AppendChild(xmlDado);

            foreach (var animal in animais)
            {
                var xmlCliente = doc.CreateElement("animal");

                var xmlNome = doc.CreateElement("nome");
                xmlNome.InnerText = animal.Nome;
                xmlCliente.AppendChild(xmlNome);

                var xmlIdade = doc.CreateElement("idade");
                xmlIdade.InnerText = animal.Idade.ToString();
                xmlCliente.AppendChild(xmlIdade);

                var xmlSexo = doc.CreateElement("sexo");
                xmlSexo.InnerText = animal.Sexo.ToString();
                xmlCliente.AppendChild(xmlSexo);

                xmlDado.AppendChild(xmlCliente);
            }

            doc.Save(caminho);
        }

        public List<Animal> PegarTodos()
        {
            var animais = new List<Animal>();

            try
            {
                var doc = XDocument.Load(caminho);

                var xmlAnimais = from c in doc.Descendants("animal")
                                 select new
                                 {
                                     Nome = c.Element("nome").Value,
                                     Idade = c.Element("idade").Value,
                                     Sexo = c.Element("sexo").Value
                                 };

                foreach (var a in xmlAnimais)
                {
                    animais.Add(
                        new Animal(a.Nome, int.Parse(a.Idade), a.Sexo == "Masculino" ? EnumSexo.Masculino : EnumSexo.Feminino)
                    );
                }

                return animais;
            }
            catch
            {
                var doc = new XmlDocument();
                doc.AppendChild(doc.CreateElement("dados"));
                doc.Save(caminho);
            }

            return animais;
        }


    }
}