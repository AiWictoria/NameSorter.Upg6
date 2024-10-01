using System.Data;
using System.Data.OleDb;

namespace NameSorter
{
    class Program
    {
        static void Main(string[] args)
        {

            List<string> names = new List<string>();
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
        public class NameSorter(List<string> names)
        {

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
                        //Anropar metoden för att lägga in namn manuellt
                        case ConsoleKey.D1:
                            AddNamesManually(names);
                            break;

                        //Anropar metoden för att lägga in namn via Excellista
                        case ConsoleKey.D2:
                            AddNamesExcel(names);
                            break;

                        //Återgå till meny
                        case ConsoleKey.Escape:
                            returnBackToMenu();
                            break;
                        default:
                            Console.WriteLine("Ogiltligt val, vänligen ange ett av val i menyn");
                            continue;
                    }

                }
            }
            //Metod för att lägga in namn med excel lista
            public void AddNamesExcel(List<string> names)
            {
                //Låter användaren lägga in excel fil + felhantering
                try
                {
                    Console.Write("\n--- Notera att namnen behöver finnas i första kolumnen i din excelfil samt läses in från rad två ---" +
                        "\nExempel: C:\\Users\\Name\\Dokuments\\Filename\n" +
                        "\nVänligen ange sökväg till excelfilen för att förtsätta: ");

                    string filePath = Console.ReadLine();

                    //Kontrollerar mellanslag och tom inmatning
                    if (string.IsNullOrWhiteSpace(filePath))
                    {
                        Console.WriteLine("Ogiltig filväg. Filvägen behöver se ut enligt följande: \nExempel: C:\\Users\\Name\\Dokuments\\Filename\n");
                        returnBackToMenu();
                    }

                    string connectionString = $"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={filePath};Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1\"";
                    //Om filväg existerar går programmet vidare till att ansluta till excelfilen och efterfrågar blad
                    using (OleDbConnection connection = new OleDbConnection(connectionString))
                    {
                        connection.Open();

                        Console.Write("\nVänligen ange bladnamn där namn ska hämtas ifrån: ");
                        string sheetName = Console.ReadLine();

                        //Kontrollerar mellanslag och tom inmatning
                        if (string.IsNullOrWhiteSpace(sheetName))
                        {
                            Console.WriteLine("Ogiltigt bladnamn, namn kan inte vara tomt eller enadst mellanslag");
                            returnBackToMenu();
                        }
                        //Använder SQL-fråga för att läsa data från excelfilen
                        OleDbCommand command = new OleDbCommand($"SELECT * FROM [{sheetName}$]", connection);
                        OleDbDataAdapter adapter = new OleDbDataAdapter(command);

                        //Använder databalbe för att hämta data från filen
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        //Lägg till namn från första kolumnen i names-listan
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
                //Hittar inte angivna filen
                catch (FileNotFoundException ex)
                {
                    Console.WriteLine($"Excel-filen hittades inte: {ex.Message}");
                    returnBackToMenu();
                }
                catch (OleDbException ex)
                {
                    Console.WriteLine($"Fel vid anslutning till Excel-filen: {ex.Message}");
                    returnBackToMenu();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Fel vid inläsning av Excel-filen: {ex.Message}");
                    returnBackToMenu();
                }
            }
            //Metod för att återgå till menyn Behöver flyttas till Meny classen 
            public void returnBackToMenu()
            {
                Console.WriteLine("\nTryck på valfri tangent för att återgå till menyn");
                Console.ReadKey();
                //--------------Metod för menyn------------------------
            }

            //Metod för att lägga till namn manuellt
            private void AddNamesManually(List<string> names)
            {
                Console.Write("Ange hur många namn du vill lägga till: ");
                try //Felhanteringen
                {
                    int antal = int.Parse(Console.ReadLine());

                    for (int i = 1; i <= antal; i++)
                    {
                        Console.Write("Ange namn du vill lägga till: ");
                        string namn = Console.ReadLine();
                        //Om det angivna namnet inte finns lägger till i listan
                        if (!names.Contains(namn))
                        {
                            names.Add(namn);
                            Console.WriteLine($"Namn {namn} har lagts till.\n");
                        }
                        //Om namn finns skriver det ut det till användaren och går vidare i loopen
                        else
                        {
                            Console.WriteLine($"Namn {namn} finns redan registrerat.");
                            continue;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Fel inmatning: {ex.Message}");
                    returnBackToMenu();
                }
            }
            //Metod för att ta bort namn
            public void RemoveNames(List<string> names)
            {
                while (true)
                {
                    Console.WriteLine("Välj ett av följande: " +
                                      "\n\t1. Ta bort namn" +
                                      "\n\tEsc. Återgå till menyn");
                    //Begränsar med endast key input för menyval
                    var key = Console.ReadKey(true).Key;

                    if (key == ConsoleKey.D1)
                    {
                        Console.Write("Ange namnet som ska tas bort: ");
                        string removeName = Console.ReadLine();
                        //Om namnet finns, tas den bort
                        if (names.Contains(removeName))
                        {
                            names.Remove(removeName);
                            Console.WriteLine($"Namnet {removeName} har tagits bort.");
                        }
                        //Om namn inte finns skriver programmet ut det och användaren skickas tillbaka till menyn
                        else
                        {
                            Console.WriteLine($"Namnet {removeName} finns inte i listan.");
                            returnBackToMenu();
                        }
                    }
                    //Återgår till menyn
                    else if (key == ConsoleKey.Escape)
                    {
                        returnBackToMenu();
                    }
                    //Felhantering
                    else
                    {
                        Console.WriteLine("Ogiltlig val, vänligen väl ett av valen i menyn");
                    }
                }
            }
            //Metod för att ändra till stor första bokstav
            public string CapitalizeFirstLetter(string input)
            {
                try
                {
                    // Ändra första bokstaven till stor bokstav och resten till små bokstäver
                    return char.ToUpper(input[0]) + input.Substring(1).ToLower();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                    return input;
                }
            }
        }





    }
}


