using ProjetoElevador.Models;
using ProjetoElevador.Views;
using System;
using System.Linq;

namespace ProjetoElevador.Controllers
{
    internal class ElevadorController
    {

        public static Elevador elevador = new();           //criando um objeto do tipo elevador: ações e props básicas do elevador(Model).

        public static ElevadorView elevadorView = new();   //chama a classe View do elevador: requerimetos e retornos ao usuário.

        bool?[] paradas;

        /*Será um array cujo tamanho é a quantidade de andares, contando com o térreo. 
         * Se o valor paradas[i] for true, significa que o elevador foi chamado por um usuário que está no andar i.
         * Se paradas[i] = false, significa que um usuário no interior do elevador escolheu sair no andar i pelo painel interno.
         * Se paradas[i] = null, o elevador não foi chamado no andar i nem interna nem externamente, então ele não deve parar.
         * Este array é constantemente atualizado no decorrer do programa de acordo com os inputs do usuário na View.
        
        Exemplo: em um elevador de 4 andares + térreo, se tivermos um array paradas={null,true,null,false,false},
        o elevador não foi chamado no térreo e nem no andar 2, mas recebeu uma chamada externa no andar 1 e chamadas internas
        (no Painel do elevador) para sair nos andares 3 e 4.
        */

        
        string status = "Parado";//status será uma variável que poderá assumir os valores "Subindo", "Descendo" ou "Parado". 


        public void Inicializar()
        {
            //Pedindo as informações do elevador na View e colocando no objeto elevador criado.

            int[] config = elevadorView.Inicializar();         //Olhar documentação do método elevadorView.Inicializar().
            int configQtdeAndares = config[0];
            int configCapacidade = config[1];
            if (configQtdeAndares > 0 & configCapacidade > 0)  //verificando se as entradas são válidas.
            {
                elevador.Inicializar(configCapacidade, configQtdeAndares);
                //recebe os dados da View de quantidade de andares e capacidade e chama o método da model para inicializar o elevador.


                paradas = new bool?[elevador.qtdeAndares + 1];//definido o tamanho do array paradas = quantidade de andares
                                                              //contando com o térreo(+1).

                for (int i = 0; i < paradas.Length; i++)     //iniciando o array com o valor "null" em todas as entradas
                {
                    paradas[i] = null;
                }


                elevadorView.Inicializou(true);            //Comunicando ao usuário que o elevador foi inicializado
                this.Ativar();                             //Começando o método principal: método elevadorController.Ativar();
            }
            else
            {
                elevadorView.Inicializou(false);//Comunica ao usuário que os dados são inválidos.
                this.Inicializar();             //Pede novamente que configure o elevador.
            }
        }

