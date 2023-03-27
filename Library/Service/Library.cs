using Library.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Library.Service
{
    internal class Library<W> where W : Basee
    {
        private int counter2 = 0;
        List<W>Books = new List<W>();
        List<W> DeletedOnes=new List<W>();

        private int counter = 0;

        public void AddBook(W book)
        {
            Console.ForegroundColor= ConsoleColor.White;
            Console.WriteLine("enter the name of book");
            AGAIN:
            book.Name = Console.ReadLine();
            if(!Helper.CheckNmae(book.Name))
            {
                goto AGAIN;
            }
            Console.WriteLine("enter author of the book");
            AGAINV2:
            book.AuthorName = Console.ReadLine();
            if (!Helper.CheckNmae(book.AuthorName))
            {
                goto AGAINV2;
            }
            Console.WriteLine("enter the Page Count of the book");
            AGAIN2:
            try
            {
                Console.ForegroundColor = ConsoleColor.White;
                book.PageCount = Convert.ToInt32(Console.ReadLine());
                if (book.PageCount < 100)
                {
                    Console.ForegroundColor= ConsoleColor.Red;
                    Console.WriteLine("books that have less than 100 pages are not accapted");
                    goto AGAIN2;
                }
                Console.ForegroundColor= ConsoleColor.White;
                Console.WriteLine("Do you wanna add extra information about the book?");
                Console.WriteLine("1-->yes");
                Console.WriteLine("2-->no");
                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        book.ExtraInfo = Console.ReadLine();
                        goto HERE;
                    case 2:
                    HERE:
                        Books.Add(book);
                        counter++;
                        book.ID = counter;
                        break;
                    default:
                        Console.ForegroundColor=ConsoleColor.Red;
                        Console.WriteLine("there are only two choices");
                        break;
                }
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("enter something accapted");
            }
            

            

        }

        public void GetAllBooks()
        {
            try
            {
                if (Books.Count == 0)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("not ant single book was added");
                    return;
                }
                foreach (var book in Books)
                {
                    Console.ForegroundColor= ConsoleColor.Yellow;
                    Console.WriteLine($"name-->{book.Name} Author-->{book.AuthorName} Page Count-->{book.PageCount} ID-->{book.ID}");
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("Do you wanna see the extra info about the books?");
                Console.WriteLine("1-->YES");
                Console.WriteLine("2-->NO");
                int select = int.Parse(Console.ReadLine());

                switch (select)
                {
                    case 1:
                       
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("Enter ID of the book that you wanna see the extrainfo");
                        int select100 = Convert.ToInt32(Console.ReadLine());
                        foreach (var item in Books)
                        {
                            if (select100 == item.ID && item.ExtraInfo == null)
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("This book does not have any extra info");
                                break;
                            }
                            else if (select100 == item.ID && item.ExtraInfo != null)
                            {
                                Console.ForegroundColor = ConsoleColor.Yellow;
                                Console.WriteLine(item.ExtraInfo);
                            }
                        }
                        break;
                    case 2:
                        break;
                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("there are only two choices");
                        break;
                }
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Enter something accapted");
            }
           
        }

        public void Delete()
        {
            if (Books.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("not any single book was added");
                return;
            }
            foreach (var item in Books)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"name-->{item.Name} ID-->{item.ID}");
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Enter the ID of the book you wanna delete");
            try
            {
                int select2 = Convert.ToInt32(Console.ReadLine());
                foreach (var item in Books.ToList())
                {

                    if (select2 == item.ID)
                    {
                        item.IsDeleted = true;
                        DeletedOnes.Add(item);
                        Books.Remove(item);
                        counter2++;
                        break;
                    }
                }
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("enter something accapted");
            }
           
        }

        public void Update()
        {
            if(Books.Count== 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Not a single book was added");
                return;
            }
            foreach (var item in Books)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine($"name-->{item.Name} Author-->{item.AuthorName} ID-->{item.ID}");
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("enter ID of the book that you wanna update");
            try
            {
                int IDD = Convert.ToInt32(Console.ReadLine());
                foreach (var item in Books.ToList())
                {
                    if (item.ID == IDD && item.IsUptated == true)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("This book already udated");
                        break;
                    }
                    if (item.ID == IDD && item.IsUptated == false)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("What you wanna update about book?");
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("1-->Name");
                        Console.WriteLine("2-->Author");
                        Console.WriteLine("3-->Extra info");
                        Console.WriteLine("4-->Page count");
                        Console.ForegroundColor = ConsoleColor.White;
                        int Decide = int.Parse(Console.ReadLine());

                        switch (Decide)
                        {
                            case 1:
                                Console.WriteLine("Enter the new name of the book BUT you can change only once");
                                item.Name = Console.ReadLine();
                                item.IsUptated = true;
                                break;
                            case 2:
                                Console.WriteLine("Enter the new Author name of the book");
                                item.AuthorName = Console.ReadLine();
                                item.IsUptated = true;
                                break;
                            case 3:
                                foreach (var itemm in Books.ToList())
                                {
                                    if (itemm.ID == IDD && itemm.ExtraInfo == null)
                                    {
                                        Console.ForegroundColor = ConsoleColor.White;
                                        Console.WriteLine("This book doesn't have extra info");
                                        Console.ForegroundColor = ConsoleColor.Green;
                                        Console.WriteLine("1-->Stiill change");
                                        Console.WriteLine("2-->I don't wanna change");
                                        Console.ForegroundColor = ConsoleColor.White;
                                        int select10 = Convert.ToInt32(Console.ReadLine());

                                        switch (select10)
                                        {
                                            case 1:
                                                itemm.ExtraInfo = Console.ReadLine();
                                                item.IsUptated = true;
                                                break;
                                            case 2:
                                                break;
                                            default:
                                                Console.WriteLine("There are only two choices");
                                                break;
                                        }

                                    }
                                    else if (itemm.ID == IDD && itemm.ExtraInfo != null)
                                    {
                                        Console.WriteLine("enter the new extra info");
                                        itemm.IsUptated = true;
                                        string NEwExtra = Console.ReadLine();
                                        itemm.ExtraInfo = NEwExtra;
                                    }
                                }
                                break;
                            case 4:
                                Console.WriteLine("enter the new amount of the page count");
                                AGAIN6:
                                int NEWpageCount=Convert.ToInt32(Console.ReadLine());
                                if (NEWpageCount < 100)
                                {
                                    Console.WriteLine("books that have less than 100 pages are not accaptedd");
                                    goto AGAIN6;
                                }
                                else
                                {
                                    item.IsUptated = true;
                                    item.PageCount = NEWpageCount; 
                                }
                                break;
                        }
                    }
                }
            }
            catch (Exception)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("enter somthing accapted");
            }
          
        }

        public void GetAllDeletedbooks()
        {
            if (DeletedOnes.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Not a single book was deleted");
                return;
            }
            else
            {
                foreach (var item in DeletedOnes)
                {
                    Console.ForegroundColor=ConsoleColor.Yellow;
                    Console.WriteLine($"name-->{item.Name} author-->{item.AuthorName} page-->{item.PageCount}");
                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine("choose one of the operations");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("1-->Go back to menu");
                Console.WriteLine("2-->Get back the book");
                Console.WriteLine("3-->Delete the book completely");
                Console.ForegroundColor = ConsoleColor.White;
                int select11 =Convert.ToInt32(Console.ReadLine());

                switch (select11)
                {
                    case 1:
                        break;
                    case 2:
                        foreach (var item in DeletedOnes)
                        {
                            Console.WriteLine($"name-->{item.Name}  id-->{item.ID}");
                        }
                        Console.WriteLine("Choose ID of the book that you wanna get back");
                        int select12=Convert.ToInt32(Console.ReadLine());
                        foreach (var item in DeletedOnes.ToList())
                        {
                            if (select12 == item.ID)
                            {
                                counter2--;
                                Books.Add(item);
                                DeletedOnes.Remove(item);
                            }
                        }
                        break;
                    case 3:
                        foreach (var item in DeletedOnes)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine($"name-->{item.Name}  id-->{item.ID}");
                        }
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.WriteLine("enter ID of the book that you wanna delete COMPLEATELY!!");
                        try
                        {
                            int ChooseID = int.Parse(Console.ReadLine());

                            foreach (var item in DeletedOnes.ToList())
                            {
                                if (ChooseID == item.ID)
                                {
                                    DeletedOnes.Remove(item);
                                    break;
                                }
                            }
                        }
                        catch (Exception)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("enter something accapted");
                        }
                        break;
                    default:
                        break;
                }

                
            }
            
        }

        public void Change()
        {
            if (Books.Count == 0)
            {
                Console.ForegroundColor= ConsoleColor.Red;
                Console.WriteLine("not a single book was added");
                return;
            }
            if(Books.Count == 1)
            {
                Console.ForegroundColor= ConsoleColor.Red;
                Console.WriteLine("at least two books must be added to start this operation");
                return;
            }
            Console.ForegroundColor= ConsoleColor.White;
            Console.WriteLine("Choose one of the operations");
            Console.ForegroundColor=ConsoleColor.Green;
            Console.WriteLine("1-->Change the Values");
            Console.WriteLine("2-->Go back the Menue");

            int select20=Convert.ToInt32(Console.ReadLine());

            switch (select20)
            {
                case 1:
                    foreach (var item in Books)
                    {
                        Console.WriteLine($"name-->{item.Name}  author-->{item.AuthorName} ID-->{item.ID}");
                    }
                    Console.WriteLine("choose  the first book that you wanna change");
                    int select25=int.Parse(Console.ReadLine());
                    foreach (var item in Books.ToList())
                    {
                        if (select25 == item.ID)
                        {
                            Console.WriteLine("choose the second book you wanna change");
                            int select26=Convert.ToInt32(Console.ReadLine());
                            foreach (var itemm in Books.ToList())
                            {
                                if (select26 == itemm.ID)
                                {
                                    Console.WriteLine("choose one of the operations");
                                    Console.WriteLine("1-->Change the name");
                                    Console.WriteLine("2-->Change Author");
                                    int select27 = Convert.ToInt32(Console.ReadLine());

                                    switch (select27)
                                    {
                                        case 1:
                                            string empthy = string.Empty;
                                            string empthy2=string.Empty;
                                            empthy = itemm.Name;
                                            empthy2 = item.Name;
                                            item.Name = empthy;
                                            itemm.Name= empthy2;
                                            break;
                                        case 2:
                                            string empthy3 = string.Empty;
                                            string empthy4 = string.Empty;
                                            empthy3 = item.AuthorName;
                                            empthy4 = itemm.AuthorName;
                                            item.AuthorName = empthy4;
                                            itemm.AuthorName= empthy3;
                                            break;
                                        default:
                                            Console.ForegroundColor = ConsoleColor.Red;
                                            Console.WriteLine("there are only two choices");
                                            break;
                                    }
                                }
                            }
                           
                        }
                    }
                    break;
                case 2:
                    break;
                default:
                    Console.ForegroundColor= ConsoleColor.Red;
                    Console.WriteLine("There are only two choices");
                    break;
            }
        }

        public void FindById()
        {
            if(Books.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("not any single book was added");
                return;
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("choose one of the operations");
            Console.ForegroundColor=ConsoleColor.Green;
            Console.WriteLine("1-->Find the book by its ID");
            Console.WriteLine("2-->Go back to menu");
            int select28=Convert.ToInt32(Console.ReadLine());

            switch (select28)
            {
                case 1:
                    Console.WriteLine("enter the ID of the book that you wanna find");
                    int enter=int.Parse(Console.ReadLine());
                    foreach (var item in Books)
                    {
                        if (item.ID == enter)
                        {
                            Console.ForegroundColor = ConsoleColor.Yellow;
                            Console.WriteLine($"name-->{item.Name} author-->{item.AuthorName} page-->{item.PageCount}");
                            return;
                        }
                    }
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("there is not any book with such an ID");
                    break;
                case 2:
                    break;
                default:
                    break;
            }
        }

        public void ShowNumberDeleted()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Number of the books that you deleted-->"+counter2);
        }
    }
}
