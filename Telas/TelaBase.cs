using System;
using System.Collections.Generic;

namespace introducao_csharp.Telas
{
    public class TelaBase<T>
    {

        protected List<T> Lista { get; set; }

        protected void Escrever(string mensagem)
        {
            Console.WriteLine(mensagem);
        }

        protected string LerString()
        {
            return Console.ReadLine();
        }

        protected string EscreverLerString(string mensagem)
        {
            Escrever(mensagem);
            return LerString();
        }

        protected int LerInt()
        {
            var retorno = 0;
            var mensagem = "";
            var executando = true;
            do
            {
                if (!string.IsNullOrWhiteSpace(mensagem))
                {
                    Escrever(mensagem);
                    mensagem = "";
                }

                try
                {
                    retorno = int.Parse(Console.ReadLine());
                    executando = false;
                }
                catch
                {
                    mensagem = "Opção inválida. Digite novamente.";
                }
            } while (executando);

            return retorno;
        }

        protected int EscreverLerInt(string mensagem)
        {
            Escrever(mensagem);
            return LerInt();
        }

        protected void LimparTela()
        {
            Console.Clear();
        }

        protected void AguardarTela()
        {
            Console.ReadKey();
        }

        protected void AguardarTela(string mensagem)
        {
            Escrever(mensagem);
            AguardarTela();
        }

    }
}