        //Método Principal da Controller
        public void Ativar()
        { 	   
            
	    bool?[] andaresMaiores = new bool?[elevador.qtdeAndares + 1 - elevador.andarAtual];
            Array.Copy(paradas, elevador.andarAtual, andaresMaiores, 0, elevador.qtdeAndares + 1 - elevador.andarAtual);
            /*é um subarray do array paradas, só com os índices referentes aos andares maiores que o atual. Com isso
            podemos verificar se o elevador precisa continuar subindo ou se não foi chamado nesses andares, podendo parar
            ou ir para os andares inferiores.*/

            bool?[] andaresMenores = new bool?[elevador.andarAtual];
            Array.Copy(paradas, andaresMenores, elevador.andarAtual);
            /*mesma ideia do subarray, mas para os andares menores. 
             * Usamos os arrays para identificar a proxima direção no switch a seguir:*/

            switch (status) //Responsável por definir a ação do elevador e/ou sua mudança de status.

            {
                case "Subindo"://se o elevador já está subindo, ele continua subindo até que não tenha que parar em nenhum andar acima dele.
                               //Quando isso acontece, ele para e decide o que fazer em seguida.

                    if (elevador.andarAtual < elevador.qtdeAndares & !andaresMaiores.All(i => i == null))//segunda condição significa: "se existir pelo menos 
													 //um elemento diferente de null no array andaresMaiores". 
                    { elevador.Subir(); }

                    else { status = "Parado"; }  //quando o elevador chega ao último andar, não dá mais para subir.
                    break; 



                case "Descendo"://quando o elevador já está descendo, ele continua até que não tenha nenhum andar para parar abaixo dele.

                    if (elevador.andarAtual > 0 & !andaresMenores.All(i => i == null))
                    { elevador.Descer(); }
                    else { status = "Parado"; } //quando o elevador chega ao térreo não dá mais para descer.
                    break;       



                case "Parado":/*Neste programa, o status do elevador só é "Parado" quando não há mais paradas na
                              direção em que está indo. Então se estiver subindo, é hora de descer. Se estiver
                              descendo, é hora de subir. Se não há nenhuma outra parada, ele continua parado.
                              (Obs: ainda que coloquemos na View às vezes que o elevador está "Parado" quando alguém entra ou sai,

                              a variável status pode não ter o valor "Parado").*/


                    if (paradas[elevador.andarAtual] != null) { } //se existe uma parada no andar atual, não fazemos nada aqui. Assim,	
								  //quando o método this.Continuar() for chamado depois deste switch,
								  //o elevador abrirá neste andar.	
							          	
                    else if (andaresMaiores.All(i => i == null) & !andaresMenores.All(i => i == null)) 
                    {
                        status = "Descendo"; //se todos os andares maiores que o atual não foram chamados
                                             //enquanto pelo menos um menor que o atual foi chamado, o elevador desce.
                    }
                    else if (andaresMenores.All(i => i == null) & !andaresMaiores.All(i => i == null))
                    {
                        status = "Subindo";  //Similarmente, o elevador sobe se não precisar mais descer quando existem
				             //chamadas em andares superiores.
                    }
                    else status = "Parado"; //se não há chamadas em nenhum andar, o elevador para.

                    break;

                default://nenhum outro valor entrará como status do elevador
                    break;
            }


		//---Tela Principal do Andar---//

            elevadorView.Visor(elevador.andarAtual, status);							   //Mostra o andar e o status
            elevadorView.Informacoes(elevador.lotacaoAtual, elevador.capacidadeMax, elevador.qtdeAndares, paradas);//Mostra a lotação atual, a capacidade,
													           //o último andar e os andares 
													           //selecionados no painel interno.


            this.Continuar();  //Método da Controller responsável por fazer alguma parada no andar atual, 
			       //caso o elevador tenha sido chamado interna ou externamente.


            string inputUsuario = elevadorView.Menu(); //Mostra o Menu e pede que o usuário escolha uma das ações.
                                                       //Clicar em enter, digitar "1","2" ,"3" ou "4", recebendo a resposta em string na variável.


            switch (inputUsuario) //Responsável por realizar a ação escolhida no menu com métodos da Controller.
            {
                case "": 

                    this.Continuar(); //enter:Continuar se o andar não for uma parada do elevador, não faz nada além de seguir para a próxima.
                         	      //Se for, o elevador fica parado e possibilita saída e/ou entrada de usuários.
                    
                    break;


                case "1":
                    ChamarElevador();  //Simula um usuário fora do elevador apertando o botão de chamar o elevador.
				       //Para isso, ele informa o seu andar e o programa registra uma chamada no andar.               
                    break;

                case "2":
                    this.SelecionarAndaresPainel();//Abre o painel de botões internos do elevador, onde um usuário pode a qualquer momento
					           //selecionar um andar. O elevador continuará seguindo seu percurso e parará em cada andar
						   //selecionado nos próximos passos do loop para a entrada/saída de usuários. 

                    break;

                case "3":

                    this.Sair();         //Pessoas no elevador podem optar por sair seja qual for o andar em que estiverem.
					 //(o elevador ainda irá parar nos andares do painel, assim como um elevador de verdade).

                    break;

                case "4":  			//Reconfigurar o elevador. Recomeça o programa. Este input é usado para que o programa
						//decida não chamar o método atual novamente e no lugar deçe chame o this.Inicializar();
                    break;
                case "5": 			//Encerrar o aplicativo. Não faz nada até o fim do método, onde a função Ativar()
                          			//não será mais chamada por causa deste input.
                    break;

                default:
                    elevadorView.Inicializou(false); 	//informa que o input é inválido caso não seja nenhum dos casos acima.
                    break;


            }

            this.Continuar();//caso exista algum pedido de parada de última hora no menu acima.
           
            if (inputUsuario != "4" & inputUsuario != "5") { Ativar(); }//Repete o método, a menos que o usuário tenha pedido para reconfigurar ou encerrar.

            if (inputUsuario == "4") { this.Inicializar(); }//Inicializa o elevador novamente.
        }

    


        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        //Métodos usados pelo método Ativar()


