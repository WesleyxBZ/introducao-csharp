using System.Collections.Generic;
using System.Linq;
using introducao_csharp.Entidades;
using introducao_csharp.Output;

namespace introducao_csharp.Telas
{
    public class TelaAnimal : TelaBase<Animal>, ITelaBase
    {

        public TelaAnimal()
        {
            Lista = new List<Animal>();
            CarregarDados();
        }

        public void Executar()
        {
            var executando = true;

            do
            {
                LimparTela();
                Escrever("\n== MENU ==\n");

                Escrever("1 - Listar");
                Escrever("2 - Adicionar");
                Escrever("3 - Remover");
                Escrever("4 - Exportar");
                Escrever("5 - Sair");
                Escrever("\nSelecione uma opção:");

                var opt = LerInt();

                switch (opt)
                {
                    case 1:
                        Listar();
                        break;
                    case 2:
                        Adicionar();
                        break;
                    case 3:
                        Remover();
                        break;
                    case 4:
                        Exportar();
                        AguardarTela();
                        break;
                    case 5:
                        executando = false;
                        break;
                    default:
                        AguardarTela("Opção inválida");
                        break;
                }


            } while (executando);

        }

        public void Adicionar()
        {
            var executando = true;

            do
            {
                LimparTela();
                Escrever("== Adicionar ==\n");
                var nome = EscreverLerString("Informe seu nome:");
                var idade = EscreverLerInt("Informe sua idade:");
                var sexoStr = EscreverLerString("Informe seu sexo (m ou f):");
                var sexo = sexoStr == "m" ? EnumSexo.Masculino : EnumSexo.Feminino;

                var animal = new Animal(nome, idade, sexo);

                Lista.Add(animal);
                new SalvarRecuperarAnimal().Salvar(Lista);

                var opt = EscreverLerString("\nDeseja preencher outro animal? (s/n)");
                executando = opt == "n" ? false : true;

            } while (executando);
        }

        public void Listar()
        {
            LimparTela();
            Escrever("== Listar ==");

            if (Lista.Capacity == 0)
            {
                Escrever("\n\nNão há animais cadastrados");
                AguardarTela("\n\nPressione qualquer tecla para voltar");
            }
            else
            {
                var count = 0;
                Escrever("\nID | Nome \n---------");
                foreach (var item in Lista)
                {
                    Escrever($" {count + 1} - {item.Nome}");
                    count++;
                }

                var numSelec = EscreverLerString("\n\nInsira o id do animal para ver mais detalhes ou deixe em branco para voltar:");

                if (!string.IsNullOrWhiteSpace(numSelec))
                {
                    var pos = int.Parse(numSelec);
                    if (pos > 0 && pos <= Lista.Capacity) Detalhes(pos - 1);
                }
            }

        }

        public void Detalhes(int index)
        {
            LimparTela();
            var a = Lista[0];
            Escrever($"== Detalhes do animal ==");
            Escrever("\n" + a.RetornaDados());
            AguardarTela("\n\nPressione qualquer tecla para voltar");
        }

        public void Exportar()
        {
            LimparTela();
            Escrever("== Exportar dados ==");

            if (Lista.Capacity == 0)
            {
                Escrever("\n\nNão há animais cadastrados");
                AguardarTela("\n\nPressione qualquer tecla para voltar");
            }
            else
            {
                var opt = EscreverLerInt("\n1 - CVS \n2 - Xml \n3 - Email \n\nOpção:");
                var nomeArquivo = EscreverLerString("\nQual o nome do arquivo");
                var exportador = FactoryExportar.RetornarExportador((EnumTipoExportacao)opt, nomeArquivo);
                exportador.Exportar(Lista.Cast<IExportarDados>().ToList());
            }
        }

        public void Remover()
        {

            if (Lista.Capacity == 0)
            {
                LimparTela();
                Escrever("== Remover ==");
                Escrever("\n\nNão há animais cadastrados");
                AguardarTela("\n\nPressione qualquer tecla para voltar");
            }
            else
            {
                var executando = true;

                do
                {
                    LimparTela();
                    Escrever("== Remover ==");

                    var count = 0;
                    Escrever("\nID | Nome \n---------");
                    foreach (var item in Lista)
                    {
                        Escrever($" {count + 1} - {item.Nome}");
                        count++;
                    }

                    var idSelecionado = EscreverLerString("\nInforme o ID do animal a ser removido ou deixe em branco para voltar");
                    if (string.IsNullOrWhiteSpace(idSelecionado)) break;

                    var objEnc = Lista[int.Parse(idSelecionado) - 1];

                    if (objEnc == null)
                        AguardarTela("Animal não encontrado");
                    else
                    {
                        Lista.Remove(objEnc);
                        new SalvarRecuperarAnimal().Salvar(Lista);
                        Escrever("\nCliente removido com sucesso\n");
                    };

                    var opt = EscreverLerString("Deseja remover outro animal? s/n");
                    executando = opt == "n" ? false : true;

                } while (executando);
            }
        }

        private void CarregarDados()
        {
            Lista = new SalvarRecuperarAnimal().PegarTodos();
        }

    }
}