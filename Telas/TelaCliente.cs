using System.Linq;
using System.Collections.Generic;
using introducao_csharp.Entidades;
using introducao_csharp.Output;

namespace introducao_csharp.Telas
{
    public class TelaCliente : TelaBase<Cliente>, ITelaBase
    {

        public TelaCliente()
        {
            Lista = new List<Cliente>();
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

        public void Exportar()
        {
            LimparTela();
            Escrever("== Exportar dados ==");

            if (Lista.Capacity == 0)
            {
                Escrever("\n\nNão há clientes cadastrados");
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
                Escrever("== Remover clientes ==");
                Escrever("\n\nNão há clientes cadastrados");
                AguardarTela("\n\nPressione qualquer tecla para voltar");
            }
            else
            {
                var executando = true;

                do
                {
                    LimparTela();
                    Escrever("== Remover clientes ==");

                    var count = 0;
                    Escrever("\nID | Nome \n---------");
                    foreach (var item in Lista)
                    {
                        Escrever($" {count + 1} - {item.Nome}");
                        count++;
                    }

                    var idSelecionado = EscreverLerString("\nInforme o ID do cliente a ser removido ou deixe em branco para voltar");
                    if (string.IsNullOrWhiteSpace(idSelecionado)) break;

                    var objEnc = Lista[int.Parse(idSelecionado) - 1];

                    if (objEnc == null)
                        AguardarTela("Cliente não enconttrado");
                    else
                    {
                        Lista.Remove(objEnc);
                        new SalvarRecuperarCliente().Salvar(Lista);
                        Escrever("\nCliente removido com sucesso\n");
                    };

                    var opt = EscreverLerString("Deseja remover outro cliente? s/n");
                    executando = opt == "n" ? false : true;

                } while (executando);
            }
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
                var cpf = "";

                do
                {
                    cpf = EscreverLerString("Informe seu cpf:");
                } while (CheckCpfUnico(cpf));


                var sexoStr = EscreverLerString("Informe seu sexo (m ou f):");
                var sexo = sexoStr == "m" ? EnumSexo.Masculino : EnumSexo.Feminino;

                var optEndereco = EscreverLerString("\nDeseja preencher o endereço (s/n):");

                var cliente = new Cliente();

                if (optEndereco == "s")
                {
                    Escrever("\nInforme o Endereço");

                    var rua = EscreverLerString("Rua:");
                    var numero = EscreverLerString("Número:");
                    var cep = EscreverLerString("CEP:");
                    var estado = EscreverLerString("Estado:");
                    var cidade = EscreverLerString("Cidade:");

                    var endereco = new Endereco
                    {
                        Rua = rua,
                        Numero = numero,
                        Cep = cep,
                        Estado = estado,
                        Cidade = cidade,
                    };

                    cliente = new Cliente(cpf, nome, idade, sexo, endereco);
                }
                else
                {
                    cliente = new Cliente(cpf, nome, idade, sexo);
                }


                if (cliente.EhMaiorDeIdade())
                {
                    var cnh = EscreverLerString("\nTem carteira de motorista (s/n):");
                    if (cnh.Equals("s"))
                        cliente.InformarCartieraMotorista(EscreverLerString("Informe a carteira de motorista: "));

                    if (sexo == EnumSexo.Masculino)
                        cliente.InformarCartieraReservista(EscreverLerString("Informe a certidão de reservista:"));
                }

                Lista.Add(cliente);
                new SalvarRecuperarCliente().Salvar(Lista);

                var opt = EscreverLerString("\nDeseja preencher outro cliente? s/n");
                executando = opt == "n" ? false : true;

            } while (executando);
        }

        public void Listar()
        {
            LimparTela();
            Escrever("== Listar clientes ==");

            if (Lista.Capacity == 0)
            {
                Escrever("\n\nNão há clientes cadastrados");
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

                var numSelec = EscreverLerString("\n\nInsira o id do cliente para ver mais detalhes ou deixe em branco para voltar:");

                if (!string.IsNullOrWhiteSpace(numSelec))
                {
                    var pos = int.Parse(numSelec);
                    if (pos > 0 && pos <= Lista.Capacity)
                        Detalhes(pos - 1);
                }
            }

        }

        public void Detalhes(int index)
        {
            LimparTela();
            var c = Lista.ElementAt(index);
            Escrever($"== Detalhes do cliente ==");
            Escrever("\n" + c.RetornaDados());
            AguardarTela("\n\nPressione qualquer tecla para voltar");
        }

        private bool CheckCpfUnico(string cpf)
        {
            var unique = Lista.Where(c => c.Cpf == cpf).FirstOrDefault() != null;
            if (unique) Escrever("\n** Já existe um cliente cadastrado com este CPF **\n");
            return unique;
        }

        private void CarregarDados()
        {
            Lista = new SalvarRecuperarCliente().PegarTodos();
        }

    }
}