        /// <summary>
        /// Não faz nada se paradas[elevador.andarAtual]=null. Se for true, para o elevador e executa o método 
        /// ElevadorController.Entrar(), se for false, para e executa ambos ElevadorController.Sair() 
        /// e ElevadorController.Entrar().
        /// </summary>
        public void Continuar()
        {
            if (paradas[elevador.andarAtual] == true)//entrar
            {
                elevadorView.Visor(elevador.andarAtual, "Chegou - " + status);
                elevadorView.Informacoes(elevador.lotacaoAtual,elevador.capacidadeMax,elevador.qtdeAndares,paradas);
                this.Entrar(); //Pergunta quantas pessoas querem entrar, já que o elevador foi chamado externamente.
              

            }
            else if (paradas[elevador.andarAtual] == false) //sair
            {
                elevadorView.Visor(elevador.andarAtual, "Chegou - " + status);
                elevadorView.Informacoes(elevador.lotacaoAtual, elevador.capacidadeMax, elevador.qtdeAndares, paradas);
                this.Sair(); //Método da Controller:
                             //pergunta quantos desejam sair, já que o elevador foi levado para o andar
                             //por requisição interna (Painel do Elevador) - olhar método.

                this.Entrar();//Método da Controller:
                              //Também pergunta se alguém quer entrar, já que a porta está aberta.
               

            }
          
        }

