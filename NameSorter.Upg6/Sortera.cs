using System.Globalization;

namespace NameSorter
{

    class NamnLista
    {
        public List<string> Namn { get; set; }

        public NamnLista()
        {
            Namn = new List<string> { "Test1", "Test2", "Test3" };
        }
    }
    public class Sortera // Class för sortering
    {

        public void SorteraNamn(string[] args)
        {
            NamnLista namnLista = new NamnLista();

            // Skriv ut den ursprungliga listan
            Console.WriteLine("Original lista:");
            foreach (var name in namnLista.Namn)
            {
                Console.WriteLine(name);
            }

            Console.WriteLine("Välj språk för sortering:");
            Console.WriteLine("1. Svenska (sv-SE)");
            Console.WriteLine("2. Engelska (en-US)");
            Console.WriteLine("3. Norska (nb-NO)");
            Console.WriteLine("4. Finska (fi-FI)");
            Console.WriteLine("5. Danska (da-DK)");

            string choice = Console.ReadLine();
            CultureInfo culture;

            // Ställ in språkkultur beroende på användarens val
            switch (choice)
            {
                case "1":
                    culture = new CultureInfo("sv-SE"); // Svenska
                    break;
                case "2":
                    culture = new CultureInfo("en-US"); // Engelska
                    break;
                case "3":
                    culture = new CultureInfo("nb-NO"); // Norska
                    break;
                case "4":
                    culture = new CultureInfo("fi-FI"); // Finska
                    break;
                case "5":
                    culture = new CultureInfo("da-DK"); // Danska
                    break;
                default:
                    Console.WriteLine("Ogiltigt val, använder standardspråket (svenska).");
                    culture = new CultureInfo("sv-SE");
                    break;
            }

            // Sortera namnlistan med det valda språket användaren har valt.
            namnLista.Namn.Sort(StringComparer.Create(culture, true));

            // Visa den sorterade listan
            Console.WriteLine("\nSorterad namnlista:");
            foreach (var namn in namnLista.Namn)
            {
                Console.WriteLine(namn);
            }
        }
    }
}