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
            Console.WriteLine("\nQual é o número máximo de pessoas (capacidade) que o elevador suporta?");
            Configuracao[1] = int.Parse(Console.ReadLine());

            return Configuracao;
        }
        public void Inicializou(bool simOuNao)
        {
            if (simOuNao)
            {
                Console.WriteLine("Elevador inicializado com sucesso.\n\n");
            }
            else
            {
                Console.WriteLine("Os valores informados não são válidos.\n\n");
            }
        }


        public int ChamarElevador(int andarAtual)
        {           
            Console.WriteLine("             ---Chamar o elevador---           ");
            Console.WriteLine("Em que andar o elevador foi chamado? ");
            int qualAndar= int.Parse(Console.ReadLine());
            Console.WriteLine($"\nO elevador está a caminho. Saindo do andar{andarAtual}");
            return qualAndar;         
       
        }
        public void Chamou(bool simOuNao) 
        {
            if (!simOuNao) 
            {
                Console.WriteLine("Andar inválido"); 
            }
        }

       
        public void Visor(int andarAtual, string subindoOuDescendo)
        {
            Console.WriteLine($"\nO andar atual é {andarAtual} - {subindoOuDescendo}");
        }

        public string Menu()
        {
            Console.WriteLine("Enter para prosseguir ou escolha 1 - Alguém chamou o elevador, 2 - Alguém quer sair");
            return Console.ReadLine();
        }


        public void Lotacao(int lotacaoAtual)
        {
            Console.WriteLine("Quantidade de pessoas no elevador: " + lotacaoAtual);
        }


        public int Entrar(int andar)
        {
            Console.WriteLine($"\nElevador parado no andar {andar}.Quantas pessoas desejam entrar? (0 - desistir de entrar)");

            if (int.TryParse(Console.ReadLine(), out int result))
                return result;
            else return -1;
        }

        public void Entrou(bool simOuNao, int lotacaoAtual,int capacidadeMax, int quantosEntraram)
        { if (simOuNao)
                Console.WriteLine($"\n{quantosEntraram} pessoas entraram. Quantidade de pessoas no elevador: " + lotacaoAtual);
        else
            Console.WriteLine($@"
O elevador não suporta a entrada dessa quantidade de pessoas ou o número informado é inválido.
Tente entrar com uma quantidade que não ultrapasse o limite de {capacidadeMax} pessoas ou esperar outro momento.");
        }
        public int BotaoDoAndar() 
        {
            Console.WriteLine("Para que andar deseja ir?");
            int saida = int.Parse(Console.ReadLine());
            return saida;
        }


        public int Sair()
        {
            Console.WriteLine("Quantas pessoas desejam sair?");
            if (int.TryParse(Console.ReadLine(), out int result))
                return result;
            else return -1;
        }
        public void Saiu(bool simOuNao, int lotacaoAtual, int quantosSairam, int andarAtual)
        {
            if (simOuNao)
                Console.WriteLine($"\n{quantosSairam} pessoas saíram no andar {andarAtual}. Quantidade de pessoas no elevador: " + lotacaoAtual);
            else
                Console.WriteLine($"\nO elevador possui {lotacaoAtual} pessoas! Andar atual:{andarAtual}");
        }
    }
}
