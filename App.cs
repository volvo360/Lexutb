using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfBloggy
{
    public class App
    {
        static BlogContext context = new BlogContext();

        public void Run()
        {
            //Functions.ClearDatabase();                //Körs en gång sedan kommenteras ut
            //Functions.AddSomeTitles();                //Körs en gång sedan kommenteras ut
            if (!context.TestConnection())
            {
                Write("Vänligen kör Update-Database i Package Manager Console först för att sätta upp databasen!!! Tryck på en tangent för att komma vidare!");
                Console.ReadKey();

                return;
            }

            if (context.BlogPosts.Count() == 0)
                Functions.AddSomeTitles();

            MainMenu();
        }

        private void ShowAllBlogPostsBrief()
        {
            foreach (var x in context.BlogPosts)
            {
                WriteLine(x.Id.ToString().PadRight(5) + x.Title.PadRight(30) + x.Author.PadRight(20));
            }
        }

        public void MainMenu()
        {
            while (true)
            {
                Header("Huvudmeny");

                ShowAllBlogPostsBrief();

                Console.WriteLine();

                Console.WriteLine("a) Addera en ny bloggpost");
                Console.WriteLine("b) Uppdatera en blogpost");
                Console.WriteLine("c) Radera en blogpost");
                Console.WriteLine("q) Avluta programmet");

                Console.WriteLine();

                Console.Write("Vad är ditt val? ");
                ConsoleKey command = Console.ReadKey(true).Key;

                if (command == ConsoleKey.A)
                    AddPost();

                else if (command == ConsoleKey.B)
                    PageUpdatePost();

                else if (command == ConsoleKey.C)
                    DeletePost();

                else if (command == ConsoleKey.Q)
                    break;
            }
        }

        private void AddPost()
        {
            bool masterBreak = true;

            while (masterBreak)
            {
                Header("Addera inlägg");

                Write("Vem är författare (q = avbryt)? ");

                string writer = Console.ReadLine().Trim();

                if (writer.ToLower() == "q")
                {
                    break;
                }

                else if (writer.Length > 0)
                {
                    while (true)
                    { 
                        Write("Ange titel (q = avbryt) : ");

                        string title = Console.ReadLine().Trim();

                        if (title.ToLower() == "q")
                        {
                            masterBreak = false;
                            break;
                        }
                        else if (title.Length > 0)
                        {
                            Functions.AddPost(title, writer);

                            Write("Bloggposten adderad, tryck på en tangent för att forstätta!");
                            Console.ReadKey();

                            break;
                        }
                    }
                }
            }
        }

        private void PageUpdatePost()
        {
            Console.Clear();

            while (true)
            { 
                Header("Uppdatera");

                ShowAllBlogPostsBrief();

                Write("\nVilken bloggpost vill du uppdatera (q = avbryt)? ");

                bool tryInt = false;
                
                string StringBlogPostId = Console.ReadLine().Trim();

                if (StringBlogPostId.ToLower() == "q")
                {
                    break;
                }

                int blogPostId = 0;

                tryInt = int.TryParse(StringBlogPostId, out blogPostId);

                if (tryInt)
                {
                    var blogPost = context.BlogPosts.Find(blogPostId);

                    if (blogPost == null)
                    {
                        Write("Bloggposten fins INTE, vänligen tryck på en tangent för nytt försök!");
                        Console.ReadKey();
                    }

                    else
                    { 
                        WriteLine("Den nuvarande titeln är: " + blogPost.Title);

                        Write("Skriv in ny titel: ");

                        string newTitle = Console.ReadLine();

                        blogPost.Title = newTitle;

                        context.BlogPosts.Update(blogPost);
                        context.SaveChanges();

                        Write("Bloggposten uppdaterad.");
                        Console.ReadKey();

                        break;
                    }
                }
                else
                {
                    Write("Inte giltig id på posten du vill editera, vänligen tryck på en tangent för nytt försök!");
                    Console.ReadKey();
                }
            }
        }

        private void DeletePost()
        {
            while(true)
            { 
                Header("Uppdatera");

                ShowAllBlogPostsBrief();

                Write("\nVilken bloggpost vill du uppdatera (q = avbryt)? ");

                bool tryInt = false;

                string StringBlogPostId = Console.ReadLine().Trim();

                if (StringBlogPostId.ToLower() == "q")
                {
                    break;
                }

                int blogPostId = 0;

                tryInt = int.TryParse(StringBlogPostId, out blogPostId);

                if (tryInt)
                {
                    var blogPost = context.BlogPosts.Find(blogPostId);

                    if (blogPost == null)
                    {
                        Write("Bloggposten fins INTE, vänligen tryck på en tangent för nytt försök!");
                        Console.ReadKey();
                    }

                    else
                    {
                        while (true)
                        {
                            Write("Vill du verkligen radera posten \"" + blogPost.Title + "\"" + " av " + blogPost.Author + " (j/N/q)? ");

                            var confirmInput = Console.ReadLine().Trim().ToLower();

                            if (confirmInput == "" || confirmInput == "n")
                            {
                                break ;
                            }

                            else if (confirmInput == "q")
                            {
                                return;
                            }
                            else if (confirmInput == "j")
                            {
                                Functions.RemovePost(blogPostId);
                                Write("Bloggposten raderard, vänligen tryck på en tangent för att komma till huvud menyn!");
                                Console.ReadKey();
                                return;
                            }
                        }
                    }
                }
            }
        }

        private void Header(string text)
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine();
            Console.WriteLine(text.ToUpper());
            Console.WriteLine();
        }
        private void WriteLine(string text = "")
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(text);
        }

        private void Write(string text)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(text);
        }
    }

}
