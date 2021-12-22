using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoElevador.Views
{
    internal class ElevadorView
    {
        /// <summary>
        /// Retorna um array de tamanho 2 contendo a quantidade de andares do prédio 
        /// e sua capacidade, respectivamente, informadas pelo usuário.
        /// </summary>
        /// <returns></returns>
        public int[] Configurar() 

        {
           
            int[] Configuracao = new int[2];

            Console.WriteLine(@"            ---Configurando o elevador---           ");
            Console.WriteLine("Desconsiderando o térreo, quantos andares há no prédio?");
            Configuracao[0] = int.Parse(Console.ReadLine());
            Console.WriteLine("\n Qual é o número máximo de pessoas (capacidade) que o elevador suporta?");
            Configuracao[1] = int.Parse(Console.ReadLine());

            return Configuracao;
        }
        public void Inicializou(bool simOuNao)
        {
            if (simOuNao)
            {
                Console.WriteLine("Elevador inicializado com sucesso.");
            }else
                Console.WriteLine("Os valores informados não são válidos.");
        }


        public int ChamarElevador()
        {           
            Console.WriteLine("         ---Chamar o elevador---         ");
            Console.WriteLine("Em que andar você se encontra? ");
            int entrada= int.Parse(Console.ReadLine());
            return entrada;
           
            //            Console.WriteLine($"O elevador está no andar {andarAtual}.");
            //            Console.WriteLine(@"Quantas pessoas desejam entrar?
            //");

            //            int entradaPessoas = int.Parse(Console.ReadLine());

            //            Console.WriteLine(@"
            //Quantas pessoas desejam sair?");

            //            int saidaPessoas = int.Parse(Console.ReadLine());
        }

        public bool Entrar();
        public void Visor(int andarAtual,string subindoOuDescendo)
        { Console.WriteLine($"O andar atual é {andarAtual}. {subindoOuDescendo}"); } 

        public int BotaoDoAndar() 
        {
            Console.WriteLine("Para que andar deseja ir?");
            int saida = int.Parse(Console.ReadLine());
            return saida;
        }


    }
}
