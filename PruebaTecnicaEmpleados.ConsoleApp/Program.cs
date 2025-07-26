using System;


namespace PruebaTecnicaEmpleados.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("--- Ejercicio 3: Números del 0 al 100 de 3 en 3 (sin bucles) ---");
            ImprimirDeTresEnTres(0);
            Console.WriteLine("\n");
            Console.WriteLine("--------------------------------------------------------------\n");
            Console.WriteLine("--- Ejercicio 4: Combinaciones posibles de una cadena ---");
            string[] entradas = { "hat", "abc", "Zu6" };
            for (int i = 0; i < entradas.Length; i++)
            {
                var resultado = new List<string>();
                CombinacionesP("", entradas[i], resultado);
                Console.WriteLine($"{i + 1} {string.Join(",", resultado)}");
            }
        }


       
        public static void ImprimirDeTresEnTres(int numero)
        {
            if (numero > 100)
                return;

            Console.WriteLine(numero);
            ImprimirDeTresEnTres(numero + 3);

        }

        public static void CombinacionesP(string prefijo, string resto, List<string> resultado)
        {
            if (resto.Length == 0)
            {
                resultado.Add(prefijo);
            }
            else
            {
                for (int i = 0; i < resto.Length; i++)
                {
                    CombinacionesP(prefijo + resto[i], resto.Substring(0, i) + resto.Substring(i + 1), resultado);
                }
            }
        }
    }
}