        /// <summary>
        /// Pergunta e recebe o andar em que o elevador foi chamado e modifica o array paradas, colocando o valor 
        /// true no índice correspondente ao andar, caso ele seja null.
        /// </summary>
        public void ChamarElevador()
        {
            string stringAndar = elevadorView.ChamarElevador();
            bool intvalido = int.TryParse(stringAndar,out int andarChamado); //pergunta o andar em que chamaram o elevador e verifica se é inteiro
            if (intvalido & andarChamado >= 0 & andarChamado <= elevador.qtdeAndares) //verifica se o número é válido
            {
                if (paradas[andarChamado] != false)
                { paradas[andarChamado] = true; }//coloca o valor da lista de paradas como true no índice
                                                 //do andar se ele for null, pois o elevador vai precisar parar no andarChamado
                                                 //e perguntar se alguém quer entrar. Mas, se o valor dele for false,
                                                 //ele já vai parar para alguém sair e depois perguntar se alguém
                                                 //quer entrar. Logo, não precisamos fazer nada nesse caso.

                 
                elevadorView.Chamou(true);      //Confirma que o elevador está a caminho.
            }  

            else if (stringAndar == "") { }     //Não faz nada se o usuário clicar em ENTER - desistir de chamar.

            else { elevadorView.Chamou(false); this.ChamarElevador(); }//Diz que o pedido é inválido e pede informações novamente.
            
        }



        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        /// <summary>
        /// Pergunta quantas pessoas querem entrar, recebe, verifica e chama o método Entrar do elevador
        /// a quantidade de vezes necessária. Depois retorna se deu certo e a lotação atualizada
        /// pela view. Em seguida, chama o método SelecionarAndaresPainel(), para que as pessoas 
        /// que entraram possam escolher selecionar andares para sair. 
        /// </summary>
        public void Entrar()
        {
            paradas[elevador.andarAtual] = null;//retira o sinal de parada do andar atual, para que o elevador
            string inputString = elevadorView.Entrar();

            bool valida = int.TryParse(inputString, out int qtdePessoas);
            //pergunta quantas pessoas querem entrar e verifica se o número é inteiro.

            if (valida & qtdePessoas > 0 & qtdePessoas <= (elevador.capacidadeMax - elevador.lotacaoAtual))//verifica se o número é válido
            {

                for (int i = 1; i <= qtdePessoas; i++)//adiciona pessoas ao objeto elevador de acordo com o número informado
                { elevador.Entrar(); }

                elevadorView.Entrou(true, elevador.lotacaoAtual, elevador.capacidadeMax, qtdePessoas);
                //informa quantas pessoas entraram e qual a lotação atual


                this.SelecionarAndaresPainel();    //Abre o painel automaticamente toda vez que alguém entra!
						   //Método da Controller: olhar sua documentação.                   


            }
            else if ((valida & qtdePessoas==0 )||inputString=="")  //Caso em que entraram 0 pessoas (não faz nada, apenas mostra o Visor e info novamente).

            { 
                elevadorView.Visor(elevador.andarAtual, "Ainda parado - " + status);
                elevadorView.Informacoes(elevador.lotacaoAtual, elevador.capacidadeMax, elevador.qtdeAndares, paradas);
            }

            else     //input inválido
            {
                elevadorView.Entrou(false, elevador.lotacaoAtual, elevador.capacidadeMax, 0);  //Informa que a entrada não foi possível e a capacidade máxima.

                this.Entrar();						//Pede que o usuário informe novamente quantas pessoas querem entrar.
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        ///Pergunta se os usuários querem selecionar andares no painel do elevador e quais.
        /// Se forem válidos, muda os valores para false nos índices
        /// correspondentes a cada andar pedido. Faz isso até que não se queira mais selecionar andares.
        /// </summary>
        public void SelecionarAndaresPainel()
        {

            string inputString = elevadorView.PainelElevador(elevador.lotacaoAtual, elevador.capacidadeMax, elevador.qtdeAndares, paradas);
            //Selecionando um andar no painel interno e recebendo o input em string


            bool intvalido = int.TryParse(inputString, out int andarSelecionado); //verifica se é um inteiro;

            if (intvalido & andarSelecionado >= 0 & andarSelecionado <= elevador.qtdeAndares)  //verifica se é número válido para um andar
            {
                paradas[andarSelecionado] = false;	//modifica a entrada do andar no array paradas para que o elevador saiba
                                                  	//que deve parar no andar e perguntar quantos querem sair.

                elevadorView.AndarSelecionado(true);	//informa que a seleção foi feita
            }
            else
                elevadorView.AndarSelecionado(false);   //informa que o andar é inválido.


            while (inputString != "")         //loop que continua perguntando se o usuário quer selecionar outros andares
            {


                inputString = elevadorView.SelecionarOutro(elevador.lotacaoAtual, elevador.capacidadeMax, elevador.qtdeAndares, paradas);
                intvalido = int.TryParse(inputString, out andarSelecionado);
                if (intvalido & andarSelecionado >= 0 & andarSelecionado <= elevador.qtdeAndares)
                {
                    paradas[andarSelecionado] = false;  //modifica a entrada do andar no array paradas para que o elevador saiba
                                                         //que deve parar no andar e perguntar quantos querem sair.
                    elevadorView.AndarSelecionado(true); //pergunta se o usuário quer selecionar outro
                                                         //andar e recebe true ou false
                }
                else if (inputString != "")
                    elevadorView.AndarSelecionado(false);


            }
	
		//Coloca Visor e Informações na tela novamente, pois foram apagadas pelos métodos da View chamados acima.

            elevadorView.Visor(elevador.andarAtual, "Ainda parado - " + status);
            elevadorView.Informacoes(elevador.lotacaoAtual, elevador.capacidadeMax, elevador.qtdeAndares, paradas);

        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------


        /// <summary>
        /// Pergunta quantas pessoas querem sair, recebe, verifica e chama o método Sair do elevador
        /// a quantidade de vezes necessária. Depois retorna se deu certo e a lotação atualizada pela view. 
        /// </summary>
        public void Sair()
        {   
            string inputString = elevadorView.Sair();	//pergunta quantas pessoas querem sair no andar e recebe o valor em string
            bool valida = int.TryParse(inputString, out int qtdepessoas); //verifica se o input é um inteiro

            if (valida&qtdepessoas > 0 & qtdepessoas <= elevador.lotacaoAtual)//verifica se o número é válido para quantidade de pessoas que querem sair
            {
                for (int i = 0; i < qtdepessoas; i++)
                {
                    elevador.Sair();//chama o método Sair do objeto elevador a quantidade de vezes
                                    //necessárias.
                }

               
                elevadorView.Saiu(true, elevador.lotacaoAtual, qtdepessoas);
                //informa quantas pessoas saíram e a lotação atual

            }
            else if ((valida&qtdepessoas==0)||inputString == "") //Caso em que 0 pessoas saíram - Não faz nada
            {  }
            else            
            {
                elevadorView.Saiu(false, elevador.lotacaoAtual, 0);//informa que o valor é inválido
                this.Sair();//Repete o método para que a pessoa informe novamente quantos querem sair.
            }
            paradas[elevador.andarAtual] = null;		//retira o sinal de parada do andar atual


        }


        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------













    }
}


