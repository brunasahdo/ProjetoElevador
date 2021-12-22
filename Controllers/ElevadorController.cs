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
        
        Elevador elevador = new();
            
        ElevadorView elevadorView = new();

        public void Inicializar()
        {      //Olhar informações do método elevadorView.Configurar().
            int configQtdeAndares = elevadorView.Configurar()[0];
            int configCapacidade = elevadorView.Configurar()[1];
            if (configQtdeAndares >= 0 & configCapacidade >= 0)
            {
                elevador.Inicializar(configQtdeAndares, configCapacidade);
                //recebe os dados da View de quantidade de andares e capacidade e manda
                //a Model inicializar o elevador. 
                elevadorView.Inicializado(true);
            }
            else
            {
                elevadorView.Inicializado(false);
            }
        

        }
        
           
    }

}

