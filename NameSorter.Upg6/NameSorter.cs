using System.Data;
using System.Data.OleDb;
using System.Globalization;

namespace NameSorter
{
    public class NameSorter(List<string> names)
    {
        //Deklarerar lista för namn

        //Metod för att lägga till namn
        public void AddNewNames(List<string> names)
        {
            Console.Clear();
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
                    break;
                default:
                    Console.WriteLine("Ogiltligt val, vänligen ange ett av val i menyn");
                    break;
            }

        }
        //Metod för att lägga in namn med excel lista
        public void AddNamesExcel(List<string> names)
        {
            Console.Clear();
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
                            name = ToCapitalFirstLetter(name);
                            names.Add(name);
                        }
                    }
                    Console.WriteLine($"\n{dataTable.Rows.Count} rader har blivit inlästa. Listan innehåller nu följande namn: ");
                    foreach (var name in names)
                    {
                        Console.WriteLine(name);
                    }
                }
            }
            //Felhantering
            catch (FileNotFoundException ex)
            {
                Console.WriteLine($"Excel-filen hittades inte: {ex.Message}");
            }
            catch (OleDbException ex)
            {
                Console.WriteLine($"Fel vid anslutning till Excel-filen: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fel vid inläsning av Excel-filen: {ex.Message}");
            }
        }

        //Metod för att lägga till namn manuellt
        public void AddNamesManually(List<string> names)
        {
            Console.Clear();
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
                        namn = ToCapitalFirstLetter(namn);
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
            }
        }
        //Metod för att ta bort namn
        public void RemoveNames(List<string> names)
        {
            Console.Clear();
            Console.Write("Ange namnet som ska tas bort: ");
            string removeName = Console.ReadLine();
            removeName = ToCapitalFirstLetter(removeName);
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
            }
        }
        //Metod för att ändra till stor första bokstav
        public string ToCapitalFirstLetter(string input)
        {
            try
            {
                // Ändra första bokstaven till stor bokstav och resten till små bokstäver
                return char.ToUpper(input[0]) + input.Substring(1).ToLower();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return input; // Returnera input som den är om ett fel uppstår
            }
        }
        public void SortNames(List<string> names)
        {
            Console.Clear();
            // Skriv ut den ursprungliga listan
            Console.WriteLine("Original lista:");
            foreach (var name in names)
            {
                Console.WriteLine(name);
            }
            Console.WriteLine(" ---- Namn sortering efter språk meny---- \n" +
                "\n\tVälj språk för sortering:" +
                "\n\t1. Svenska (sv-SE)" +
                "\n\t2. Engelska (en-US)" +
                "\n\t3. Norska (nb-NO)" +
                "\n\t4. Finska (fi-FI)" +
                "\n\t5. Danska (da-DK)");

            CultureInfo culture;

            // Ställ in språkkultur beroende på användarens val
            switch (Console.ReadKey(true).Key)
            {
                case ConsoleKey.D1:
                    culture = new CultureInfo("sv-SE"); // Svenska
                    break;
                case ConsoleKey.D2:
                    culture = new CultureInfo("en-US"); // Engelska
                    break;
                case ConsoleKey.D3:
                    culture = new CultureInfo("nb-NO"); // Norska
                    break;
                case ConsoleKey.D4:
                    culture = new CultureInfo("fi-FI"); // Finska
                    break;
                case ConsoleKey.D5:
                    culture = new CultureInfo("da-DK"); // Danska
                    break;
                default:
                    Console.WriteLine("Ogiltigt val, använder standardspråket (svenska).");
                    culture = new CultureInfo("sv-SE");
                    break;
            }

            // Sortera namnlistan med det valda språket användaren har valt.
            names.Sort(StringComparer.Create(culture, true));

            // Visa den sorterade listan
            Console.WriteLine("\nSorterad namnlista:");
            foreach (var namn in names)
            {
                Console.WriteLine(namn);
            }
        }
        // Metod för specifik sökning efter ett namn
        internal void SearchNames(List<string> names)
        {
            Console.Clear();
            // Begär användarens input för sökning
            Console.Write("\nAnge namn att söka: ");
            string searchName = Console.ReadLine();
            searchName = ToCapitalFirstLetter(searchName);
            // Kontrollera att input inte är tomt
            if (string.IsNullOrWhiteSpace(searchName))
            {
                Console.WriteLine("Ogiltligt namn, vänligen ange ett nytt namn.");
            }
            else
            {
                // Metod för att söka efter namn
                if (names.Contains(searchName, StringComparer.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"{searchName} finns i listan.");
                }
                else
                {
                    Console.WriteLine($"{searchName} finns inte i listan.");
                }
            }
        }
    }
}
