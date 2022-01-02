using System;

namespace ProjetoElevador.Views
{
    internal class ElevadorView
    {
        //--------------------------------------------------------------------------------------------------------------------------
        //Métodos de inicialização
        //--------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Retorna um array de tamanho 2 contendo a quantidade de andares do prédio 
        /// e sua capacidade, respectivamente, informadas pelo usuário.
        /// </summary>
        /// <returns></returns>
        public int[] Inicializar()

        {
            Console.Clear();

            int[] Configuracao = new int[2];
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(@"            ---Configurando o elevador---                ");
            Console.ResetColor();
            Console.WriteLine("Desconsiderando o térreo, quantos andares há no prédio?");
            bool valido1=int.TryParse(Console.ReadLine(), out Configuracao[0]);
            Console.WriteLine("\nQual é o número máximo de pessoas (capacidade) que o elevador suporta?");
            bool valido2 = int.TryParse(Console.ReadLine(),out Configuracao[1]);
    

            if (valido1 & valido2)
                return Configuracao;
            else  return new int[] { -1,-1};
        }

        public void Inicializou(bool simOuNao)
        {
            if (simOuNao)
            {
                Console.WriteLine("\nElevador inicializado com sucesso." +
                    "\nO elevador está vazio. Quem quiser entrar precisa chamar o elevador até o seu andar.\n");
                Console.WriteLine("\nPressione ENTER para continuar");
               


            }
            else
            {


                Console.ForegroundColor = ConsoleColor.Red;

                Console.WriteLine("\nOs valores informados não são válidos. Pressione ENTER para tentar novamente\n\n");
                Console.ResetColor();
            }
            Console.ReadLine();
            
           
        }
        //--------------------------------------------------------------------------------------------------------------------------
        //Métodos da tela principal
        //--------------------------------------------------------------------------------------------------------------------------

