using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjetoElevador.Interface
{
    internal interface IElevador //Interface de acordo com a página do projeto.
    {     
        void Inicializar(int capacidadeMax, int qtdeAndares);
        // ***Inicializar*** : deve receber como parâmetros a capacidade do elevador 
        //e o total de andares no prédio(os elevadores sempre começam no térreo e vazio);
        bool Entrar();
        
        //***Entrar*** : deve acrescentar uma pessoa no elevador(só deve acrescentar se
        //ainda houver espaço);
        bool Sair();
        //***Sair*** : deve remover uma pessoa do elevador(só deve remover se houver
        //alguém dentro dele);
        bool Subir();
        //***Subir*** : deve subir um andar(não deve subir se já estiver no último andar);

        bool Descer();
        //***Descer*** : deve descer um andar(não deve descer se já estiver no térreo);

    }
}
