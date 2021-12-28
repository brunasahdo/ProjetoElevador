using ProjetoElevador.Models;
using ProjetoElevador.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoElevador.Controllers
{
    internal class ElevadorController
    {

        public static Elevador elevador = new(); //criando um objeto do tipo elevador: ações e props básicas do elevador(Model).
        public static ElevadorView elevadorView = new(); //chama a classe View do elevador: requerimenros e retornos ao usuário
        bool?[] paradas; /*Será um array cujo tamanho é a quantidade de andares, contando com o térreo. 
                         Se o valor paradas[i]= true, significa que o elevador foi chamado por um usuário que está no andar i
                         Se paradas[i] = false, significa que um usuário no interior do elevador escolheu sair no andar i.
                         Se paradas[i] = null, o elevador não foi chamado no andar i. Então ele não vai parar.
        
                          Exemplo: em um elevador de 4 andares + térreo, teremos um array paradas={null,true,null,false,false}
        que não foi chamado no térreo e nem no andar 2, que recebeu uma chamada externa no primeiro andar e chamadas internas
        (no Painel do elevador) para sair nos andares 3 e 4.
        */

        string inputUsuario; //será a resposta do usuário ao Menu, que pode modificar a ação do elevador.

        string status = "Parado";//status será uma variável que pode ter valores "Subindo", "Descendo" ou "Parado" 


        public void Inicializar()
        {
            //Pedindo as informações do elevador na View e colocando no objeto elevador.

            int[] config = elevadorView.Inicializar(); //Olhar documentação do método elevadorView.Configurar().
            int configQtdeAndares = config[0];
            int configCapacidade = config[1];
            if (configQtdeAndares >= 0 & configCapacidade >= 0)//verificando se as entradas são válidas
            {
                elevador.Inicializar(configCapacidade, configQtdeAndares);
                //recebe os dados da View de quantidade de andares e capacidade e manda a Model inicializar o elevador.


                paradas = new bool?[elevador.qtdeAndares + 1];//definido o tamanho do array paradas = quantidade de andares
                                                              //contando com o térreo.

                for (int i = 0; i < paradas.Length; i++) //iniciando o array com o valor "null" em todas as entradas
                {
                    paradas[i] = null;
                }
                Console.Clear();
                elevadorView.Inicializou(true); //Comunicando ao usuário que o elevador foi inicializado
                this.Ativar();//Começando o programa principal: método elevadorController.Ativar;
            }
            else
            {
                elevadorView.Inicializou(false);//Comunica ao usuário que os dados são inválidos.
                this.Inicializar(); //Pede novamente que configure.
            }
        }

        //Método Principal do Controller
        public void Ativar()
        {
            Console.Clear();//Limpa o Console, deixando apenas as informações do andar atual

            elevadorView.Visor(elevador.andarAtual, status);//Mostra o andar e o status
            elevadorView.Informacoes(elevador.lotacaoAtual, elevador.capacidadeMax,elevador.qtdeAndares,paradas);//Mostra a lotação atual, a capacidade
                                                                                            //e os andares selecionados no painel.

            inputUsuario = elevadorView.Menu();//Mostra o Menu e pede que o usuário escolha uma das ações.
                                               //Clicar em enter, digitar "1","2" ,"3" ou "4".


            switch (inputUsuario)
            {
                case "": this.Continuar();
                    //enter: se o andar não for uma parada do elevador, não faz nada além de seguir para a próxima.
                    //Se for, o elevador fica parado para que usuários saiam e/ou entrem.
                    break;


                case "1":ChamarElevador();//Chamar elevador               
                    break;
                case "2": this.SelecionarAndaresPainel();break;

                case "3": this.Sair(); //Sair:olhar documentação.
                    break;

                case "4": //Encerrar o aplicativo. Não faz nada até o fim do método, onde a função Ativar()
                          //não será mais chamada por causa deste input.
                    break;
                default:
                    elevadorView.Inicializou(false);//informa que o input é inválido
                    break;

                   
            }//Responsável por agir de acordo com o input do Menu

            bool?[] andaresMaiores = new bool?[elevador.qtdeAndares + 1 - elevador.andarAtual];
            Array.Copy(paradas, elevador.andarAtual, andaresMaiores, 0, elevador.qtdeAndares + 1 - elevador.andarAtual);
            /*é um subarray do array andares, só com os índices referentes aos andares maiores que o atual. Com isso
            podemos verificar se o elevador precisa continuar subindo ou se não foi chamado nesses andares, podendo parar
            ou ir para os andares inferiores, caso tenha sido chamado.*/

            bool?[] andaresMenores = new bool?[elevador.andarAtual];
            Array.Copy(paradas, andaresMenores, elevador.andarAtual);
            /*mesma ideia do subarray para os andares menores. 
             * Usamos os arrays para identificar a proxima direção no switch a seguir:*/

            switch (status)
            {
                case "Subindo"://se o elevador já está subindo, ele continua subindo até que não tenha que parar em nenhum andar acima dele.
                               //Quando isso acontece, ele para e decide o que fazer em seguida.

                    if (elevador.andarAtual < elevador.qtdeAndares & !andaresMaiores.All(i => i == null)) 
                    { elevador.Subir(); }

                    else { status = "Parado"; }
                    break;//quando o elevador chega ao último andar, não dá mais para subir



                case "Descendo"://quando o elevador já está descendo, ele continua até que não tenha nenhum andar para parar abaixo dele.
                    if (elevador.andarAtual > 0 & !andaresMenores.All(i => i == null))
                    { elevador.Descer(); }
                    else { status = "Parado"; }
                    break;//quando o elevador chega ao térreo não dá mais para descer



                case "Parado":/*Neste programa, o status do elevador só é "Parado" quando não há mais paradas na
                              direção em que está indo. Então se estiver subindo, é hora de descer. Se estiver
                              descendo, é hora de subir. Se não há nenhuma outra parada, ele continua parado.
                              (mesmo que coloquemos na View que o elevador está "Parado" quando alguém entra ou sai,

                              a variável status pode não ter o valor "Parado").*/
                    if (andaresMaiores.All(i => i == null) & !andaresMenores.All(i => i == null))
                    {
                        status = "Descendo"; //se todos os andares maiores que o atual não foram chamados
                                             //enquanto existe pelo menos um menor que foi chamado, o elevador desce.
                    }
                    else if (andaresMenores.All(i => i == null) & !andaresMaiores.All(i => i == null))
                    {
                        status = "Subindo";  //----//----
                    }
                    break;

                default://nenhum outro valor entrará como status do elevador
                    break;
            }//Responsável por definir a próxima ação do elevador


            if (inputUsuario != "4") { Ativar(); }//Repete o método, a menos que o usuário tenha pedido para encerrar.

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
                elevadorView.Visor(elevador.andarAtual, "Parado");
                this.Entrar(); //Pergunta quantas pessoas querem entrar, já que o elevador foi chamado externamente.
                paradas[elevador.andarAtual] = null;//tira o andar atual do array de paradas
            }
            else if (paradas[elevador.andarAtual] == false) //sair
            {
                elevadorView.Visor(elevador.andarAtual, "Parado");
                this.Sair(); //Método da Controller:
                             //pergunta quantos desejam sair, já que o elevador foi levado para o andar
                             //por requisição interna (Painel do Elevador) - olhar método.

                this.Entrar();//Método da Controller:
                              //Também pergunta se alguém quer entrar, já que a porta está aberta.

                paradas[elevador.andarAtual] = null;
            }
          
        }

        /// <summary>
        /// Pergunta e recebe o andar em que o elevador foi chamado e modifica o array colocando o valor 
        /// true no índice correspondente ao andar.
        /// </summary>
        public void ChamarElevador()
        {
            int andarChamado = elevadorView.ChamarElevador(); //pergunta o andar em que chamaram o elevador
            if (andarChamado >= 0 & andarChamado <= elevador.qtdeAndares) //verifica se o número é válido
            {   
                if (paradas[andarChamado] == null)
                { paradas[andarChamado] = true; }//colocar o valor da lista de paradas como true no índice
                                              //do andar se ele for null, pois o elevador vai precisar parar no andarChamado
                                              //e perguntar se alguém quer entrar. Mas se o valor já for false,
                                              //então ele já vai parar para alguém sair e depois perguntar se alguém
                                              //quer entrar. Então não precisamos fazer nada.
               

                elevadorView.Chamou(true);//Confirma que o elevador está a caminho
            }
            else { elevadorView.Chamou(false); }//Diz que o pedido é inválido
            
        }

        /// <summary>
        ///Pergunta se os usuários querem selecionar andares no painel do elevador e quais.
        /// Se forem válidos, muda os valores do para false nos índices
        /// correspondentes a cada andar pedido. Faz isso até que o usuário 
        /// não queira mais selecionar andares.
        /// </summary>
        public void SelecionarAndaresPainel()
        {

            string inputString = elevadorView.PainelElevador();     //Selecionando um andar no painel interno


            bool intvalido = int.TryParse(inputString, out int andarSelecionado);

            if (intvalido & andarSelecionado >= 0 & andarSelecionado <= elevador.qtdeAndares)
            {
                paradas[andarSelecionado] = false;//modifica a entrada do andar no array paradas para que o elevador saiba
                                                  //que deve parar no andar e perguntar quantos querem sair.
                elevadorView.AndarSelecionado(true);//informa que a seleção foi feita
            }
            else
                elevadorView.AndarSelecionado(false);


            while (inputString != "")
            {
                inputString = elevadorView.SelecionarOutro();
                intvalido = int.TryParse(inputString, out andarSelecionado);

                if (intvalido & andarSelecionado >= 0 & andarSelecionado <= elevador.qtdeAndares)
                {
                    paradas[andarSelecionado] = false;//modifica a entrada do andar no array paradas para que o elevador saiba
                                                      //que deve parar no andar e perguntar quantos querem sair.
                    elevadorView.AndarSelecionado(true);//pergunta se o usuário quer selecionar outro
                                                        //andar e recebe true ou false
                }
                else if (inputString != "")
                    elevadorView.AndarSelecionado(false);

            }
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

            int qtdePessoas = elevadorView.Entrar();//pergunta quantas pessoas querem entrar

            if (qtdePessoas > 0 & qtdePessoas <= (elevador.capacidadeMax - elevador.lotacaoAtual))//verifica que o número é válido
            {

                for (int i = 1; i <= qtdePessoas; i++)//adiciona pessoas ao objeto elevador de acordo com o número informado
                { elevador.Entrar(); }

                elevadorView.Entrou(true, elevador.lotacaoAtual, elevador.capacidadeMax, qtdePessoas);
                //informa quantas pessoas entraram e qual a lotação atual

                this.SelecionarAndaresPainel();//Método da Controller:ver documentação



                paradas[elevador.andarAtual] = null;//retira o sinal de parada do andar atual, para que o elevador
                                                    //não pare novamente nele a menos que seja chamado novamente.



            }
            else if (qtdePessoas == 0)

            { elevadorView.Entrou(true, elevador.lotacaoAtual, elevador.capacidadeMax, qtdePessoas); }

            else
            {
                elevadorView.Entrou(false, elevador.lotacaoAtual, elevador.capacidadeMax, 0);
                //informa que a entrada não foi possível e a capacidade máxima

                this.Entrar();//pede que o usuário informe novamente quantas pessoas querem entrar
            }
        }

        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------

        /// <summary>
        /// Pergunta quantas pessoas querem sair, recebe, verifica e chama o método Sair do elevador
        /// a quantidade de vezes necessária. Depois retorna se deu certo e a lotação atualizada pela view. 
        /// </summary>
        public void Sair()
        {
            int quantasPessoas = elevadorView.Sair();//pergunta quantas pessoas querem sair no andar

            if (quantasPessoas >= 0 & quantasPessoas <= elevador.lotacaoAtual)//verifica se o número é válido
            {
                for (int i = 0; i < quantasPessoas; i++)
                {
                    elevador.Sair();//chama o método Sair do objeto elevador a quantidade de vezes
                                    //necessárias.
                }

                paradas[elevador.andarAtual] = null;//retira o sinal de parada do andar atual
                elevadorView.Saiu(true, elevador.lotacaoAtual, quantasPessoas);
                //informa quantas pessoas saíram e a lotação atual


            }
            else
            {
                elevadorView.Saiu(false, elevador.lotacaoAtual, 0);//informa que o valor é inválido
                this.Sair();//Repete o método para que a pessoa informe novamente quantos querem sair.
            }



        }


        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
        //-----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------













    }
}


