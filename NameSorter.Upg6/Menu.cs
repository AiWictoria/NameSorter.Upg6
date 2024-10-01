﻿using System;
using System.Collections.Generic;
using System.Globalization;

namespace NameSorter
{
    public class Meny
    {
        public Meny()
        {
        }
        public void MainMeny()
        {
            var meny = new Meny();
            while (true)
            {
                Console.Clear(); //Rensar konsolen för att få den mer läsbar
                Console.WriteLine(" ---- Namn Sortering ---- \n" +
                "\n\tVälj en av följande operationer för vad du önskar att göra:" +
                "\n\t1. Lägg till namn" +
                "\n\t2. Sortera (språk)" +
                "\n\t3. Ta bort namn" +
                "\n\t4. Sök efter namn" +
                "\n\tEsc. Avsluta programmet");

                //Switch med readkey för enklare användning + Felhantering med default
                switch (Console.ReadKey(true).Key)
                {

                    //Anropar metod för att addera namn. (vik)
                    case ConsoleKey.D1:

                        break;

                    //Anropar metod för att sortera namn efter önskat språk (joh)
                    case ConsoleKey.D2:

                        break;

                    //Anropar metod för att ta bort namn. (vik)
                    case ConsoleKey.D3:

                        break;

                    //Anropar metod för att söka på namn. (joh)
                    case ConsoleKey.D4:

                        break;

                    //Avslutar whileloopen och programmet
                    case ConsoleKey.Escape:
                        Console.WriteLine("Programmet avslutas...");
                        Environment.Exit(0);
                        break;

                    //Felhantering av val som inte finns i menyn
                    default:
                        Console.WriteLine("Ogiltligt val, vänligen ange ett av valen i menyn");
                        break;
                }
                //Efter visad resultat får användaren trycka på valfri tangent för att återgå till menyn
                Console.WriteLine("\nTryck på valfri tangent för att återgå till menyn...");
                Console.ReadKey();
            }
        }
    }
}
