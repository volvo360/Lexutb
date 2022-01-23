using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EfBloggyImproved
{
    public class App
    {
        static BlogContext context = new BlogContext();

        public void Run()
        {
            if (!context.TestConnection())
            {
                Write("Vänligen kör Update-Database i Package Manager Console först för att sätta upp databasen!!! Tryck på en tangent för att komma vidare!");
                Console.ReadKey(); 
                
                return;
            }

            if (context.BlogPosts.Count() == 0)
                Functions.AddSomeDefaultData();

            MainMenu();
        }

        private void ShowAllBlogPostsBrief()
        {
            var temp = context.BlogPosts.Include(y => y.blogAuthor).ToList();
            foreach (var x in temp)
            {
                if (x.blogAuthor != null)
                WriteLine(x.Id.ToString().PadRight(5) + x.Title.PadRight(30) + x.blogAuthor.Author.ToString().PadRight(20));
            }
        }

        private void ShowAllBlogers()
        {
            var temp = context.BlogAuthors.ToList();

            WriteLine("Id".PadRight(5) + "Bloggare".PadRight(30));
            WriteLine();

            foreach (var x in temp)
            {
                if (x.Author != null)
                {
                    WriteLine(x.Id.ToString().PadRight(5) + x.Author.PadRight(30));
                }
                    
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
                Console.WriteLine("d) Hantera blogganvändare");
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
                else if (command == ConsoleKey.D)
                    ShowEditerBlogerMenu();

                else if (command == ConsoleKey.Q)
                    break;
            }
        }

        private void AddPost()
        {
            bool masterBreak = true;

            string writer;

            BlogAuthor temp = new BlogAuthor();

            while (masterBreak)
            {
                Header("Addera inlägg");

                Write("Vem är författare (q = avbryt)? ");

                writer = Console.ReadLine().Trim();

                if (writer.ToLower() == "q")
                {
                    return;
                }

                else if (writer.Length > 0)
                {
                    var authorId = 0;

                    bool statusParse = int.TryParse(writer, out authorId);

                    temp = context.BlogAuthors.SingleOrDefault(x => x.Author.ToLower() == writer.ToLower());

                    if (temp != null)
                    {
                        //Do nothing, all is alright
                    }
                    else if (statusParse)
                    {
                        temp = context.BlogAuthors.Single(x => x.Id == authorId);
                        //BlogAuthor author = context.BlogAuthors.Single(c => c.Id == 1);

                        if (temp != null)
                        {
                            //Do nothing, all is alright
                        }

                        else
                        {
                            while (true)
                            {
                                Write("Bloggaren existerar inte i systemet, vill du se alla bloggare (J/n/q = avbryt)? ");

                                string inputShowBlogers = Console.ReadLine().Trim().ToLower();

                                if (inputShowBlogers == "" || inputShowBlogers == "j")
                                {
                                    ShowAllBlogers();
                                    WriteLine();
                                    Write("Tryck på en tangent för att komma vidare!");
                                    Console.ReadKey();

                                    break;
                                }
                                else if (inputShowBlogers == "q")
                                {
                                    return;
                                }
                                else if (inputShowBlogers == "n")
                                {
                                    break;
                                }
                            }

                            continue;
                        }
                    }

                    while (true)
                    { 
                        Write("Ange titel (q = avbryt) : ");

                        string title = Console.ReadLine().Trim();

                        if (title.ToLower() == "q")
                        {
                            masterBreak = false;
                            return;
                        }
                        else if (title.Length > 0)
                        {
                            //Functions.AddSomeDefaultData();

                            Functions.AddPost(title, temp.Id);

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
                Header("Uppdatera bloggpost");

                ShowAllBlogPostsBrief();

                Write("\nVilken bloggpost vill du uppdatera (q = avbryt)? ");

                bool tryInt = false;
                
                string StringBlogPostId = Console.ReadLine().Trim();

                if (StringBlogPostId.ToLower() == "q")
                {
                    return;
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
                Header("Raddera bloggpost");

                ShowAllBlogPostsBrief();

                Write("\nVilken bloggpost vill du radera (q = avbryt)? ");

                bool tryInt = false;

                string StringBlogPostId = Console.ReadLine().Trim();

                if (StringBlogPostId.ToLower() == "q")
                {
                    return;
                }

                int blogPostId = 0;

                tryInt = int.TryParse(StringBlogPostId, out blogPostId);

                if (tryInt)
                {
                    var blogPost = context.BlogPosts.SingleOrDefault(x => x.Id == blogPostId);

                    if (blogPost == null)
                    {
                        Write("Bloggposten fins INTE, vänligen tryck på en tangent för nytt försök!");
                        Console.ReadKey();
                    }

                    else
                    {
                        while (true)
                        {
                            Write("Vill du verkligen radera posten \"" + blogPost.Title + "\"" + " av " + blogPost.blogAuthor.Author + " (j/N/q)? ");

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

        private void ShowEditerBlogerMenu()
        {
            while (true)
            {
                Header("Bloggare");

                ShowAllBlogers();

                Console.WriteLine();

                Console.WriteLine("a) Addera en ny bloggare");
                Console.WriteLine("b) Uppdatera en bloggare");
                Console.WriteLine("c) Radera en bloggare");
                Console.WriteLine("q) Åter till huvudmenyn");

                Console.WriteLine();

                Console.Write("Vad är ditt val? ");
                ConsoleKey command = Console.ReadKey(true).Key;

                if (command == ConsoleKey.A)
                    AddBloger();

                else if (command == ConsoleKey.B)
                    UpdateBloger();

                else if (command == ConsoleKey.C)
                    DeleteBloger();

                else if (command == ConsoleKey.Q)
                    break;
            }

        }

        private void DeleteBloger()
        {
            string writer;

            BlogAuthor temp = new BlogAuthor();
            while (true)
            {
                Header("Raddera bloggare");

                Write("Vem vill du raddera (q = avbryt)? ");

                writer = Console.ReadLine().Trim();

                if (writer.ToLower() == "q")
                {
                    return;
                }

                else if (writer.Length > 0)
                {
                    var authorId = 0;

                    bool statusParse = int.TryParse(writer, out authorId);

                    temp = context.BlogAuthors.SingleOrDefault(x => x.Author.ToLower() == writer.ToLower());

                    if (temp != null)
                    {
                        //Do nothing, all is alright
                        break;
                    }
                    else if (statusParse)
                    {
                        temp = context.BlogAuthors.SingleOrDefault(x => x.Id == authorId);

                        if (temp != null)
                        {
                            //Do nothing, all is alright
                            break;
                        }

                        else
                        {
                            while (true)
                            {
                                Write("Bloggaren existerar inte i systemet, vill du se alla bloggare (J/n/q = avbryt)? ");

                                string inputShowBlogers = Console.ReadLine().Trim().ToLower();

                                if (inputShowBlogers == "" || inputShowBlogers == "j")
                                {
                                    ShowAllBlogers();
                                    WriteLine();
                                    Write("Tryck på en tangent för att komma vidare!");
                                    Console.ReadKey();

                                    break;
                                }
                                else if (inputShowBlogers == "q")
                                {
                                    return;
                                }
                                else if (inputShowBlogers == "n")
                                {
                                    break;
                                }
                            }

                            continue;
                        }
                    }
                }
            }

            while (true)
            {
                Write("Vill du verkligen raddera \"" + temp.Author + "\" (j/N/q = avbryt)? ");

                string deleteUser = Console.ReadLine().Trim().ToLower();

                if (deleteUser == "" || deleteUser == "n")
                {
                    break;
                }
                else if (deleteUser == "q")
                {
                    return;
                }
                else if (deleteUser == "j")
                {
                    Functions.DeleteBloger(temp.Id);

                    Write("Användaren radderad, tyck på en tangent för att återkomma till menyn!");

                    Console.ReadKey();

                    return;
                }
            }
        }

        private void UpdateBloger()
        {
            Console.Clear();

            BlogAuthor blogAuthors = new BlogAuthor();

            while (true)
            {
                Header("Uppdatera bloggare");

                ShowAllBlogPostsBrief();

                Write("\nVilken bloggare vill du uppdatera (q = avbryt)? ");

                bool tryInt = false;

                string StringBlogPost = Console.ReadLine().Trim();

                if (StringBlogPost.ToLower() == "q")
                {
                    return;
                }

                int blogPostId = 0;

                

                tryInt = int.TryParse(StringBlogPost, out blogPostId);

                if (StringBlogPost.Length > 0 && !tryInt)
                { 
                    if (tryInt)
                    {
                        blogAuthors = (BlogAuthor)context.BlogAuthors.Where(x => x.Id == blogPostId);

                        if (blogAuthors == null)
                        {
                            Write("Bloggaren fins INTE, vill du se alla bloggare J/n/q = avbryt!");
                            
                            string showAllBlogers = Console.ReadLine().Trim().ToLower();

                            while (true)
                            {
                                if (showAllBlogers == "" || showAllBlogers == "j")
                                {
                                    ShowAllBlogers();

                                    WriteLine("Tryck på en tangent för att börja om!");
                                    Console.ReadKey();
                                }
                                else if (showAllBlogers == "n")
                                {
                                    break;
                                }
                                else if (showAllBlogers == "q")
                                {
                                    return;
                                }
                            }
                            continue;
                        }

                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        blogAuthors = context.BlogAuthors.SingleOrDefault(x => x.Author.ToLower() == StringBlogPost.ToLower());

                        if (blogAuthors != null)
                        {
                            break;    
                        }
                        else
                        {
                            Write("Bloggaren fins INTE, vill du se alla bloggare J/n/q = avbryt!");

                            string showAllBlogers = Console.ReadLine().Trim().ToLower();

                            while (true)
                            {
                                if (showAllBlogers == "" || showAllBlogers == "j")
                                {
                                    ShowAllBlogers();

                                    WriteLine("Tryck på en tangent för att börja om!");
                                    Console.ReadKey();
                                }
                                else if (showAllBlogers == "n")
                                {
                                    break;
                                }
                                else if (showAllBlogers == "q")
                                {
                                    return;
                                }
                            }
                            continue;
                        }
                    }
                    
                }
                else
                {
                    Write("Inmatningen var ej giltig, vänligen tryck på en tangent för ett nytt försök!");
                    Console.ReadKey();
                }
            }

            while(true)
            {
                Write("Ange det nya namnet (q = avbrytt) : ");

                string inputName = Console.ReadLine().Trim();

                if (inputName == "q")
                {
                    return;
                }

                else if (inputName.Length > 0)
                {
                    BlogAuthor temp = context.BlogAuthors.SingleOrDefault(x => x.Author.ToLower() == inputName.ToLower());

                    if (temp == null)
                    {
                        BlogAuthor tempBlogAuthor = new BlogAuthor();

                        tempBlogAuthor.Author = inputName;
                        tempBlogAuthor.Id = temp.Id;

                        context.BlogAuthors.Update(tempBlogAuthor);
                        context.SaveChanges();

                        Write("Användare har blivit uppdaterad, vänligen tryck på en tangent för att komma åter till menyn!");
                        Console.ReadKey();

                        return;
                    }
                    else
                    {
                        Write("Användarnamnet är redan upptaget av person med id = " + temp.Id + ". Vänligen tryck på en tangent för att börja om!");
                        Console.ReadKey();
                    }
                }
            }
        }

        private void AddBloger()
        {
            Header("Addera bloggare");

            while (true)
            {
                Write("Vem vill du addera (q = avbryt)? ");

                string writer = Console.ReadLine().Trim();

                if (writer.ToLower() == "q")
                {
                    return;
                }

                else if (writer.Length > 0)
                {
                    var authorId = 0;

                    bool statusParse = int.TryParse(writer, out authorId);

                    var temp = context.BlogAuthors.SingleOrDefault(x => x.Author.ToLower() == writer.ToLower());

                    if (temp == null)
                    {
                        BlogAuthor blogAuthor = new BlogAuthor();
                        blogAuthor.Author = writer;
                        blogAuthor.DateAdded = DateTime.Now;   

                        //Do nothing, all is alright
                        Functions.AddBloger(blogAuthor);


                        break;
                    }
                    else
                    {
                        Write("Användaren existerar redan, denna har id = " + temp.Id + ". Vänligen tryck på en tangent för att försöka på nytt!");

                        Console.ReadKey();
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
