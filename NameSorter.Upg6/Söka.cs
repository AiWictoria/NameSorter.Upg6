namespace NameSorter
{
    public class Söka
    {
        internal void SökaNamn()
        {
            NamnLista namnLista = new NamnLista();

            // Begär användarens input för sökning
            Console.WriteLine("\nEnter name to search");
            string searchName = Console.ReadLine();

            // Kontrollera att input inte är tomt
            if (string.IsNullOrWhiteSpace(searchName))
            {
                Console.WriteLine("Input cannot be empty. Please enter a valid name.");
            }
            else
            {
                // Använd binärsökning för att söka efter namnet, ignorerar stora/små bokstäver
                int index = namnLista.Namn.BinarySearch(searchName, StringComparer.OrdinalIgnoreCase);

                if (index >= 0)
                {
                    Console.WriteLine($"{searchName} is in the list.");
                }
                else
                {
                    Console.WriteLine($"{searchName} is not in the list.");

                }
            }
        }
    }
}
