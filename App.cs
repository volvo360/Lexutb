using System;
using System.Collections.Generic;
using Bloggy.Demo.Domain;

namespace Bloggy.Demo
{
    public class App
    {
        DataAccess _dataAccess = new DataAccess();

        public void Run()
        {
            _dataAccess.checkDatabase();

            PageMainMenu();
        }

        private void PageMainMenu()
        {
            while (true)
            {
                Console.Clear();

                Header("Huvudmeny");
            
                ShowAllBlogPostsBrief();

                WriteLine("Vad vill du göra?\n");
                WriteLine("a) Addera en ny post");
                WriteLine("b) Uppdatera en bloggpost");
                WriteLine("c) Raddera en bloggpost");
                WriteLine("d) Hantera skribenter");
                WriteLine("q) Avsluta programmet");

                ConsoleKey command = Console.ReadKey(true).Key;

                if (command == ConsoleKey.A)
                    AddNewPost();

                else if (command == ConsoleKey.B)
                    PageUpdatePost();

                else if (command == ConsoleKey.C)
                    DeleteBlogPost();

                else if (command == ConsoleKey.D)
                    manageWriterMenu();

                else if (command == ConsoleKey.Q)
                    return;
            }
        }

        private void AddNewPost()
        {
            while (true)
            {
                Header("Addera nytt inlägg");

                //Some code below added by Anders Wallin, Helsingborg, 2022-01-18
                Write("Vem är bloggaren? ");
             
                string inputAuthor = Console.ReadLine().Trim();

                if (_dataAccess.ValidateAuthor(inputAuthor).exists)
                {
                    while (true)
                    {
                        Write("Skriv titelen på inlägget : ");

                        string inputTitle = Console.ReadLine().Trim();

                        if (inputTitle.Length > 0)
                        {
                            BlogPost blogPost = new BlogPost();

                            blogPost.Title = inputTitle;
                            blogPost.Author = inputAuthor;

                            _dataAccess.AddBlogPost(blogPost);
                                                       
                            WriteLine("\nPosten har adderats! Tryck på en tangent för att börja om....");

                            Console.ReadKey(true);

                            return;
                        }
                    }
                }
                else
                {
                    Write("Författaren existerar inte i systemet!\n");
                    Write("Vill du se alla författare i systemet (J/n/q)? ");

                    inputAuthor = Console.ReadLine().Trim().ToLower();

                    if (inputAuthor == "" || inputAuthor == "j")
                    {
                        List<BlogAuthor> authorList = new List<BlogAuthor>();

                        authorList = _dataAccess.ListAuthors();

                        Header("Id".PadRight(5) + "Författare".PadRight(30) + "");

                        foreach (var author in authorList)
                        {
                            WriteLine(author.Id.ToString().PadRight(5)+author.AuthorName.PadRight(15));
                        }
                        
                    }
                    else if (inputAuthor == "q")
                    {
                        return;
                    }
                    else if (inputAuthor == "n")
                    {
                        continue;
                    }
                }

                Write("\nTryck på en tangent för att börja om....");

                Console.ReadKey(true);

                break;
            }
        }

        private void PageUpdatePost()
        {
            while (true)
            { 
                Header("Uppdatera");

                ShowAllBlogPostsBrief();

                Write("Vilken bloggpost vill du uppdatera? ");

                var input = Console.ReadLine().Trim().ToLower();

                if (input == "q")
                {
                    return;
                }

                int postId =0;

                bool res = int.TryParse(input, out postId);

                if (!res)
                {
                    continue;
                }

                BlogPost blogpost = _dataAccess.GetPostById(postId);

                //Added fix by Anders Wallin, Helsingborg, 2022-01-18

                if (blogpost != null)
                {
                    WriteLine("Den nuvarande titeln är: " + blogpost.Title);

                    Write("Skriv in ny titel: ");

                    string newTitle = Console.ReadLine();

                    blogpost.Title = newTitle;

                    _dataAccess.UpdateBlogpost(blogpost);

                    Write("Bloggposten uppdaterad. Vänlig tryck på en tangent!");
                    Console.ReadKey();
                }
                else
                {
                    WriteLine("Bloggposten existerar inte! Vänligen försök på nytt genom att trycka på en tangent!");
                    Console.ReadKey();
                }
            }
        }

