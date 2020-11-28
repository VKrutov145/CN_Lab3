using System;

namespace CN_Lab3



{
    internal class Program
    {
        private static int cycleCounter = 0;
        public static void Cycle(CPU cpu)
        {
            if (cycleCounter % 2 == 0)
            {
                cpu.NextCycle();
                cpu.readCommand();
            }
            else
            {
                cpu.NextCycle();
                cpu.proceedCommand();
            }
            cycleCounter++;
        }
        public static void Main(string[] args)
        {
            CPU CPU1 = new CPU();
            
            ConsoleKeyInfo key;
            do
            {
                key = Console.ReadKey();
                if (key.Key == ConsoleKey.Enter)
                {
                    Cycle(CPU1);
                }
            }
            while (key.Key != ConsoleKey.Escape); // по нажатию на Escape завершаем цикл
        }
    }
}