        public void Visor(int andarAtual, string subindoOuDescendo)
        {
            Console.Clear();     //Limpa o Console, deixando apenas as informações do andar atual
            Console.WriteLine("");
            string andarAtualstring = Convert.ToString(andarAtual);

            if (andarAtualstring == "0")
            {
                andarAtualstring = "térreo";
            }

            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.ForegroundColor = ConsoleColor. White;
            
            Console.WriteLine(@$"
  Andar atual: {andarAtualstring} - {subindoOuDescendo} 

");
            Console.ResetColor();
        }
        public void Informacoes(int lotacaoAtual, int capacidadeMax, int qtdeAndares, bool?[] paradas)
        {
            Console.WriteLine($@"                                                       Quantidade de pessoas no elevador: {lotacaoAtual}
                                                       Capacidade: {capacidadeMax} pessoas
                                                       Último Andar: {qtdeAndares}");

           

                Console.WriteLine($@"                                                       Andares Chamados");

                for (int i = 0; i < paradas.Length; i++)

                {
                    if (paradas[i] != null)
                        Console.WriteLine($@"                                                             {i}");
                }
                Console.WriteLine("");
            
             
        }
        public string Menu()
        {
            Console.WriteLine("\n\n" + @" A qualquer momento pressione ENTER para continuar sem fazer nenhuma ação,
 ou escolha uma das opções:

    1 - Alguém chamou o elevador;
    2 - Selecionar um novo andar no painel;
    3 - Alguém deseja sair neste andar;
    4 - Reconfigurar elevador;
    5 - Encerrar o aplicativo.
");
            return Console.ReadLine();
        }


        //--------------------------------------------------------------------------------------------------------------------------
        // Métodos de Chamada
        //--------------------------------------------------------------------------------------------------------------------------
        
        //Chamada externa
        public string ChamarElevador()
        {


            Console.WriteLine("               ---Chamar o elevador---                  ");


            Console.WriteLine("Em que andar o elevador foi chamado? (ENTER - desistir da ação)");
            string qualAndar = Console.ReadLine();
            return qualAndar;

        }


        //Confirmação
        public void Chamou(bool simOuNao)
        {
            if (simOuNao)
            {
                Console.WriteLine($"\nO elevador está a caminho. (ENTER - continuar)");
                Console.ReadLine();

            }
            else
            {
               

                Console.WriteLine("");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(" Andar inválido ");
                Console.ResetColor();
    

            }
        }

        //Chamada interna
        public string PainelElevador(int lotacaoAtual, int capacidadeMax, int qtdeAndares, bool?[] paradas)
        {
            Console.Clear();
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n\n" + @"           ---Painel interno do elevador---                 ");
            Console.ResetColor();
            Console.WriteLine("");
            this.Informacoes(lotacaoAtual, capacidadeMax, qtdeAndares, paradas);
            Console.WriteLine("\nSelecione um andar ou pressione ENTER para sair do painel");
            string saida = Console.ReadLine();
            Console.Clear();
            return saida;

        }


        //Confirmação
        /// <summary>
        /// retorna true se o usuário quer selecionar mais andares no painel.
        /// retorna false para terminar a seleção.
        /// </summary>
        /// <param name="simOuNao"></param>
        public void AndarSelecionado(bool simOuNao)
        {

            Console.Clear();
            Console.BackgroundColor = ConsoleColor.DarkMagenta;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("\n\n" + @"           ---Painel interno do elevador---                    ");
            Console.ResetColor();
            if (simOuNao)
            {
                Console.WriteLine("\n" + @"Andar selecionado com sucesso.");


            }
            else 
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(@"Andar inválido"); 
                Console.ResetColor();
            }


        }


        //Continuar fazendo chamadas internas. A confirmação é feita pelo método acima.
        public string SelecionarOutro(int lotacaoAtual, int capacidadeMax, int qtdeAndares, bool?[] paradas)
        {

            this.Informacoes(lotacaoAtual, capacidadeMax, qtdeAndares, paradas);
            Console.WriteLine("\nSelecione outro andar ou pressione ENTER para sair do painel");
            string saida = Console.ReadLine();
            Console.Clear();
            return saida;
        }


        //--------------------------------------------------------------------------------------------------------------------------
        // Métodos de fluxo de pessoas no elevador e suas confirmações
        //--------------------------------------------------------------------------------------------------------------------------


        public string Entrar()
        {
            Console.WriteLine($"\nQuantas pessoas desejam entrar? (ENTER - ninguém)");
            string andarEntrada = Console.ReadLine();
            return andarEntrada;
        }
        public void Entrou(bool simOuNao, int lotacaoAtual, int capacidadeMax, int quantosEntraram)
        {
            if (quantosEntraram != 0 & simOuNao)
            {
                Console.WriteLine($"\n{quantosEntraram} pessoas entraram. Quantidade de pessoas no elevador: " + lotacaoAtual + ". (ENTER - prosseguir para o painel)");

                Console.ReadLine();


            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;

                Console.WriteLine($@"
O elevador não suporta a entrada dessa quantidade de pessoas ou o número informado é inválido.
Tente entrar com uma quantidade que não ultrapasse o limite de {capacidadeMax} pessoas ou esperar outro momento.
");
                Console.ResetColor();
            }


        }
        public string Sair()
        {
            Console.WriteLine("\nQuantas pessoas desejam sair? (ENTER - ninguém)");
            string andarSaida = Console.ReadLine();
            return andarSaida;
        }
        public void Saiu(bool simOuNao, int lotacaoAtual, int quantosSairam)
        {

            if (simOuNao&quantosSairam!=0)
            { 
                Console.WriteLine($"\n{quantosSairam} pessoas saíram. Quantidade de pessoas no elevador: " + lotacaoAtual+ ". (ENTER - continuar)");
                Console.ReadLine();

            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\nPedido inválido. Quantidade de pessoas no elevador:{lotacaoAtual}.");
                Console.ResetColor();
            }
                       

        }


     


        //--------------------------------------------------------------------------------------------------------------------------
        //--------------------------------------------------------------------------------------------------------------------------




        

    }
}
