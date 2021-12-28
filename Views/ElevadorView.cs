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
        public int[] Inicializar() 

        {
           
            int[] Configuracao = new int[2];

            Console.WriteLine(@"            ---Configurando o elevador---           ");
            Console.WriteLine("Desconsiderando o térreo, quantos andares há no prédio?");
            Configuracao[0] = int.Parse(Console.ReadLine());
            Console.WriteLine("\nQual é o número máximo de pessoas (capacidade) que o elevador suporta?");
            Configuracao[1] = int.Parse(Console.ReadLine());

            return Configuracao;
        }
        //--------------------------------------------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------------------------------------------

        public void Inicializou(bool simOuNao)
        {
            if (simOuNao)
            {
                Console.WriteLine("\nElevador inicializado com sucesso." +
                    "\nO elevador está vazio. Quem quiser entrar precisa chamar o elevador até o seu andar.\n");
            }
            else
            {
                Console.WriteLine("\nOs valores informados não são válidos.\n\n");
            }
            Console.WriteLine("\nPressione Enter para continuar");
            Console.ReadLine();
        }
        //--------------------------------------------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------------------------------------------

        public void Informacoes(int lotacaoAtual, int capacidadeMax, int qtdeAndares, bool?[] paradas)
        {
            Console.WriteLine($@"                                                       Quantidade de pessoas no elevador: {lotacaoAtual}
                                                       Capacidade Máxima: {capacidadeMax} pessoas
                                                       Último Andar: {qtdeAndares}");

            Console.WriteLine($@"                                                       Andares Chamados");
            for (int i = 0; i < paradas.Length; i++)
            {
                if (paradas[i] != null)
                    Console.WriteLine($@"                                                             {i}");
            }
            Console.WriteLine("");
        }

        //--------------------------------------------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------------------------------------------
        public void Visor(int andarAtual, string subindoOuDescendo)
        {
            string andarAtualstring = Convert.ToString(andarAtual);

            if (andarAtualstring == "0")
            {
                andarAtualstring = "térreo";
            }

            Console.WriteLine($"\nAndar atual: {andarAtualstring} - {subindoOuDescendo}");
        }

        //--------------------------------------------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------------------------------------------

        public string Menu()
        {
            Console.WriteLine("\n\n" + @"Escolha uma das ações:

Enter - Continuar;
1 - Alguém chamou o elevador;
2 - Selecionar um novo andar;
3 - Alguém deseja sair neste andar;
4 - Encerrar o aplicativo.
");
            return Console.ReadLine();
        }

        //--------------------------------------------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------------------------------------------
        public int ChamarElevador()
        {
           
            Console.WriteLine("\n             ---Chamar o elevador---           ");
            Console.WriteLine("Em que andar o elevador foi chamado? (0 - térreo)");
            bool inteiro = int.TryParse(Console.ReadLine(), out int qualAndar);
            
                return qualAndar;         
       
        }
        public void Chamou(bool simOuNao) 
        {
            if (simOuNao)
            {
                Console.WriteLine($"\nO elevador está a caminho.");
            }
            else Console.WriteLine("\n Andar inválido");
            Console.ReadLine();
        }


        //--------------------------------------------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------------------------------------------


        public int Entrar()
        {
            Console.WriteLine($"\nQuantas pessoas desejam entrar? (Enter - ninguém)");
            string andarEntrada = Console.ReadLine();
            if (int.TryParse(andarEntrada, out int result))
            { return result; }

            else if (andarEntrada == "")

            { return 0; }

            else return -1;
        }
        public void Entrou(bool simOuNao, int lotacaoAtual, int capacidadeMax, int quantosEntraram)
        { if (simOuNao)
            { Console.WriteLine($"\n{quantosEntraram} pessoas entraram. Quantidade de pessoas no elevador: " + lotacaoAtual); }
            else
            { Console.WriteLine($@"
O elevador não suporta a entrada dessa quantidade de pessoas ou o número informado é inválido.
Tente entrar com uma quantidade que não ultrapasse o limite de {capacidadeMax} pessoas ou esperar outro momento.
"); }
        }
        //--------------------------------------------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------------------------------------------

        public int Sair()
        {
            Console.WriteLine("\nQuantas pessoas desejam sair? (Enter - ninguém)");
            string andarSaida = Console.ReadLine();
            if (int.TryParse(andarSaida, out int result))
            { return result; }
            else if (andarSaida == "")
            {return 0;} 
            else return -1;
        }
        public void Saiu(bool simOuNao, int lotacaoAtual, int quantosSairam)
        {

            if (simOuNao)
            { 
                Console.WriteLine($"\n{quantosSairam} pessoas saíram. Quantidade de pessoas no elevador: " + lotacaoAtual);
                Console.ReadLine();
            }
            else
            { Console.WriteLine($"\nPedido inválido. Quantidade de pessoas no elevador:{lotacaoAtual}.");}

           

        }


        //--------------------------------------------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------------------------------------------

        public string PainelElevador() 
        {
            Console.WriteLine("\n\n"+@"           ---Painel interno do elevador---            ");       

            Console.WriteLine("\n"+@"Qual andar deseja selecionar no painel? 
(pressione Enter caso não queira selecionar)");
            string saida = Console.ReadLine();
            return saida;

        }

        /// <summary>
        /// retorna true se o usuário quer selecionar mais andares no painel.
        /// retorna false para terminar a seleção.
        /// </summary>
        /// <param name="simOuNao"></param>
        public void AndarSelecionado(bool simOuNao)
        {
            if (simOuNao)
            {
                Console.WriteLine("\n" + @"Andar selecionado com sucesso.");
            }
            else { Console.WriteLine(@"Andar inválido"); }
        }

        public string SelecionarOutro()
        {            
            Console.WriteLine("\n" + @"Que outro andar deseja selecionar? (Enter - Sair do painel)");
            string saida = Console.ReadLine();
            return saida;

        }


        //--------------------------------------------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------------------------------------------




        

    }
}
