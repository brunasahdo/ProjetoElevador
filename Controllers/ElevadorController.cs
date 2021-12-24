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


        public static Elevador elevador = new();
        public static ElevadorView elevadorView = new();

       

        public void Inicializar()
        {
            int[] config  = elevadorView.Configurar();
        //Olhar informações do método elevadorView.Configurar().
            int configQtdeAndares =config[0];
             int configCapacidade = config[1];
            if (configQtdeAndares >= 0 & configCapacidade >= 0)
            {
                elevador.Inicializar(configCapacidade, configQtdeAndares);
                //recebe os dados da View de quantidade de andares e capacidade e manda
                //a Model inicializar o elevador. 
                elevadorView.Inicializou(true);
                elevadorView.Visor(elevador.andarAtual, "Parado");
                this.showMenu("Parado");
            }
            else
            {
                elevadorView.Inicializou(false);
                this.Inicializar();
               
            }
            
            
        }

      public void showMenu(string subindoOuDescendo="", bool showMenu = true)//segundo argumento é opcional e o seu default é "".
        {
            //elevadorView.Visor(elevador.andarAtual, subindoOuDescendo);
            //this.Menu(subindoOuDescendo);
            while (showMenu) 
            {
                if (subindoOuDescendo == "Parado" & showMenu)
                {
                    this.Menu(subindoOuDescendo);
                    
                }
                else
                    showMenu = this.Menu(subindoOuDescendo);

            } 
        }

        public bool Menu(string subindoOuDescendo)
        {
            string inputUsuario = elevadorView.Menu();
            
            bool continuar = false;
            switch (inputUsuario)
            {
                case "":  break;
                case "1": this.ChamarElevador(); continuar = true; break;
                case "2": this.Sair(); continuar = true; break;
                default: continuar = true; break;
            }
           
           
            elevadorView.Visor(elevador.andarAtual, subindoOuDescendo);
            return continuar;
        }

        public void ChamarElevador()
        {
            int Andar = elevadorView.ChamarElevador(elevador.andarAtual);

            if (Andar >= 0 & Andar <= elevador.qtdeAndares)
            {
                if (elevador.andarAtual < Andar)
                {
                    while (elevador.andarAtual < Andar)
                    {
                        elevador.Subir();
                        this.showMenu("Subindo");
                                                                       
                    }
                   // this.showMenu("Subindo");

                }

                else
                {
                    while (elevador.andarAtual > Andar)
                    {
                        elevador.Descer();
                        this.showMenu("Descendo");
                        
                    }
                   // this.showMenu("Descendo");


                }
                
            }
            else { elevadorView.Chamou(false); this.showMenu("Parado"); }
            this.Entrar();
        }


        public void Entrar()
        {
            int qtdePessoas = elevadorView.Entrar(elevador.andarAtual);
            if (qtdePessoas>=0 & qtdePessoas <= (elevador.capacidadeMax - elevador.lotacaoAtual))
            {
                for (int i = 0; i < qtdePessoas; i++)
                {
                    elevador.Entrar();
                }
                elevadorView.Entrou(true,elevador.lotacaoAtual,elevador.capacidadeMax,qtdePessoas);
            } else
            {
                elevadorView.Entrou(false, elevador.lotacaoAtual,elevador.capacidadeMax,0);
                this.Entrar();
            }

            
        }

        public void Sair()
        {
            int qtdePessoas = elevadorView.Sair();
            if (qtdePessoas >= 0 & qtdePessoas <= elevador.lotacaoAtual)
            {
                for (int i = 0; i < qtdePessoas; i++)
                {
                    elevador.Sair();
                }
                elevadorView.Saiu(true, elevador.lotacaoAtual,qtdePessoas, elevador.andarAtual);
                this.Menu("Parado");
            }
            else 
            {
                elevadorView.Saiu(false, elevador.lotacaoAtual,0, elevador.andarAtual);
               this.Menu("");

            }

           
            

        }


    }

}

