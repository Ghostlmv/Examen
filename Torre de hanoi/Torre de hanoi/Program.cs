using System;
using System.Collections.Generic;

class TorresDeHanoiConsola
{
    static int numDiscos;
    static Stack<int>[] postes = new Stack<int>[3];
    static int movimientos = 0;

    static void Main()
    {
        Console.WriteLine("¡Bienvenido a las Torres de Hanoi!");

        int nivelDificultad;
        do
        {
            Console.WriteLine("Elige el nivel de dificultad:");
            Console.WriteLine("1. Fácil (3 discos)");
            Console.WriteLine("2. Media (5 discos)");
            Console.WriteLine("3. Difícil (7 discos)");
            Console.WriteLine("4. Resolver Torres de Hanoi automáticamente");
            Console.Write("Ingresa el número de la dificultad: ");


        } while (!int.TryParse(Console.ReadLine(), out nivelDificultad) || (nivelDificultad< 1 || nivelDificultad> 4));


        if (nivelDificultad == 1)
        {
            numDiscos = 3;
        }
        else if (nivelDificultad == 2)
        {
            numDiscos = 5;
        }
        else if (nivelDificultad == 3)
        {
            numDiscos = 7;
        }
        else if (nivelDificultad == 4)
        {
            Console.WriteLine("Seleccione la dificultad para ver la solución:");
            Console.WriteLine("1. Fácil (3 discos)");
            Console.WriteLine("2. Media (5 discos)");
            Console.WriteLine("3. Difícil (7 discos)");
            Console.Write("Ingresa el número de la dificultad: ");

            int dificultadSeleccionada;
            do
            {
                if (!int.TryParse(Console.ReadLine(), out dificultadSeleccionada) || dificultadSeleccionada < 1 || dificultadSeleccionada > 3)
                {
                    Console.WriteLine("Entrada no válida. Ingresa un número de dificultad válido (1, 2 o 3):");
                }
            } while (dificultadSeleccionada < 1 || dificultadSeleccionada > 3);

            int numDiscosSeleccionados = 0;

            switch (dificultadSeleccionada)
            {
                case 1:
                    numDiscosSeleccionados = 3;
                    break;
                case 2:
                    numDiscosSeleccionados = 5;
                    break;
                case 3:
                    numDiscosSeleccionados = 7;
                    break;
            }

            Console.WriteLine($"Solución de las Torres de Hanoi para la dificultad {dificultadSeleccionada}:");
            MostrarSolucionTexto(numDiscosSeleccionados, 0, 2, 1);
            Console.WriteLine("Fin de la solución.");
            Console.ReadKey();
            return;
        }



        InicializarJuego();
        ImprimirTorres();

        while (!JuegoCompletado())
        {
            Console.WriteLine("Ingresa el número del poste de origen:");
            int origen;
            while (!int.TryParse(Console.ReadLine(), out origen) || origen < 1 || origen > 3)
            {
                Console.WriteLine("Entrada no válida. Ingresa un número de poste válido (1, 2 o 3):");
            }

            Console.WriteLine("Ingresa el número del poste de destino:");
            int destino;
            while (!int.TryParse(Console.ReadLine(), out destino) || destino < 1 || destino > 3)
            {
                Console.WriteLine("Entrada no válida. Ingresa un número de poste válido (1, 2 o 3):");
            }

            origen--;
            destino--;

            if (MoverDisco(origen, destino))
            {
                ImprimirTorres();
                movimientos++;
            }
            else
            {
                Console.WriteLine("Movimiento no válido. Intenta de nuevo.");
            }
        }

        Console.WriteLine("¡Felicidades, ya ganaste!");
        Console.WriteLine($"Movimientos realizados: {movimientos}");
        Console.ReadKey();
    }
    static void MostrarSolucionTexto(int n, int origen, int destino, int auxiliar)
    {
        if (n == 1)
        {
            Console.WriteLine($"Mueve el disco 1 de poste {origen + 1} a poste {destino + 1}");
            return;
        }

        MostrarSolucionTexto(n - 1, origen, auxiliar, destino);
        Console.WriteLine($"Mueve el disco {n} de poste {origen + 1} a poste {destino + 1}");
        MostrarSolucionTexto(n - 1, auxiliar, destino, origen);
    }

    

    static void InicializarJuego()
    {
        for (int i = 0; i < 3; i++)
        {
            postes[i] = new Stack<int>();
        }

        for (int disco = numDiscos; disco >= 1; disco--)
        {
            postes[0].Push(disco);
        }
    }

    static void ImprimirTorres()
    {
        Console.Clear();
        for (int nivel = numDiscos; nivel >= 1; nivel--)
        {
            for (int poste = 0; poste < 3; poste++)
            {
                if (postes[poste].Count >= nivel)
                {
                    int[] discosEnPoste = postes[poste].ToArray();
                    int disco = discosEnPoste[discosEnPoste.Length - nivel];
                    Console.Write(new string(' ', numDiscos - disco));
                    Console.Write(new string('█', disco * 2 - 1));
                    Console.Write(new string(' ', numDiscos - disco));
                }
                else
                {
                    Console.Write(new string(' ', numDiscos * 2 + 1));
                }
            }
            Console.WriteLine();
        }

        Console.WriteLine(new string('-', numDiscos * 6));
        Console.WriteLine("1".PadLeft(numDiscos * 2) + "2".PadLeft(numDiscos * 3) + "3".PadLeft(numDiscos * 3));
    }

    static bool MoverDisco(int origen, int destino)
    {
        if (postes[origen].Count == 0)
        {
            return false;
        }

        if (postes[destino].Count == 0 || postes[origen].Peek() < postes[destino].Peek())
        {
            int disco = postes[origen].Pop();
            postes[destino].Push(disco);
            return true;
        }

        return false;
    }

    static bool JuegoCompletado()
    {
        return postes[2].Count == numDiscos;
    }
}