        //Added by Anders Wallin, Helsingborg, 2022-01-18
        private void DeleteBlogPost()
        {
            while (true)
            {
                Header("Radera bloggpost");

                ShowAllBlogPostsBrief();

                Write("Vilken bloggpost vill du raddera? ");

                var input = Console.ReadLine().Trim().ToLower();

                if (input == "q")
                {
                    return;
                }

                int postId = 0;

                bool res = int.TryParse(input, out postId);

                if (!res)
                {
                    continue;
                }

                BlogPost blogpost = _dataAccess.GetPostById(postId);

                if (blogpost != null)
                {
                    Write("Vill du verkligen radera bloggposten : " + blogpost.Title +" av " + blogpost.Author+" (j/N/q = avbryt) ");

                    string verifyDeletePost = Console.ReadLine().Trim().ToLower();

                    if (verifyDeletePost == "" || verifyDeletePost == "n")
                    {
                        break;
                    }
                    else if (verifyDeletePost == "q")
                    {
                        return;
                    }
                    else if (verifyDeletePost == "j")
                    {
                        _dataAccess.deleteBlogPost(postId);

                        Write("Bloggposten radderad!!!");
                        Console.ReadKey();
                        break;
                    }
                }
                else
                {
                    WriteLine("Bloggposten existerar inte! Vänligen försök på nytt genom att trycka på en tangent!");
                    Console.ReadKey();
                }
            }
        }

        //Added by Anders Wallin, Helsingborg, 2022-01-18
        private void ShowWriters()
        {
            List<BlogAuthor> Authors = _dataAccess.GetAllWriters();

            Header("Skribenter");

            if (Authors == null)
            {
                WriteLine("Det finns inga skribenter än!!!");

                return;
            }

            WriteLine("Id".PadRight(5) + "Författare".PadRight(30)+"\n");

            foreach (BlogAuthor author in Authors)
            {
                WriteLine(author.Id.ToString().PadRight(5) + author.AuthorName.PadRight(30));
            }
        }

        //Added by Anders Wallin, Helsingborg, 2022-01-18
        private void AddNewWriter()
        {
            while (true)
            {
                Header("Addera ny skribent");

                Write("Ange namnet på bloggaren (q = avbryt)? ");

                string inputAuthor = Console.ReadLine().Trim();

                if (inputAuthor == "")
                {
                    continue;
                }
                else if (inputAuthor == "q")
                {
                    return ;
                }
                else if (inputAuthor.Length > 0)
                {
                    var temp = _dataAccess.CheckIfWriterExists(inputAuthor);

                    if (temp.exists)
                    {
                        WriteLine(temp.data.AuthorName+ " finns med id = "+temp.data.Id+". Vänligen tryck på en tanget för att försöka på nytt!");
                        
                        Console.ReadKey();
                    }
                    else
                    {
                        if (_dataAccess.AddBlogWriter(inputAuthor))
                        {
                            WriteLine(inputAuthor + " har adderats till databasen över skribenter! Tryck på en tanget för att komma vidare!");
                        }
                        else
                        {
                            WriteLine(inputAuthor + " kunde INTE adderas till databasen över skribenter! Tryck på en tanget för att komma vidare!");
                        }
                        Console.ReadKey();

                    }
                }
            }
        }

        //Added by Anders Wallin, Helsingborg, 2022-01-18

