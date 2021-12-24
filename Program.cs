using System;
using ProjetoElevador.Controllers;
using ProjetoElevador.Models;

namespace ProjetoElevador
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            ElevadorController elevadorController = new ElevadorController();

            elevadorController.Inicializar();



        }
    }
}
