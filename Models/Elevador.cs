using ProjetoElevador.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoElevador.Models
{
    internal class Elevador:IElevador

    {        //Propriedades da classe
       public int andarAtual { get; set; } //Pode assumir valores desde o térreo=0 até 
                                            //último andar
       public int qtdeAndares { get; set; } //desconsiderando o térreo

       public int capacidadeMax { get; set; } //quantidade máxima de pessoas permitida no elevador

        public int lotacaoAtual { get; set; }//quantidade atual de pessoas no elevador.
                                             //Pode assumir valores entre 0 e capacidadeMax

        //--------------------------------------------------------------------------------------------------

        //Métodos implementados pela interface 
       
      
        public void Inicializar(int capacidadeMax, int qtdeAndares)
        {
                this.capacidadeMax = capacidadeMax;
                this.qtdeAndares = qtdeAndares;
                this.andarAtual = 0;   //térreo
                this.lotacaoAtual = 0; //vazio, como pede a interface
            
                          
        
        }

        public bool Entrar()
        {
            if (this.lotacaoAtual < this.capacidadeMax)
            {
                this.lotacaoAtual++;
                return true;
            }
            else
                return false;
        }


        public bool Sair()
        {
            if (this.lotacaoAtual != 0)
            {
                this.lotacaoAtual--;
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool Subir()
        { 
            if (this.andarAtual<this.qtdeAndares)
            {
                this.andarAtual++;
                return true;
            } 
            else 
            { 
                return false; 
            }            
        }


        public bool Descer()
        {
            if (this.andarAtual > 0)
            {
                this.andarAtual--;
                return true;
            }
            else
            {
                return false;
            }
        }



    }
}