        private void PageUpdatWriter()
        {
            while (true)
            {
                Header("Editera skribent");

                Write("Ange namnet på bloggaren eller id (q = avbryt)? ");

                var inputAuthor = Console.ReadLine().Trim();

                if (inputAuthor == "")
                {
                    continue;
                }
                else if (inputAuthor == "q")
                {
                    break;
                }
                else if (inputAuthor.Length > 0)
                {
                    int tempId = 0;

                    if (int.TryParse(inputAuthor, out tempId))
                    {
                        var temp = _dataAccess.CheckIfWriterExists(tempId);

                        if (temp.exists)
                        {
                            Write("Vill du verkligen editera användaren \"" + temp.data.AuthorName + "\" (j/N/q = avbryt) ");

                            string chekIfEdit = Console.ReadLine().Trim().ToLower();

                            if (chekIfEdit == "" || chekIfEdit == "n")
                            {
                                return;
                            }

                            else if (chekIfEdit == "n")
                            {
                                continue;
                            }
                            else if (chekIfEdit == "j")
                            { 
                                //WriteLine(temp.data.AuthorName + " finns med id = " + temp.data.Id + ". Vänligen tryck på en tanget för att försöka på nytt!");

                                Write("Ange det nya namnet (q = avbryt) : ");

                                string inputNewName = Console.ReadLine().Trim();

                                var TempAuthor = _dataAccess.CheckIfWriterExists(inputNewName);

                                if (TempAuthor.exists == false)
                                {
                                    BlogAuthor authorTemp = new BlogAuthor();

                                    authorTemp.Id = temp.data.Id;
                                    authorTemp.AuthorName = inputNewName;

                                    _dataAccess.UpdateBlogWritert(authorTemp);
                                }

                                Console.WriteLine(temp.data.AuthorName + " har uppdatterats till " + inputNewName + ". Vänligen tryck på en tanget för att fortsätta!");

                                Console.ReadKey();
                                break;
                            }
                        }
                        else
                        {
                            WriteLine(inputAuthor + " kunde INTE hittas i databasen över skribenter! Tryck på en tanget för att komma vidare!");

                            Console.ReadKey();
                        }
                    }
                    else
                    {
                        var temp = _dataAccess.CheckIfWriterExists(inputAuthor);

                        if (temp.exists)
                        {
                            Write("Vill du verkligen editera användaren \"" + temp.data.AuthorName + "\" (j/N/q = avbryt) ");

                            string chekIfEdit = Console.ReadLine().Trim().ToLower();

                            if (chekIfEdit == "" || chekIfEdit == "n")
                            {
                                return;
                            }

                            else if (chekIfEdit == "n")
                            {
                                continue;
                            }
                            else if (chekIfEdit == "j")
                            {
                                //WriteLine(temp.data.AuthorName + " finns med id = " + temp.data.Id + ". Vänligen tryck på en tanget för att försöka på nytt!");

                                Write("Ange det nya namnet (q = avbryt) : ");

                                string inputNewName = Console.ReadLine().Trim();

                                var TempAuthor = _dataAccess.CheckIfWriterExists(inputNewName);

                                if (TempAuthor.exists == false)
                                {
                                    BlogAuthor authorTemp = new BlogAuthor();

                                    authorTemp.Id = temp.data.Id;
                                    authorTemp.AuthorName = inputNewName;

                                    _dataAccess.UpdateBlogWritert(authorTemp);
                                }

                                Console.WriteLine(temp.data.AuthorName + " har uppdatterats till " + inputNewName + ". Vänligen tryck på en tanget för att fortsätta!");

                                Console.ReadKey();
                                break;
                            }
                        }
                        else
                        {
                            WriteLine(inputAuthor + " kunde INTE hittas i databasen över skribenter! Tryck på en tanget för att komma vidare!");

                            Console.ReadKey();
                        }
                    }                    

                    
                }
            }
        }

        //Added by Anders Wallin, Helsingborg, 2022-01-18

