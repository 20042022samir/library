

using System.Diagnostics.Tracing;

internal static class Helper
{
    public static bool CheckNmae(string name)
    {
        bool status = true;
        for (int i = 0; i < name.Length; i++)
        {
            if (char.IsDigit(name[i]))
            {
                status = false;
                Console.WriteLine("You can't add any digit to the name");
                break;
            }
        }
        if (!char.IsUpper(name[0]))
        {
            status = false;
            Console.WriteLine("The firsst name must be upper");
        }
        if (string.IsNullOrWhiteSpace(name))
        {
            status = false;
            Console.WriteLine("You can not add empthy place to the name");
        }
        return status;

    }

    public static void Words()
    {
        Console.ForegroundColor = ConsoleColor.DarkMagenta;
        Console.WriteLine("<<MENU>>>");
        Console.ForegroundColor= ConsoleColor.Blue;
        Console.WriteLine("1-->Add Book");
        Console.WriteLine("2-->Delete Book");
        Console.WriteLine("3-->Update Book");
        Console.WriteLine("4-->Show deleted books");
        Console.WriteLine("5--> Show All the books");
        Console.WriteLine("6-->Change the values of the books");
        Console.WriteLine("7-->Find the book by its ID");
        Console.WriteLine("8-->Finish the operation");
        Console.WriteLine("9-->Show number of the books that you deleted");
    }

}
    
    

