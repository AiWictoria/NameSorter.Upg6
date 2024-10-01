using System.Data;
using System.Data.OleDb;

namespace NameSorter
{
    class Program
    {
        static void Main(string[] args)
        {

            //Console.WriteLine("Original list:");
            //foreach (var name in names)
            //{
            //    Console.WriteLine(name);
            //}

            //names.Sort();  // Sort the names alphabetically
            //Console.WriteLine("\nSorted list:");
            //foreach (var name in names)
            //{
            //    Console.WriteLine(name);
            //}

            //Console.WriteLine("\nEnter name to search:");
            //string searchName = Console.ReadLine();
            //int index = names.BinarySearch(searchName);
            //if (index >= 0)
            //{
            //    Console.WriteLine($"{searchName} is in the list.");
            //}
            //else
            //{
            //    Console.WriteLine($"{searchName} is not in the list.");
            //}
            //Console.ReadKey();



        }
        public class NameSorter
        {
          
            List<string> names = new List<string>();
            //Metod för att lägga till namn
            public void AddNewNames()
            {
                while (true)
                {
                    Console.WriteLine("Välj ett av följande: " +
                        "\n\t1. Lägga in namn manuellt" +
                        "\n\t2. Ladda upp excelfil med namn" +
                        "\n\tEsc. Återgå till menyn");

                    switch (Console.ReadKey(true).Key)
                    {
                        case ConsoleKey.D1:
                            //AddNamesManually();
                            break;

                        //Lägga till namn via Excellista
                        case ConsoleKey.D2:
                            AddNamesExcel(names);
                            break;

                        //Återgå till meny
                        case ConsoleKey.Escape:
                            returnBackToMenu();
                            break;
                    }

                }
            }
            //Metod för att lägga in namn med excel lista
            public void AddNamesExcel(List<string> names)
            {
                NameSorter namesSorter = new NameSorter();
                //Låter användaren lägga in excel fil + felhantering
                try
                {
                    Console.Write("\n--- Notera att namnen behöver finnas i första kolumnen i din excelfil samt läses in från rad två ---" +
                        "\nExempel: C:\\Users\\Name\\Dokuments\\Filename\n" +
                        "\nVänligen ange sökväg till excelfilen för att förtsätta: ");

                    string filePath = Console.ReadLine();

                    // Kontrollerar mellanslag och tom inmatning
                    if (string.IsNullOrWhiteSpace(filePath))
                    {
                        Console.WriteLine("Ogiltig filväg. Filvägen behöver se ut enligt följande: \nExempel: C:\\Users\\Name\\Dokuments\\Filename\n");
                        namesSorter.returnBackToMenu();
                    }

                    string connectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={filePath};Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\"";
                    // Om filväg existerar går programmet vidare till att ansluta till excelfilen och efterfrågar blad
                    using (OleDbConnection connection = new OleDbConnection(connectionString))
                    {
                        connection.Open();

                        Console.Write("\nVänligen ange bladnamn där namn ska hämtas ifrån: ");
                        string sheetName = Console.ReadLine();

                        // Kontrollerar mellanslag och tom inmatning
                        if (string.IsNullOrWhiteSpace(sheetName))
                        {
                            Console.WriteLine("Ogiltigt bladnamn, namn kan inte vara tomt eller enadst mellanslag");
                            namesSorter.returnBackToMenu();
                        }
                        // Använder SQL-fråga för att läsa data från excelfilen
                        OleDbCommand command = new OleDbCommand($"SELECT * FROM [{sheetName}$]", connection);
                        OleDbDataAdapter adapter = new OleDbDataAdapter(command);

                        // Använder databalbe för att hämta data från filen
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // Lägg till namn från första kolumnen i names-listan
                        foreach (DataRow row in dataTable.Rows)
                        {
                            string name = row[0].ToString();
                            // Adderar namn om data finns i den raden
                            if (!string.IsNullOrEmpty(name))
                            {
                                names.Add(name);
                            }
                        }
                        Console.WriteLine($"Namnen har lagts till. {dataTable.Rows.Count} rader har blivit inlästa.");
                    }
                }
                // Hittar inte angivna filen
                catch (FileNotFoundException ex)
                {
                    Console.WriteLine($"Excel-filen hittades inte: {ex.Message}");
                    namesSorter.returnBackToMenu();
                }
                catch (OleDbException ex)
                {
                    Console.WriteLine($"Fel vid anslutning till Excel-filen: {ex.Message}");
                    namesSorter.returnBackToMenu();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Fel vid inläsning av Excel-filen: {ex.Message}");
                    namesSorter.returnBackToMenu();
                }
            }
            // Metod för att återgå till menyn
            public void returnBackToMenu()
            {
                Console.WriteLine("\nTryck på valfri tangent för att återgå till menyn");
                Console.ReadKey();
                //--------------Metod för menyn------------------------
            }

        }

        private void AddNamesManually()
        {
            throw new NotImplementedException();
        }

        //Metod för att ta bort namn
        public void RemoveName()
        {
            while (true)
            {
                switch (Console.ReadKey(true).Key)
                {
                    //Ta bort namn
                    case ConsoleKey.D1:

                        break;

                    //Återgå till meny
                    case ConsoleKey.Escape:
                        break;
                }

            }
        }
    }
}


