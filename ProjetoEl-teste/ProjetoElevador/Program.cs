using System;
using ProjetoElevador.Controllers;
using ProjetoElevador.Models;

namespace ProjetoElevador
{
    internal class Program
    {
        static void Main(string[] args)
        {

            ElevadorController elevadorController = new();

            elevadorController.Inicializar();



        }
    }
}