        private void DeleteWriter()
        {
            while (true)
            {
                Header("Radera skribent");

                Write("Ange namnet på bloggaren eller id på skribent som du vill radera (q = avbryt)? ");

                var inputAuthor = Console.ReadLine().Trim();

                if (inputAuthor == "")
                {
                    continue;
                }
                else if (inputAuthor == "q")
                {
                    break;
                }
                else if (inputAuthor.Length > 0)
                {
                    int tempId = 0;

                    if (int.TryParse(inputAuthor, out tempId))
                    {
                        var temp = _dataAccess.CheckIfWriterExists(tempId);

                        if (temp.exists)
                        {
                            Write("Vill du verkligen RADERA användaren \"" + temp.data.AuthorName + "\" (j/N/q = avbryt) ");

                            string chekIfEdit = Console.ReadLine().Trim().ToLower();

                            if (chekIfEdit == "" || chekIfEdit == "n")
                            {
                                return;
                            }

                            else if (chekIfEdit == "n")
                            {
                                continue;
                            }
                            else if (chekIfEdit == "j")
                            {

                                _dataAccess.DeleteBlogWriter(temp.data.Id);

                                Console.WriteLine(temp.data.AuthorName + " har raderats från databasen. Vänligen tryck på en tanget för att fortsätta!");

                                Console.ReadKey();
                                break;
                            }
                        }
                        else
                        {
                            WriteLine(inputAuthor + " kunde INTE hittas i databasen över skribenter! Tryck på en tanget för att komma vidare!");

                            Console.ReadKey();
                        }
                    }
                    else
                    {
                        var temp = _dataAccess.CheckIfWriterExists(inputAuthor);

                        if (temp.exists)
                        {
                            Write("Vill du verkligen RADERA användaren \"" + temp.data.AuthorName + "\" (j/N/q = avbryt) ");

                            string chekIfEdit = Console.ReadLine().Trim().ToLower();

                            if (chekIfEdit == "" || chekIfEdit == "n")
                            {
                                return;
                            }

                            else if (chekIfEdit == "n")
                            {
                                continue;
                            }
                            else if (chekIfEdit == "j")
                            {

                                _dataAccess.DeleteBlogWriter(temp.data.Id);

                                Console.WriteLine(temp.data.AuthorName + " har raderats från databasen. Vänligen tryck på en tanget för att fortsätta!");

                                Console.ReadKey();
                                break;
                            }
                        }
                        else
                        {
                            WriteLine(inputAuthor + " kunde INTE hittas i databasen över skribenter! Tryck på en tanget för att komma vidare!");

                            Console.ReadKey();
                        }
                    }
                }
            }
        }

        //Added by Anders Wallin, Helsingborg, 2022-01-18

        private void manageWriterMenu()
        {
            while (true)
            { 
                Console.Clear();

                Header("Editera skribenter");

                ShowWriters();

                WriteLine();

                WriteLine("Vad vill du göra?\n");
                WriteLine("a) Addera en ny skribent");
                WriteLine("b) Uppdatera en skribent");
                WriteLine("c) Raddera en skribent");
                WriteLine("q) Avsluta hantering av skribenter");

                ConsoleKey command = Console.ReadKey(true).Key;

                if (command == ConsoleKey.A)
                    AddNewWriter();

                else if (command == ConsoleKey.B)
                    PageUpdatWriter();

                else if (command == ConsoleKey.C)
                    DeleteWriter();

                else if (command == ConsoleKey.Q)
                    return;
            }

        }

        private void ShowAllBlogPostsBrief()
        {
            List<BlogPost> list = _dataAccess.GetAllBlogPostsBrief();

            Console.WriteLine("Id".PadRight(5) + "Tidsstämpel".PadRight(30) + "Titel".PadRight(30) + "Författare".PadRight(20)+"\n");

            foreach (BlogPost bp in list)
            {
                WriteLine(bp.Id.ToString().PadRight(5) +bp.Ts.ToString().PadRight(30)+ bp.Title.PadRight(30) + bp.Author.PadRight(20));
            }
            WriteLine();
        }

        private void Header(string text)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();
            Console.WriteLine(text.ToUpper());
            Console.WriteLine();
        }

        public void WriteLine(string text = "")
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(text);
        }

        public void Write(string text)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(text);
        }
    }
}
